using Godot;
using UISystem.Common.Enums;
using static Godot.DisplayServer;

namespace UISystem.Constants;

// Suppressing jsut to keep elements grouped by sections.
#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable SA1201 // Elements should appear in the correct order
#pragma warning disable SA1516 // Elements should be separated by blank line
/// <summary>
/// Class containing config keys and default values.
/// </summary>
public static class ConfigData
{
    public const string ConfigLocation = "user://Config.cfg";

    public const string AudioSectionName = "AudioSettings";
    public const string MusicVolumeKey = "MusicVolume";
    public const float DefaultMusicVolume = 0.5f;
    public const string SfxVolumeKey = "SfxVolume";
    public const float DefaultSfxVolume = 0.5f;

    public const string VideoSectionName = "VideoSettings";
    public const string ResolutionKey = "Resolution";
    public static Vector2I DefaultResolution => ScreenGetSize();
    public const string WindowModeKey = "WindowMode";
    public const WindowMode DefaultWindowMode = WindowMode.ExclusiveFullscreen;

    public const string KeysSectionName = "Keys";

    public const string InterfaceSectionName = "Interface";
    public const string ControllerIconsKey = "ControllerIcons";
    public const ControllerIconsType DefaultControllerIconsType = ControllerIconsType.Xbox;
}
#pragma warning restore SA1600 // Elements should be documented
#pragma warning restore SA1201 // Elements should appear in the correct order
#pragma warning restore SA1516 // Elements should be separated by blank line
