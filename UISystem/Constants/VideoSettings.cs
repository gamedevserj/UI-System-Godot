using Godot;
using System;
using System.Text.RegularExpressions;
using static Godot.DisplayServer;

namespace UISystem.Constants;
public static class VideoSettings
{

    public static readonly WindowMode[] WindowModeOptions = new WindowMode[]
    {
        WindowMode.ExclusiveFullscreen,
        WindowMode.Fullscreen,
        WindowMode.Windowed,
        WindowMode.Maximized,
    };
    public static readonly string[] WindowModeNames;

    private static readonly Vector2I[] Resolutions16x9 = new Vector2I[]
    {

        new (854, 480),
        new (960, 540),
        new (1280, 720),
        new (1366, 768),
        new(1600, 900),
        new(1920, 1080),
        new(2560, 1440),
        new(3200, 1800),
        new(3840, 2160),
    };
    private static readonly string[] ResolutionNames16x9;

    private static readonly Vector2I[] Resolutions16x10 = new Vector2I[]
    {
        new (1280, 800),
        new (1440, 900),
        new (1680, 1050),
        new (1920, 1200),
        new (2560, 1600),
        new (3840, 2400),
    };
    private static readonly string[] ResolutionNames16x10;

    static VideoSettings()
    {
        ResolutionNames16x9 = new string[Resolutions16x9.Length];
        for (int i = 0; i < Resolutions16x9.Length; i++)
        {
            ResolutionNames16x9[i] = GetResolutionName(Resolutions16x9[i]);
        }

        ResolutionNames16x10 = new string[Resolutions16x10.Length];
        for (int i = 0; i < Resolutions16x10.Length; i++)
        {
            ResolutionNames16x10[i] = GetResolutionName(Resolutions16x10[i]);
        }

        WindowModeNames = new string[WindowModeOptions.Length];
        for (int i = 0; i < WindowModeOptions.Length; i++)
        {
            WindowModeNames[i] = Regex.Replace(WindowModeOptions[i].ToString(), "([A-Z])", " $1").Trim(); // to have space in ExclusiveFullscreen
        }
    }

    public static Vector2I[] GetResolutionsForAspect(double aspect)
    {
        if (Mathf.IsEqualApprox(aspect, 1.77f))
            return Resolutions16x9;
        if (Mathf.IsEqualApprox(aspect, 1.6f))
            return Resolutions16x10;

        return Resolutions16x9;
    }

    public static string[] GetResolutionsNamesForAspect(double aspect)
    {
        if (Mathf.IsEqualApprox(aspect, 1.77f))
            return ResolutionNames16x9;
        if (Mathf.IsEqualApprox(aspect, 1.6f))
            return ResolutionNames16x10;

        return ResolutionNames16x9;
    }

    public static int GetResolutionIndex(Vector2I resolution, Vector2I[] allResolutions)
    {
        return Array.IndexOf(allResolutions, resolution);
    }

    public static int GetWindwoModeIndex(WindowMode mode)
    {
        return Array.IndexOf(WindowModeOptions, mode);
    }

    private static string GetResolutionName(Vector2I resolution)
    {
        return resolution.X + "x" + resolution.Y;
    }

}
