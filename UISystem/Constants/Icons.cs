using Godot;
using UISystem.Common.Constants;

namespace UISystem.Constants;
public static class Icons
{

    public const string EllipsisImage = "res://UISystem/Textures/Inputs/ellipsis.png";
    
    public static string GetIcon(Key key) => KeyboardIcons.GetIcon(key);
    public static string GetIcon(MouseButton button) => MouseIcons.GetIcon(button);
    public static string GetIcon(JoyButton button) => PS5Icons.GetIcon(button);
    public static string GetIcon(JoyAxis axis, float positive) => PS5Icons.GetIcon(axis, positive);

}
