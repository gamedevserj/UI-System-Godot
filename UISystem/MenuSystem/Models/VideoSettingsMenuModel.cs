using Godot;
using UISystem.Constants;
using UISystem.MenuSystem.Interfaces;
using static Godot.DisplayServer;

namespace UISystem.MenuSystem.Models;
public class VideoSettingsMenuModel : IMenuModel
{

    private Vector2I _currentResolution;
    private Vector2I _tempResolution;
    private WindowMode _currentWindowMode;
    private WindowMode _tempWindowMode;
    private readonly ConfigFile _config;

    public int CurrentResolutionIndex { get; private set; }
    public int CurrenWindowModeIndex { get; private set; }
    public bool HasUnappliedSettings => _currentResolution != _tempResolution || _currentWindowMode != _tempWindowMode;
    private float Aspect => (float)ScreenGetSize().X / ScreenGetSize().Y;

    public VideoSettingsMenuModel(ConfigFile config)
    {
        _config = config;
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

    public WindowMode[] GetWindowModeOptionNames()
    {
        return VideoSettings.WindowModeOptions;
    }

    public string[] GetAvailableResolutionNames()
    {
        return VideoSettings.GetResolutionsNamesForAspect(Aspect);
    }

    public void SaveSettings()
    {
        _currentResolution = _tempResolution;
        _currentWindowMode = _tempWindowMode;
        _config.SetValue(ConfigData.VideoSectionName, ConfigData.ResolutionKey, _currentResolution);
        _config.SetValue(ConfigData.VideoSectionName, ConfigData.WindowModeKey, (int)_currentWindowMode);
        _config.Save(ConfigData.ConfigLocation);
    }

    public void ResetToDefault()
    {
        _currentResolution = _tempResolution = ConfigData.DefaultResolution;
        _currentWindowMode = _tempWindowMode = ConfigData.DefaultWindowMode;
        SaveSettings();
        SetResolution(_currentResolution);
        SetWindowMode( _currentWindowMode);
    }

    private Vector2I[] GetAvailableResolutions()
    {
        return VideoSettings.GetResolutionsForAspect(Aspect);
    }

    private void LoadSettings()
    {
        _currentResolution = _tempResolution = 
            (Vector2I)_config.GetValue(ConfigData.VideoSectionName, ConfigData.ResolutionKey, ConfigData.DefaultResolution);
        _currentWindowMode = _tempWindowMode = 
            (WindowMode)(int)_config.GetValue(ConfigData.VideoSectionName, ConfigData.WindowModeKey, (int)ConfigData.DefaultWindowMode);
        SetResolution(_currentResolution);
        SetWindowMode(_currentWindowMode);
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
