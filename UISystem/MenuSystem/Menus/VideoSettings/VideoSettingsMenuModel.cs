using Godot;
using UISystem.Constants;
using UISystem.Core.MenuSystem;
using static Godot.DisplayServer;

namespace UISystem.MenuSystem.Models;
public class VideoSettingsMenuModel : ISettingsMenuModel
{

    private Vector2I _tempResolution;
    private WindowMode _tempWindowMode;

    private Vector2I _lastResolution;
    private WindowMode _lastWindowMode;

    private readonly GameSettings _settings;

    public int CurrentResolutionIndex => VideoSettings.GetResolutionIndex(_settings.Resolution, GetAvailableResolutions());
    public int CurrenWindowModeIndex => VideoSettings.GetWindwoModeIndex(_settings.WindowMode);
    public bool HasUnappliedSettings => CurrentWindowSize != _lastResolution || _settings.WindowMode != _lastWindowMode;

    private static float Aspect => (float)ScreenGetSize().X / ScreenGetSize().Y;
    private static Vector2I CurrentWindowSize => WindowGetSize(); // to allow saving resized window

    public VideoSettingsMenuModel(GameSettings settings)
    {
        _settings = settings;
        SetVideoParameters();
        RememberLastSavedSettings();
    }

    public void SelectWindowMode(int index)
    {
        SetWindowMode(VideoSettings.WindowModeOptions[index]);
    }

    public void SelectResolution(int index)
    {
        SetResolution(GetAvailableResolutions()[index]);
    }

    public string[] GetWindowModeOptionNames()
    {
        return VideoSettings.WindowModeNames;
    }

    public string[] GetAvailableResolutionNames()
    {
        return VideoSettings.GetResolutionsNamesForAspect(Aspect);
    }

    public void SaveSettings()
    {
        if (_settings.WindowMode == WindowMode.Windowed)
            _settings.Resolution = CurrentWindowSize;

        RememberLastSavedSettings();
        _settings.SaveVideoSettings();
    }

    public void DiscardChanges()
    {
        _settings.Resolution = _lastResolution;
        _settings.WindowMode = _lastWindowMode;
        SetVideoParameters();
    }

    public void ResetToDefault()
    {
        _settings.Resolution = ConfigData.DefaultResolution;
        _settings.WindowMode = ConfigData.DefaultWindowMode;
        SetVideoParameters();
        SaveSettings();
    }

    private void SetVideoParameters()
    {
        SetResolution(_settings.Resolution);
        SetWindowMode(_settings.WindowMode);
    }

    private static Vector2I[] GetAvailableResolutions()
    {
        return VideoSettings.GetResolutionsForAspect(Aspect);
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
