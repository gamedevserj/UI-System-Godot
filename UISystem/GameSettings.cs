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

    private readonly ConfigFile _config;

    public static float MusicVolume { get; private set; } = ConfigData.DefaultMusicVolume;
    public static float SfxVolume { get; private set; } = ConfigData.DefaultSfxVolume;
    public static Vector2I Resolution { get; private set; } = ConfigData.DefaultResolution;
    public static WindowMode WindowMode { get; private set; } = ConfigData.DefaultWindowMode;
    public static ControllerIconsType ControllerIconsType { get; private set; } = ConfigData.DefaultControllerIconsType;


    public GameSettings(ConfigFile config, Error err)
    {
        _config = config;
        if (err != Error.Ok)
        {
            SaveDefaultSettings(_config);
        }
        else
        {
            LoadSettings(_config);
        }
    }

    public void SetMusicVolume(float volume)
    {
        MusicVolume = volume;
        _config.SetValue(ConfigData.AudioSectionName, ConfigData.MusicVolumeKey, volume);
        OnMusicVolumeChanged?.Invoke(volume);
    }

    public void SetSfxVolume(float volume)
    {
        SfxVolume = volume;
        _config.SetValue(ConfigData.AudioSectionName, ConfigData.SfxVolumeKey, volume);
        OnSfxVolumeChanged?.Invoke(volume);
    }

    public void SetControllerIconsType(ControllerIconsType type)
    {
        ControllerIconsType = type;
        _config.SetValue(ConfigData.InterfaceSectionName, ConfigData.ControllerIconsKey, (int)type);
        OnControllerIconsChanged?.Invoke(type);
    }

    public void SetResolution(Vector2I resolution)
    {
        Resolution = resolution;
        _config.SetValue(ConfigData.VideoSectionName, ConfigData.ResolutionKey, resolution);
    }

    public void SetWindowMode(WindowMode windowMode)
    {
        WindowMode = windowMode;
        _config.SetValue(ConfigData.VideoSectionName, ConfigData.WindowModeKey, (int)windowMode);
    }

    public void Save()
    {
        _config.Save(ConfigData.ConfigLocation);
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

    private static void LoadSettings(ConfigFile config)
    {
        MusicVolume = (float)config.GetValue(ConfigData.AudioSectionName, ConfigData.MusicVolumeKey, ConfigData.DefaultMusicVolume);
        SfxVolume = (float)config.GetValue(ConfigData.AudioSectionName, ConfigData.SfxVolumeKey, ConfigData.DefaultSfxVolume);

        Resolution = (Vector2I)config.GetValue(ConfigData.VideoSectionName, ConfigData.ResolutionKey, ConfigData.DefaultResolution);
        WindowMode = (WindowMode)(int)config.GetValue(ConfigData.VideoSectionName, ConfigData.WindowModeKey, (int)ConfigData.DefaultWindowMode);

        ControllerIconsType = (ControllerIconsType)(int)config.GetValue(ConfigData.InterfaceSectionName, ConfigData.ControllerIconsKey, (int)ConfigData.DefaultControllerIconsType);
    }

}
