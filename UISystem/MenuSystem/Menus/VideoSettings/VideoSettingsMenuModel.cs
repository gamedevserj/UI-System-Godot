using Godot;
using UISystem.Constants;
using UISystem.Core.MenuSystem;
using static Godot.DisplayServer;

namespace UISystem.MenuSystem.Models;

/// <summary>
/// Video settings menu model.
/// </summary>
public class VideoSettingsMenuModel : ISettingsMenuModel
{
    private readonly GameSettings _settings;

    private Vector2I _lastResolution;
    private WindowMode _lastWindowMode;

    /// <summary>
    /// Initializes a new instance of the <see cref="VideoSettingsMenuModel"/> class.
    /// </summary>
    /// <param name="settings">Game settings.</param>
    public VideoSettingsMenuModel(GameSettings settings)
    {
        _settings = settings;
        SetVideoParameters();
        RememberLastSavedSettings();
    }

    /// <summary>
    /// Gets resolution names for resolutions available for the current screen aspect.
    /// </summary>
    public static string[] AvailableResolutionNames => VideoSettings.GetResolutionsNamesForAspect(Aspect);

    /// <summary>
    /// Gets window mode option names.
    /// </summary>
    public static string[] WindowModeOptionNames => VideoSettings.WindowModeNames;

    /// <summary>
    /// Gets index of current resolution in the list of available resolutions. -1 if resolution is not found in the list.
    /// </summary>
    public int CurrentResolutionIndex => VideoSettings.GetResolutionIndex(_settings.Resolution, GetAvailableResolutions());

    /// <summary>
    /// Gets index of current window mode in the list of available modes. -1 if mode is not found in the list.
    /// </summary>
    public int CurrenWindowModeIndex => VideoSettings.GetWindwoModeIndex(_settings.WindowMode);

    /// <inheritdoc/>
    public bool HasUnappliedSettings => CurrentWindowSize != _lastResolution || _settings.WindowMode != _lastWindowMode;

    private static float Aspect => (float)ScreenGetSize().X / ScreenGetSize().Y;

    private static Vector2I CurrentWindowSize => WindowGetSize(); // to allow saving resized window

    /// <summary>
    /// Selects window mode.
    /// </summary>
    /// <param name="index">Window mode index.</param>
    public void SelectWindowMode(int index) => SetWindowMode(VideoSettings.WindowModeOptions[index]);

    /// <summary>
    /// Selects resolution.
    /// </summary>
    /// <param name="index">Resolution index.</param>
    public void SelectResolution(int index) => SetResolution(GetAvailableResolutions()[index]);

    /// <inheritdoc/>
    public void SaveSettings()
    {
        if (_settings.WindowMode == WindowMode.Windowed)
            _settings.Resolution = CurrentWindowSize;

        RememberLastSavedSettings();
        _settings.SaveVideoSettings();
    }

    /// <inheritdoc/>
    public void DiscardChanges()
    {
        _settings.Resolution = _lastResolution;
        _settings.WindowMode = _lastWindowMode;
        SetVideoParameters();
    }

    /// <inheritdoc/>
    public void ResetToDefault()
    {
        _settings.Resolution = ConfigData.DefaultResolution;
        _settings.WindowMode = ConfigData.DefaultWindowMode;
        SetVideoParameters();
        SaveSettings();
    }

    private static Vector2I[] GetAvailableResolutions() => VideoSettings.GetResolutionsForAspect(Aspect);

    private void SetVideoParameters()
    {
        SetResolution(_settings.Resolution);
        SetWindowMode(_settings.WindowMode);
    }

    private void SetResolution(Vector2I resolution)
    {
        _settings.Resolution = resolution;
        WindowSetSize(resolution);
    }

    private void SetWindowMode(WindowMode mode)
    {
        _settings.WindowMode = mode;
        WindowSetMode(mode);

        // if you change resolution in fullscreen, then change window mode - window will not have the resolution that was selected
        // this is to prevent that
        if (mode == WindowMode.Windowed)
            WindowSetSize(_settings.Resolution);
    }

    private void RememberLastSavedSettings()
    {
        _lastResolution = _settings.Resolution;
        _lastWindowMode = _settings.WindowMode;
    }
}
