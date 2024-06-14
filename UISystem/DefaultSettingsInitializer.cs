using Godot;
using UISystem.Constants;

namespace UISystem;
public class DefaultSettingsInitializer
{
    public static void SaveDefaultSettings(ConfigFile config)
    {
        config.SetValue(ConfigData.AudioSectionName, ConfigData.MusicVolumeKey, ConfigData.DefaultMusicVolume);
        config.SetValue(ConfigData.AudioSectionName, ConfigData.SfxVolumeKey, ConfigData.DefaultSfxVolume);

        config.SetValue(ConfigData.VideoSectionName, ConfigData.ResolutionKey, ConfigData.DefaultResolution);
        config.SetValue(ConfigData.VideoSectionName, ConfigData.WindowModeKey, (int)ConfigData.DefaultWindowMode);
        config.Save(ConfigData.ConfigLocation);
    }
}
