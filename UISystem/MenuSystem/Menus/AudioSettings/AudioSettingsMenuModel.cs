using Godot;
using UISystem.Constants;
using UISystem.Core.MenuSystem;

namespace UISystem.MenuSystem.Models;

/// <summary>
/// Audio settings menu model.
/// </summary>
public class AudioSettingsMenuModel : ISettingsMenuModel
{
    private readonly GameSettings _settings;

    private float _lastMusicVolume;
    private float _lastSfxVolume;

    /// <summary>
    /// Initializes a new instance of the <see cref="AudioSettingsMenuModel"/> class.
    /// </summary>
    /// <param name="settings">Game settings.</param>
    public AudioSettingsMenuModel(GameSettings settings)
    {
        _settings = settings;
        RememberLastSavedSettings();
    }

    /// <inheritdoc/>
    public bool HasUnappliedSettings => !Mathf.IsEqualApprox(MusicVolume, _lastMusicVolume)
        || !Mathf.IsEqualApprox(SfxVolume, _lastSfxVolume);

    /// <summary>
    /// Gets or sets music volume.
    /// </summary>
    public float MusicVolume { get => _settings.MusicVolume; set => _settings.MusicVolume = value; }

    /// <summary>
    /// Gets or sets SFX volume.
    /// </summary>
    public float SfxVolume { get => _settings.SfxVolume; set => _settings.SfxVolume = value; }

    /// <inheritdoc/>
    public void ResetToDefault()
    {
        MusicVolume = ConfigData.DefaultMusicVolume;
        SfxVolume = ConfigData.DefaultSfxVolume;
        SaveSettings();
    }

    /// <inheritdoc/>
    public void SaveSettings()
    {
        RememberLastSavedSettings();
        _settings.SaveAudioSettings();
    }

    /// <inheritdoc/>
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
