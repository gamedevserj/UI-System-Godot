using Godot;
using System.Collections.Generic;

namespace UISystem.Common.Constants;
public static class XboxIcons
{

    private static string ItemsFolder => "res://UISystem/Textures/Inputs/Xbox/";

    private static readonly Dictionary<JoyButton, string> _buttons;
    private static readonly Dictionary<JoyAxis, string> _triggersPositive;
    private static readonly Dictionary<JoyAxis, string> _triggersNegative;

    static XboxIcons()
    {
        _buttons = new Dictionary<JoyButton, string>
        {
            { JoyButton.DpadLeft, "xbox_dpad_left.png" },
            { JoyButton.DpadUp, "xbox_dpad_up.png" },
            { JoyButton.DpadRight, "xbox_dpad_right.png" },
            { JoyButton.DpadDown, "xbox_dpad_down.png" },
            { JoyButton.A, "xbox_button_color_a.png" },
            { JoyButton.B, "xbox_button_color_b.png" },
            { JoyButton.X, "xbox_button_color_x.png" },
            { JoyButton.Y, "xbox_button_color_y.png" },
            // L1
            { JoyButton.LeftShoulder, "xbox_lb.png" },
            // R1
            { JoyButton.RightShoulder, "xbox_rb.png" },
            // share
            { JoyButton.Misc1, "xbox_button_share.png" },
            // options
            { JoyButton.Start, "xbox_button_menu.png" },
            // L3
            { JoyButton.LeftStick, "xbox_ls.png" },
            // R3
            { JoyButton.RightStick, "xbox_rs.png" },
        };

        _triggersPositive = new Dictionary<JoyAxis, string>
        {
            {JoyAxis.TriggerLeft, "xbox_lt.png" },
            {JoyAxis.TriggerRight, "xbox_rt.png" },
            {JoyAxis.LeftX, "xbox_stick_l_right.png" },
            {JoyAxis.LeftY, "xbox_stick_l_down.png" },
            {JoyAxis.RightX, "xbox_stick_r_right.png" },
            {JoyAxis.RightY, "xbox_stick_r_down.png" },
        };

        _triggersNegative = new Dictionary<JoyAxis, string>
        {
            {JoyAxis.LeftX, "xbox_stick_l_left.png" },
            {JoyAxis.LeftY, "xbox_stick_l_up.png" },
            {JoyAxis.RightX, "xbox_stick_r_left.png" },
            {JoyAxis.RightY, "xbox_stick_r_up.png" },
        };

    }

    public static string GetIcon(JoyButton button)
    {
        return ItemsFolder + _buttons[button];
    }

    public static string GetIcon(JoyAxis axis, float positive = 1)
    {
        string icon = positive > 0 ? _triggersPositive[axis] : _triggersNegative[axis];
        return ItemsFolder + icon;
    }

}
