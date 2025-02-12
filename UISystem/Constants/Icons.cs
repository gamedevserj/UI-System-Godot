using Godot;
using UISystem.Common.Constants;
using UISystem.Common.Enums;

namespace UISystem.Constants;
public static class Icons
{

    public const string EllipsisImage = "res://UISystem/Textures/Inputs/ellipsis.png";

    public static string GetIcon(InputEvent inputEvent, ControllerIconsType iconsType)
    {
        if (inputEvent is InputEventKey key)
        {
            return GetIcon(key.PhysicalKeycode);
        }
        else if (inputEvent is InputEventMouseButton mouseButton)
        {
            return GetIcon(mouseButton.ButtonIndex);
        }
        if (inputEvent is InputEventJoypadButton inputButton)
        {
            return GetIcon(inputButton.ButtonIndex, iconsType);
        }
        else if (inputEvent is InputEventJoypadMotion motion)
        {
            return GetIcon(motion.Axis, motion.AxisValue, iconsType);
        }
        else
            throw new System.Exception("Couldn't find icon for input type " + inputEvent.GetType());
    }
    private static string GetIcon(Key key) => KeyboardIcons.GetIcon(key);
    private static string GetIcon(MouseButton button) => MouseIcons.GetIcon(button);
    private static string GetIcon(JoyButton button, ControllerIconsType iconsType) => GetIconByControllerType(button, iconsType);
    private static string GetIcon(JoyAxis axis, float positive, ControllerIconsType iconsType) => GetIconByControllerType(axis, positive, iconsType);


    private static string GetIconByControllerType(JoyButton button, ControllerIconsType iconsType)
    {
        return iconsType switch
        {
            ControllerIconsType.Xbox => XboxIcons.GetIcon(button),
            ControllerIconsType.Ps5 => PS5Icons.GetIcon(button),
            _ => XboxIcons.GetIcon(button),
        };
    }

    private static string GetIconByControllerType(JoyAxis axis, float positive, ControllerIconsType iconsType)
    {
        return iconsType switch
        {
            ControllerIconsType.Xbox => XboxIcons.GetIcon(axis, positive),
            ControllerIconsType.Ps5 => PS5Icons.GetIcon(axis, positive),
            _ => XboxIcons.GetIcon(axis, positive),
        };
    }
}
