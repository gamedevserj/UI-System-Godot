using UISystem.Constants;
using UISystem.Core.MenuSystem;

namespace UISystem.MenuSystem.Models;
public class AudioSettingsMenuModel : ISettingsMenuModel
{

    private float _lastMusicVolume;
    private float _lastSfxVolume;

    private readonly GameSettings _settings;

    public bool HasUnappliedSettings => MusicVolume != _lastMusicVolume || SfxVolume != _lastSfxVolume;
    public float MusicVolume { get => _settings.MusicVolume; set => _settings.MusicVolume = value; }
    public float SfxVolume { get => _settings.SfxVolume; set => _settings.SfxVolume = value; }

    public AudioSettingsMenuModel(GameSettings settings)
    {
        _settings = settings;
        RememberLastSavedSettings();
    }

    public void ResetToDefault()
    {
        MusicVolume = ConfigData.DefaultMusicVolume;
        SfxVolume = ConfigData.DefaultSfxVolume;
        SaveSettings();
    }

    public void SaveSettings()
    {
        RememberLastSavedSettings();
        _settings.SaveAudioSettings();
    }

    public void DiscardChanges()
    {
        MusicVolume = _lastMusicVolume;
        SfxVolume = _lastSfxVolume;
    }

    private void RememberLastSavedSettings()
    {
        _lastMusicVolume = MusicVolume;
        _lastSfxVolume = SfxVolume;
    }

}
