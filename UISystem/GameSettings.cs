using Godot;
using System;
using System.Linq;
using UISystem.Common.Enums;
using UISystem.Constants;
using UISystem.Core.Constants;
using static Godot.DisplayServer;

namespace UISystem;
public class GameSettings
{

    public static event Action<float> OnMusicVolumeChanged;
    public static event Action<float> OnSfxVolumeChanged;
    public static event Action<ControllerIconsType> OnControllerIconsChanged;

    private float _musicVolume;
    private float _sfxVolume;

    private readonly ConfigFile _config;

    public float MusicVolume 
    {   
        get => _musicVolume;
        set
        {
            _musicVolume = value;
            OnMusicVolumeChanged?.Invoke(value);
        } 
    }
    public float SfxVolume 
    { 
        get => _sfxVolume; 
        set
        {
            _sfxVolume = value;
            OnSfxVolumeChanged?.Invoke(value);
        }
    }
    public Vector2I Resolution { get; set; } = ConfigData.DefaultResolution;
    public WindowMode WindowMode { get; set; } = ConfigData.DefaultWindowMode;

    // keeping this static as it is easier to select icons for controllers this way
    public static ControllerIconsType ControllerIconsType { get; private set; } = ConfigData.DefaultControllerIconsType;


    public GameSettings(ConfigFile config)
    {
        _config = config;
        LoadSettings();
    }

    public void SaveAudioSettings()
    {
        _config.SetValue(ConfigData.AudioSectionName, ConfigData.MusicVolumeKey, MusicVolume);
        _config.SetValue(ConfigData.AudioSectionName, ConfigData.SfxVolumeKey, SfxVolume);
        Save();
    }

    public void SetControllerIconsType(ControllerIconsType type)
    {
        ControllerIconsType = type;
        _config.SetValue(ConfigData.InterfaceSectionName, ConfigData.ControllerIconsKey, (int)type);
        OnControllerIconsChanged?.Invoke(type);
    }

    public void SaveVideoSettings()
    {
        _config.SetValue(ConfigData.VideoSectionName, ConfigData.ResolutionKey, Resolution);
        _config.SetValue(ConfigData.VideoSectionName, ConfigData.WindowModeKey, (int)WindowMode);
        Save();
    }

    public void ResetInputMapToDefault()
    {
        InputMap.LoadFromProjectSettings();
        SetAllInputsInConfig();
        Save();
    }

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

    public void Save()
    {
        _config.Save(ConfigData.ConfigLocation);
    }

    private void LoadSettings()
    {
        bool saveNewSettings = false;
        MusicVolume = (float)GetConfigValue(ConfigData.AudioSectionName, ConfigData.MusicVolumeKey, ConfigData.DefaultMusicVolume, ref saveNewSettings);
        SfxVolume = (float)GetConfigValue(ConfigData.AudioSectionName, ConfigData.SfxVolumeKey, ConfigData.DefaultSfxVolume, ref saveNewSettings);

        Resolution = (Vector2I)GetConfigValue(ConfigData.VideoSectionName, ConfigData.ResolutionKey, ConfigData.DefaultResolution, ref saveNewSettings);
        WindowMode = (WindowMode)(int)GetConfigValue(ConfigData.VideoSectionName, ConfigData.WindowModeKey, (int)ConfigData.DefaultWindowMode, ref saveNewSettings);

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
        if (isNewSetting) _config.SetValue(sectionName, keyName, value);

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
