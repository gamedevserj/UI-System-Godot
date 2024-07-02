using UISystem.Constants;
using UISystem.MenuSystem.Interfaces;

namespace UISystem.MenuSystem.Models;
public class AudioSettingsMenuModel : IMenuModel
{

    private float _tempMusicVolume;
    private float _tempSfxVolume;

    private readonly GameSettings _settings;

    public bool HasUnappliedSettings => MusicVolume != _tempMusicVolume || SfxVolume != _tempSfxVolume;
    public float MusicVolume { get => GameSettings.MusicVolume; set => _tempMusicVolume = value; }
    public float SfxVolume { get => GameSettings.SfxVolume; set => _tempSfxVolume = value; }

    public AudioSettingsMenuModel(GameSettings settings)
    {
        _settings = settings;
        LoadSettings();
    }

    public void ResetToDefault()
    {
        _tempMusicVolume = ConfigData.DefaultMusicVolume;
        _tempSfxVolume = ConfigData.DefaultSfxVolume;
        SaveSettings();
    }

    public void SaveSettings()
    {
        _settings.SetMusicVolume(_tempMusicVolume);
        _settings.SetSfxVolume(_tempSfxVolume);
        _settings.Save();
    }

    public void DiscardChanges()
    {
        _tempMusicVolume = GameSettings.MusicVolume;
        _tempSfxVolume = GameSettings.SfxVolume;
    }

    private void LoadSettings()
    {
        _tempMusicVolume = GameSettings.MusicVolume;
        _tempSfxVolume = GameSettings.SfxVolume;
    }

}
