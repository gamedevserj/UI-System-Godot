using Godot;
using System;
using UISystem.Common.Enums;
using UISystem.Constants;
using static Godot.DisplayServer;

namespace UISystem;
/// <summary>
/// methods to set properties are not marked as static so that only classes with access to instance can change them
/// </summary>
public class GameSettings
{

    public static event Action<float> OnMusicVolumeChanged;
    public static event Action<float> OnSfxVolumeChanged;
    public static event Action<ControllerIconsType> OnControllerIconsChanged;

    public static float CurrentMusicVolume { get; private set; } = ConfigData.DefaultMusicVolume;
    public static float CurrentSfxVolume { get; private set; } = ConfigData.DefaultSfxVolume;
    public static Vector2I CurrentResolution { get; private set; } = ConfigData.DefaultResolution;
    public static WindowMode CurrentWindowMode { get; private set; } = ConfigData.DefaultWindowMode;
    public static ControllerIconsType CurrentControllerIconsType { get; private set; } = ConfigData.DefaultControllerIconsType;

    public GameSettings(ConfigFile config, Error err)
    {
        if (err != Error.Ok)
        {
            SaveDefaultSettings(config);
        }
    }

    public void SetMusicVolume(float volume)
    {
        CurrentMusicVolume = volume;
        OnMusicVolumeChanged?.Invoke(volume);
    }

    public void SetSfxVolume(float volume)
    {
        CurrentSfxVolume = volume;
        OnSfxVolumeChanged?.Invoke(volume);
    }

    public void SetControllerIconsType(ControllerIconsType type)
    {
        CurrentControllerIconsType = type;
        OnControllerIconsChanged?.Invoke(type);
    }

    private static void SaveDefaultSettings(ConfigFile config)
    {
        config.SetValue(ConfigData.AudioSectionName, ConfigData.MusicVolumeKey, ConfigData.DefaultMusicVolume);
        config.SetValue(ConfigData.AudioSectionName, ConfigData.SfxVolumeKey, ConfigData.DefaultSfxVolume);

        config.SetValue(ConfigData.VideoSectionName, ConfigData.ResolutionKey, ConfigData.DefaultResolution);
        config.SetValue(ConfigData.VideoSectionName, ConfigData.WindowModeKey, (int)ConfigData.DefaultWindowMode);

        config.SetValue(ConfigData.InterfaceSectionName, ConfigData.ControllerIconsKey, (int)ConfigData.DefaultControllerIconsType);
        
        config.Save(ConfigData.ConfigLocation);
    }

}
