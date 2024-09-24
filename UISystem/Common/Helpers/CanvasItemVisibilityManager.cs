using Godot;

namespace UISystem.Common.Helpers;
public static class CanvasItemVisibilityManager
{

    public static void HideItem(CanvasItem item)
    {
        SwitchItemVisibility(item, 0);
    }

    public static void ShowItem(CanvasItem item)
    {
        SwitchItemVisibility(item, 1);
    }

    public static void SwitchItemVisibility(CanvasItem item, float visibility)
    {
        item.Modulate = new Color(item.Modulate, visibility);
    }

}
