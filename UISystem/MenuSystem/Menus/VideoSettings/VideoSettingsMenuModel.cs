using Godot;
using UISystem.Constants;
using UISystem.MenuSystem.SettingsMenu.Interfaces;
using static Godot.DisplayServer;

namespace UISystem.MenuSystem.Models;
public class VideoSettingsMenuModel : ISettingsMenuModel
{

    private Vector2I _tempResolution;
    private WindowMode _tempWindowMode;
    private readonly GameSettings _settings;

    public int CurrentResolutionIndex { get; private set; }
    public int CurrenWindowModeIndex { get; private set; }
    public bool HasUnappliedSettings => GameSettings.Resolution != _tempResolution || GameSettings.WindowMode != _tempWindowMode;

    private static float Aspect => (float)ScreenGetSize().X / ScreenGetSize().Y;

    public VideoSettingsMenuModel(GameSettings settings)
    {
        _settings = settings;
        LoadSettings();
    }

    public void SelectWindowMode(int index)
    {
        _tempWindowMode = VideoSettings.WindowModeOptions[index];
        SetWindowMode(_tempWindowMode);
    }

    public void SelectResolution(int index)
    {
        _tempResolution = GetAvailableResolutions()[index];
        SetResolution(_tempResolution);
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
        _settings.SetResolution(_tempResolution);
        _settings.SetWindowMode(_tempWindowMode);
        _settings.Save();
    }

    public void DiscardChanges()
    {
        LoadSettings();
    }

    public void ResetToDefault()
    {
        _tempResolution = ConfigData.DefaultResolution;
        _tempWindowMode = ConfigData.DefaultWindowMode;
        SaveSettings();
        SetResolution(_tempResolution);
        SetWindowMode(_tempWindowMode);
    }

    private static Vector2I[] GetAvailableResolutions()
    {
        return VideoSettings.GetResolutionsForAspect(Aspect);
    }

    private void LoadSettings()
    {
        _tempResolution = GameSettings.Resolution;
        _tempWindowMode = GameSettings.WindowMode;
        SetResolution(_tempResolution);
        SetWindowMode(_tempWindowMode);
    }

    private void SetResolution(Vector2I resolution)
    {
        CurrentResolutionIndex = VideoSettings.GetResolutionIndex(resolution, GetAvailableResolutions());
        WindowSetSize(resolution);
    }

    private void SetWindowMode(WindowMode mode)
    {
        CurrenWindowModeIndex = VideoSettings.GetWindwoModeIndex(mode);
        WindowSetMode(mode);

        // if you change resolution in fullscreen, then change window mode - window will not have the resolution that was selected
        // this is to prevent that
        if (mode == WindowMode.Windowed)
            WindowSetSize(_tempResolution);
    }

}
