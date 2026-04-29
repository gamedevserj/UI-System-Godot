using System;
using System.Text.RegularExpressions;
using Godot;
using static Godot.DisplayServer;

namespace UISystem.Constants;

/// <summary>
/// Class containing video settings.
/// </summary>
public static class VideoSettings
{
    private static readonly WindowMode[] _windowModeOptions = new WindowMode[]
    {
        WindowMode.ExclusiveFullscreen,
        WindowMode.Fullscreen,
        WindowMode.Windowed,
        WindowMode.Maximized,
    };

    private static readonly string[] _windowModeNames;

    private static readonly Vector2I[] _resolutions16x9 = new Vector2I[]
    {
        new(854, 480),
        new(960, 540),
        new(1280, 720),
        new(1366, 768),
        new(1600, 900),
        new(1920, 1080),
        new(2560, 1440),
        new(3200, 1800),
        new(3840, 2160),
    };

    private static readonly string[] _resolutionNames16x9;

    private static readonly Vector2I[] _resolutions16x10 = new Vector2I[]
    {
        new(1280, 800),
        new(1440, 900),
        new(1680, 1050),
        new(1920, 1200),
        new(2560, 1600),
        new(3840, 2400),
    };

    private static readonly string[] _resolutionNames16x10;

    static VideoSettings()
    {
        _resolutionNames16x9 = new string[_resolutions16x9.Length];
        for (int i = 0; i < _resolutions16x9.Length; i++)
        {
            _resolutionNames16x9[i] = GetResolutionName(_resolutions16x9[i]);
        }

        _resolutionNames16x10 = new string[_resolutions16x10.Length];
        for (int i = 0; i < _resolutions16x10.Length; i++)
        {
            _resolutionNames16x10[i] = GetResolutionName(_resolutions16x10[i]);
        }

        _windowModeNames = new string[_windowModeOptions.Length];
        for (int i = 0; i < _windowModeOptions.Length; i++)
        {
            _windowModeNames[i] = Regex.Replace(_windowModeOptions[i].ToString(), "([A-Z])", " $1").Trim(); // to have space in ExclusiveFullscreen
        }
    }

    /// <summary>
    /// Gets window mode options.
    /// </summary>
    public static WindowMode[] WindowModeOptions => _windowModeOptions;

    /// <summary>
    /// Gets window mode names.
    /// </summary>
    public static string[] WindowModeNames => _windowModeNames;

    /// <summary>
    /// Gets resolutions available for the aspect.
    /// </summary>
    /// <param name="aspect">Target aspect.</param>
    /// <returns>Array of resolutions available for the aspect.</returns>
    public static Vector2I[] GetResolutionsForAspect(double aspect)
    {
        if (Mathf.IsEqualApprox(aspect, 1.77f))
            return _resolutions16x9;
        if (Mathf.IsEqualApprox(aspect, 1.6f))
            return _resolutions16x10;

        return _resolutions16x9;
    }

    /// <summary>
    /// Gets resolution names for the aspects.
    /// </summary>
    /// <param name="aspect">Target aspect.</param>
    /// <returns>Array of formatted resolution names.</returns>
    public static string[] GetResolutionsNamesForAspect(double aspect)
    {
        if (Mathf.IsEqualApprox(aspect, 1.77f))
            return _resolutionNames16x9;
        if (Mathf.IsEqualApprox(aspect, 1.6f))
            return _resolutionNames16x10;

        return _resolutionNames16x9;
    }

    /// <summary>
    /// Gets index of the resolution.
    /// </summary>
    /// <param name="resolution">Target resolution.</param>
    /// <param name="allResolutions">Available resolutions.</param>
    /// <returns>Index of the resolution in the array, -1 if array does not contain resolution.</returns>
    public static int GetResolutionIndex(Vector2I resolution, Vector2I[] allResolutions)
    {
        return Array.IndexOf(allResolutions, resolution);
    }

    /// <summary>
    /// Gets index of the window mode.
    /// </summary>
    /// <param name="mode">Target window mode.</param>
    /// <returns>Index of the window mode, -1 if window mode was not found.</returns>
    public static int GetWindwoModeIndex(WindowMode mode)
    {
        return Array.IndexOf(_windowModeOptions, mode);
    }

    private static string GetResolutionName(Vector2I resolution)
    {
        return resolution.X + "x" + resolution.Y;
    }
}
