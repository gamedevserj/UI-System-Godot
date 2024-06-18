using Godot;
using UISystem.Constants;
using UISystem.MenuSystem.Interfaces;

namespace UISystem.MenuSystem.Models;
public class AudioSettingsMenuModel : IMenuModel
{

    private float _musicVolume;
    private float _sfxVolume;
    private float _tempMusicVolume;
    private float _tempSfxVolume;

    private readonly ConfigFile _config;

    public bool HasUnappliedSettings => _musicVolume != _tempMusicVolume || _sfxVolume != _tempSfxVolume;
    public float MusicVolume { get => _musicVolume; set => _tempMusicVolume = value; }
    public float SfxVolume { get => _sfxVolume; set => _tempSfxVolume = value; }

    private static string ConfigLocation => ConfigData.ConfigLocation;
    private static string SectionName => ConfigData.AudioSectionName;
    private static string MusicVolumeKey => ConfigData.MusicVolumeKey;
    private static string SfxVolumeKey => ConfigData.SfxVolumeKey;
    private static float DefaultMusicVolume => ConfigData.DefaultMusicVolume;
    private static float DefaultSfxVolume => ConfigData.DefaultSfxVolume;

    public AudioSettingsMenuModel(ConfigFile config)
    {
        _config = config;
        LoadSettings();
    }

    public void ResetToDefault()
    {
        _musicVolume = _tempMusicVolume = DefaultMusicVolume;
        _sfxVolume = _tempSfxVolume = DefaultSfxVolume;
        SaveSettings();
    }

    public void SaveSettings()
    {
        _musicVolume = _tempMusicVolume;
        _sfxVolume = _tempSfxVolume;
        SaveToConfig();
    }

    private void LoadSettings()
    {
        _musicVolume = _tempMusicVolume = (float)_config.GetValue(SectionName, MusicVolumeKey, DefaultMusicVolume);
        _sfxVolume = _tempSfxVolume = (float)_config.GetValue(SectionName, SfxVolumeKey, DefaultSfxVolume);
    }

    private void SaveToConfig()
    {
        _config.SetValue(SectionName, MusicVolumeKey, _musicVolume);
        _config.SetValue(SectionName, SfxVolumeKey, _sfxVolume);
        _config.Save(ConfigLocation);
    }

}
