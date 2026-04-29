using System.Collections.Generic;
using Godot;

namespace UISystem.Common.Constants;

/// <summary>
/// Class containging mouse icon names.
/// </summary>
public static class MouseIcons
{
    private static readonly Dictionary<MouseButton, string> _keys = new()
        {
            { MouseButton.Left, "mouse_left.png" },
            { MouseButton.Right, "mouse_right.png" },
            { MouseButton.Middle, "mouse_scroll.png" },
            { MouseButton.WheelUp, "mouse_scroll_up.png" },
            { MouseButton.WheelDown, "mouse_scroll_down.png" },
        };

    private static string ItemsFolder => "res://UISystem/Textures/Inputs/Keyboard/";

    /// <summary>
    /// Gets the mouse button icon.
    /// </summary>
    /// <param name="button">Mouse button.</param>
    /// <returns>Path to the icon image.</returns>
    public static string GetIcon(MouseButton button)
    {
        return ItemsFolder + _keys[button];
    }
}
