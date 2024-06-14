using Godot;
using System;
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

    public static readonly string[] ResolutionNames16x9 = new string[]
    {
        "640x360",
        "854x480",
        "960x540",
        "1280x720",
        "1366x768",
        "1600x900",
        "1920x1080",
        "2560x1440",
        "3200x1800",
        "3840x2160",
    };

    public static readonly Vector2I[] Resolutions16x9 = new Vector2I[]
    {
        new (640, 360),
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

    public static readonly string[] ResolutionNames16x10 = new string[]
    {
        "1280x800",
        "1440x900",
        "1680x1050",
        "1920x1200",
        "2560x1600",
        "3840x2400",
    };

    public static readonly Vector2I[] Resolutions16x10 = new Vector2I[]
    {
        new (1280, 800),
        new (1440, 900),
        new (1680, 1050),
        new (1920, 1200),
        new (2560, 1600),
        new (3840, 2400),
    };

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

}
