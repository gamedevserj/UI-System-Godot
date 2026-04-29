using System.Collections.Generic;
using Godot;

namespace UISystem.Common.Constants;

/// <summary>
/// Class containing paths to PS5 icons.
/// </summary>
public static class PS5Icons
{
    private static readonly Dictionary<JoyButton, string> _buttons = new()
        {
            { JoyButton.DpadLeft, "playstation_dpad_left.png" },
            { JoyButton.DpadUp, "playstation_dpad_up.png" },
            { JoyButton.DpadRight, "playstation_dpad_right.png" },
            { JoyButton.DpadDown, "playstation_dpad_down.png" },
            { JoyButton.A, "playstation_button_color_cross.png" }, // cross
            { JoyButton.B, "playstation_button_color_circle.png" }, // circle
            { JoyButton.X, "playstation_button_color_square.png" }, // square
            { JoyButton.Y, "playstation_button_color_triangle.png" }, // triangle
            { JoyButton.LeftShoulder, "playstation_trigger_l1_alternative.png" }, // L1
            { JoyButton.RightShoulder, "playstation_trigger_r1_alternative.png" }, // R1
            { JoyButton.Misc1, "playstation_button_create.png" }, // share
            { JoyButton.Start, "playstation_button_options.png" }, // options
            { JoyButton.LeftStick, "playstation_button_l3.png" }, // L3
            { JoyButton.RightStick, "playstation_button_r3.png" }, // R3
        };

    private static readonly Dictionary<JoyAxis, string> _triggersPositive = new()
        {
            { JoyAxis.TriggerLeft, "playstation_trigger_l2_alternative.png" },
            { JoyAxis.TriggerRight, "playstation_trigger_r2_alternative.png" },
            { JoyAxis.LeftX, "playstation_stick_l_right.png" },
            { JoyAxis.LeftY, "playstation_stick_l_down.png" },
            { JoyAxis.RightX, "playstation_stick_r_right.png" },
            { JoyAxis.RightY, "playstation_stick_r_down.png" },
        };

    private static readonly Dictionary<JoyAxis, string> _triggersNegative = new()
        {
            { JoyAxis.LeftX, "playstation_stick_l_left.png" },
            { JoyAxis.LeftY, "playstation_stick_l_up.png" },
            { JoyAxis.RightX, "playstation_stick_r_left.png" },
            { JoyAxis.RightY, "playstation_stick_r_up.png" },
        };

    private static string ItemsFolder => "res://UISystem/Textures/Inputs/PS5/";

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
    public static string GetIcon(JoyAxis axis, float positive)
    {
        string icon = positive > 0 ? _triggersPositive[axis] : _triggersNegative[axis];
        return ItemsFolder + icon;
    }
}
