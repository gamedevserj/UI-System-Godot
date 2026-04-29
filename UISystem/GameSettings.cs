using System;
using System.Linq;
using Godot;
using UISystem.Common.Enums;
using UISystem.Constants;
using UISystem.Core.Constants;
using static Godot.DisplayServer;

namespace UISystem;

/// <summary>
/// Game settings.
/// </summary>
public class GameSettings
{
    private readonly ConfigFile _config;

    private float _musicVolume;
    private float _sfxVolume;
    private ControllerIconsType _controllerIcons;

    /// <summary>
    /// Initializes a new instance of the <see cref="GameSettings"/> class.
    /// </summary>
    /// <param name="config">Config file.</param>
    public GameSettings(ConfigFile config)
    {
        _config = config;
        LoadSettings();
    }

    /// <summary>
    /// Event fired when music volume is changed.
    /// </summary>
    public static event Action<float> OnMusicVolumeChanged;

    /// <summary>
    /// Event fired when SFX volume is changed.
    /// </summary>
    public static event Action<float> OnSfxVolumeChanged;

    /// <summary>
    /// Event fired when control icons are changed.
    /// </summary>
    public static event Action<ControllerIconsType> OnControllerIconsChanged;

    /// <summary>
    /// Gets or sets music volume.
    /// </summary>
    public float MusicVolume
    {
        get => _musicVolume;
        set
        {
            _musicVolume = value;
            OnMusicVolumeChanged?.Invoke(value);
        }
    }

    /// <summary>
    /// Gets or sets SFX volume.
    /// </summary>
    public float SfxVolume
    {
        get => _sfxVolume;
        set
        {
            _sfxVolume = value;
            OnSfxVolumeChanged?.Invoke(value);
        }
    }

    /// <summary>
    /// Gets or sets resolution.
    /// </summary>
    public Vector2I Resolution { get; set; } = ConfigData.DefaultResolution;

    /// <summary>
    /// Gets or sets window mode.
    /// </summary>
    public WindowMode WindowMode { get; set; } = ConfigData.DefaultWindowMode;

    /// <summary>
    /// Gets or sets controller icon type.
    /// </summary>
    public ControllerIconsType ControllerIconsType
    {
        get => _controllerIcons;
        set
        {
            _controllerIcons = value;
            OnControllerIconsChanged?.Invoke(value);
        }
    }

    /// <summary>
    /// Saves audio settings.
    /// </summary>
    public void SaveAudioSettings()
    {
        _config.SetValue(ConfigData.AudioSectionName, ConfigData.MusicVolumeKey, MusicVolume);
        _config.SetValue(ConfigData.AudioSectionName, ConfigData.SfxVolumeKey, SfxVolume);
        Save();
    }

    /// <summary>
    /// Saves interface settings.
    /// </summary>
    public void SaveInterfaceSettings()
    {
        _config.SetValue(ConfigData.InterfaceSectionName, ConfigData.ControllerIconsKey, (int)ControllerIconsType);
        Save();
    }

    /// <summary>
    /// Saves video settings.
    /// </summary>
    public void SaveVideoSettings()
    {
        _config.SetValue(ConfigData.VideoSectionName, ConfigData.ResolutionKey, Resolution);
        _config.SetValue(ConfigData.VideoSectionName, ConfigData.WindowModeKey, VideoSettings.GetWindwoModeIndex(WindowMode));
        Save();
    }

    /// <summary>
    /// Resets input controls to default.
    /// </summary>
    public void ResetInputMapToDefault()
    {
        InputMap.LoadFromProjectSettings();
        SetAllInputsInConfig();
        Save();
    }

    /// <summary>
    /// Saves input action key.
    /// </summary>
    /// <param name="action">Input action.</param>
    /// <param name="events">Button/key to assign and save.</param>
    public void SaveInputActionKey(string action, Godot.Collections.Array<InputEvent> events)
    {
        InputMap.ActionEraseEvents(action);
        foreach (var item in events)
        {
            InputMap.ActionAddEvent(action, item);
        }

        SetInputInConfig(action);
        Save();
    }

    private void Save()
    {
        _config.Save(ConfigData.ConfigLocation);
    }

    private void LoadSettings()
    {
        bool saveNewSettings = false;
        MusicVolume = (float)GetConfigValue(ConfigData.AudioSectionName, ConfigData.MusicVolumeKey, ConfigData.DefaultMusicVolume, ref saveNewSettings);
        SfxVolume = (float)GetConfigValue(ConfigData.AudioSectionName, ConfigData.SfxVolumeKey, ConfigData.DefaultSfxVolume, ref saveNewSettings);

        Resolution = (Vector2I)GetConfigValue(ConfigData.VideoSectionName, ConfigData.ResolutionKey, ConfigData.DefaultResolution, ref saveNewSettings);
        int windowModeIndex = (int)GetConfigValue(
            ConfigData.VideoSectionName,
            ConfigData.WindowModeKey,
            VideoSettings.GetWindwoModeIndex(ConfigData.DefaultWindowMode),
            ref saveNewSettings);
        WindowMode = VideoSettings.WindowModeOptions[windowModeIndex];

        ControllerIconsType = (ControllerIconsType)(int)GetConfigValue(ConfigData.InterfaceSectionName, ConfigData.ControllerIconsKey, (int)ConfigData.DefaultControllerIconsType, ref saveNewSettings);

        LoadInputs(ref saveNewSettings);

        if (saveNewSettings)
            Save();
    }

    // if config didn't contain the key, saves and returns default value, otherwise returns saved value
    // is used to save newly added keys
    private Variant GetConfigValue(string sectionName, string keyName, Variant defaultValue, ref bool isNewSetting)
    {
        if (!_config.HasSection(sectionName) || !_config.HasSectionKey(sectionName, keyName))
            isNewSetting = true;

        Variant value = _config.GetValue(sectionName, keyName, defaultValue);
        if (isNewSetting)
            _config.SetValue(sectionName, keyName, value);

        return value;
    }

    private void LoadInputs(ref bool saveNewSettings)
    {
        if (!_config.HasSection(ConfigData.KeysSectionName))
        {
            InputMap.LoadFromProjectSettings();
            SetAllInputsInConfig();
            saveNewSettings = true;
            return;
        }

        var savedKeys = _config.GetSectionKeys(ConfigData.KeysSectionName);
        for (int i = 0; i < InputsData.RebindableActions.Length; i++)
        {
            if (!savedKeys.Contains(InputsData.RebindableActions[i]))
            {
                SetInputInConfig(InputsData.RebindableActions[i]);
                saveNewSettings = true;
            }
        }

        for (int i = 0; i < savedKeys.Length; i++)
        {
            var action = savedKeys[i];
            Godot.Collections.Array<InputEvent> events = (Godot.Collections.Array<InputEvent>)_config.GetValue(ConfigData.KeysSectionName, action);

            InputMap.ActionEraseEvents(action);
            for (int k = 0; k < events.Count; k++)
            {
                InputMap.ActionAddEvent(action, events[k]);
            }
        }
    }

    private void SetAllInputsInConfig()
    {
        for (var i = 0; i < InputsData.RebindableActions.Length; i++)
        {
            SetInputInConfig(InputsData.RebindableActions[i]);
        }
    }

    private void SetInputInConfig(string action)
    {
        var events = InputMap.ActionGetEvents(action);
        _config.SetValue(ConfigData.KeysSectionName, action, events);
    }
}
