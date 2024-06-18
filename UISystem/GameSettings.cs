using Godot;
using UISystem.Common.Enums;
using UISystem.Constants;
using UISystem.MenuSystem.Models;
using static Godot.DisplayServer;

namespace UISystem;
public static class GameSettings
{

    public static float CurrentMusicVolume { get; private set; } = ConfigData.DefaultMusicVolume;
    public static float CurrentSfxVolume { get; private set; } = ConfigData.DefaultSfxVolume;
    public static Vector2I CurrentResolution { get; private set; } = ConfigData.DefaultResolution;
    public static WindowMode CurrentWindowMode { get; private set; } = ConfigData.DefaultWindowMode;
    public static ControllerIconsType CurrentControllerIconsType { get; private set; } = ConfigData.DefaultControllerIconsType;

    static GameSettings()
    {
        InterfaceSettingsMenuModel.OnControllerIconsTypeChange += (type) => CurrentControllerIconsType = type;
    }

    public static void SaveDefaultSettings(ConfigFile config)
    {
        config.SetValue(ConfigData.AudioSectionName, ConfigData.MusicVolumeKey, ConfigData.DefaultMusicVolume);
        config.SetValue(ConfigData.AudioSectionName, ConfigData.SfxVolumeKey, ConfigData.DefaultSfxVolume);

        config.SetValue(ConfigData.VideoSectionName, ConfigData.ResolutionKey, ConfigData.DefaultResolution);
        config.SetValue(ConfigData.VideoSectionName, ConfigData.WindowModeKey, (int)ConfigData.DefaultWindowMode);

        config.SetValue(ConfigData.InterfaceSectionName, ConfigData.ControllerIconsKey, (int)ConfigData.DefaultControllerIconsType);
        
        config.Save(ConfigData.ConfigLocation);
    }

}
