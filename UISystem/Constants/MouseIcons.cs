using Godot;
using System.Collections.Generic;

namespace UISystem.Common.Constants;
public static class MouseIcons
{
    private static string ItemsFolder => "res://UISystem/Textures/Inputs/Keyboard/";
    private static readonly Dictionary<MouseButton, string> _keys;

    static MouseIcons()
    {
        _keys = new Dictionary<MouseButton, string>()
        {
            {MouseButton.Left, "mouse_left.png" },
            {MouseButton.Right, "mouse_right.png" },
            {MouseButton.Middle, "mouse_scroll.png" },
            {MouseButton.WheelUp, "mouse_scroll_up.png" },
            {MouseButton.WheelDown, "mouse_scroll_down.png" },
        };
    }

    public static string GetIcon(MouseButton button)
    {
        return ItemsFolder + _keys[button];
    }

}
