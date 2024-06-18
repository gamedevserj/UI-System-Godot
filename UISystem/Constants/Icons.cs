using Godot;
using UISystem.Common.Constants;

namespace UISystem.Constants;
public static class Icons
{

    public const string EllipsisImage = "res://UISystem/Textures/Inputs/ellipsis.png";

    public static string GetIcon(InputEvent inputEvent)
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
            return GetIcon(inputButton.ButtonIndex);
        }
        else if (inputEvent is InputEventJoypadMotion motion)
        {
            return GetIcon(motion.Axis, motion.AxisValue);
        }
        else
            throw new System.Exception("Couldn't find icon for input type " + inputEvent.GetType());
    }
    private static string GetIcon(Key key) => KeyboardIcons.GetIcon(key);
    private static string GetIcon(MouseButton button) => MouseIcons.GetIcon(button);
    private static string GetIcon(JoyButton button) => PS5Icons.GetIcon(button);
    private static string GetIcon(JoyAxis axis, float positive) => PS5Icons.GetIcon(axis, positive);

}
