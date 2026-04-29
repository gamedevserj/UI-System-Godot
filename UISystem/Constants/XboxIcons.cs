using System.Collections.Generic;
using Godot;

namespace UISystem.Common.Constants;

/// <summary>
/// Class containing paths to Xbox icons.
/// </summary>
public static class XboxIcons
{
    private static readonly Dictionary<JoyButton, string> _buttons = new Dictionary<JoyButton, string>
        {
            { JoyButton.DpadLeft, "xbox_dpad_left.png" },
            { JoyButton.DpadUp, "xbox_dpad_up.png" },
            { JoyButton.DpadRight, "xbox_dpad_right.png" },
            { JoyButton.DpadDown, "xbox_dpad_down.png" },
            { JoyButton.A, "xbox_button_color_a.png" },
            { JoyButton.B, "xbox_button_color_b.png" },
            { JoyButton.X, "xbox_button_color_x.png" },
            { JoyButton.Y, "xbox_button_color_y.png" },
            { JoyButton.LeftShoulder, "xbox_lb.png" }, // L1
            { JoyButton.RightShoulder, "xbox_rb.png" }, // R1
            { JoyButton.Misc1, "xbox_button_share.png" }, // share
            { JoyButton.Start, "xbox_button_menu.png" }, // options
            { JoyButton.LeftStick, "xbox_ls.png" }, // L3
            { JoyButton.RightStick, "xbox_rs.png" }, // R3
        };

    private static readonly Dictionary<JoyAxis, string> _triggersPositive = new Dictionary<JoyAxis, string>
        {
            { JoyAxis.TriggerLeft, "xbox_lt.png" },
            { JoyAxis.TriggerRight, "xbox_rt.png" },
            { JoyAxis.LeftX, "xbox_stick_l_right.png" },
            { JoyAxis.LeftY, "xbox_stick_l_down.png" },
            { JoyAxis.RightX, "xbox_stick_r_right.png" },
            { JoyAxis.RightY, "xbox_stick_r_down.png" },
        };

    private static readonly Dictionary<JoyAxis, string> _triggersNegative = new Dictionary<JoyAxis, string>
        {
            { JoyAxis.LeftX, "xbox_stick_l_left.png" },
            { JoyAxis.LeftY, "xbox_stick_l_up.png" },
            { JoyAxis.RightX, "xbox_stick_r_left.png" },
            { JoyAxis.RightY, "xbox_stick_r_up.png" },
        };

    private static string ItemsFolder => "res://UISystem/Textures/Inputs/Xbox/";

    /// <summary>
    /// Gets buttons icon.
    /// </summary>
    /// <param name="button">Target button.</param>
    /// <returns>Path to the icon image.</returns>
    public static string GetIcon(JoyButton button)
    {
        return ItemsFolder + _buttons[button];
    }

    /// <summary>
    /// Gets axis icon.
    /// </summary>
    /// <param name="axis">Target axis.</param>
    /// <param name="positive">Whether axis is positive.</param>
    /// <returns>Path to the icon image.</returns>
    public static string GetIcon(JoyAxis axis, float positive = 1)
    {
        string icon = positive > 0 ? _triggersPositive[axis] : _triggersNegative[axis];
        return ItemsFolder + icon;
    }
}
