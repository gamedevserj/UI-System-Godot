using Godot;
using System.Collections.Generic;

namespace UISystem.Common.Constants;
public static class PS5Icons
{
    private static string ItemsFolder => "res://UISystem/Textures/Inputs/PS5/";

    private static readonly Dictionary<JoyButton, string> _buttons;
    private static readonly Dictionary<JoyAxis, string> _triggersPositive;
    private static readonly Dictionary<JoyAxis, string> _triggersNegative;

    static PS5Icons()
    {
        _buttons = new Dictionary<JoyButton, string>
        {
            { JoyButton.DpadLeft, "playstation_dpad_left.png" },
            { JoyButton.DpadUp, "playstation_dpad_up.png" },
            { JoyButton.DpadRight, "playstation_dpad_right.png" },
            { JoyButton.DpadDown, "playstation_dpad_down.png" },
            // cross
            { JoyButton.A, "playstation_button_color_cross.png" },
            // circle
            { JoyButton.B, "playstation_button_color_circle.png" },
            // square
            { JoyButton.X, "playstation_button_color_square.png" },
            // triangle
            { JoyButton.Y, "playstation_button_color_triangle.png" },
            // L1
            { JoyButton.LeftShoulder, "playstation_trigger_l1_alternative.png" },
            // R1
            { JoyButton.RightShoulder, "playstation_trigger_r1_alternative.png" },
            // share
            { JoyButton.Misc1, "playstation_button_create.png" },
            // options
            { JoyButton.Start, "playstation_button_options.png" },
            // L3
            { JoyButton.LeftStick, "playstation_button_l3.png" },
            // R3
            { JoyButton.RightStick, "playstation_button_r3.png" },
        };

        _triggersPositive = new Dictionary<JoyAxis, string>
        {
            {JoyAxis.TriggerLeft, "playstation_trigger_l2_alternative.png" },
            {JoyAxis.TriggerRight, "playstation_trigger_r2_alternative.png" },
            {JoyAxis.LeftX, "playstation_stick_l_right.png" },
            {JoyAxis.LeftY, "playstation_stick_l_down.png" },
            {JoyAxis.RightX, "playstation_stick_r_right.png" },
            {JoyAxis.RightY, "playstation_stick_r_down.png" },
        };

        _triggersNegative = new Dictionary<JoyAxis, string>
        {
            {JoyAxis.LeftX, "playstation_stick_l_left.png" },
            {JoyAxis.LeftY, "playstation_stick_l_up.png" },
            {JoyAxis.RightX, "playstation_stick_r_left.png" },
            {JoyAxis.RightY, "playstation_stick_r_up.png" },
        };

    }

    public static string GetIcon(JoyButton button)
    {
        return ItemsFolder + _buttons[button];
    }

    public static string GetIcon(JoyAxis axis, float positive)
    {
        string icon = positive > 0 ? _triggersPositive[axis] : _triggersNegative[axis];
        return ItemsFolder + icon;
    }
}
