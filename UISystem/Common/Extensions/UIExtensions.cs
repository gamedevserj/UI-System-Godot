using Godot;
using UISystem.Common.Structs;
using UISystem.Core.Common.ElementViews;

namespace UISystem.Common.Extensions;
internal static class UIExtensions
{

    public static void AddMultipleItems(this DropdownView optionButton, OptionButtonItem[] items)
    {
        for (int i = 0; i < items.Length; i++)
        {
            optionButton.AddItem(items[i].Label, items[i].Id);
        }
    }

    public static void SetSizeAndPosition(this Control control, Vector2 size, Vector2 position)
    {
        control.Size = size;
        control.Position = position;
    }

    public static void HideItem(this CanvasItem item)
    {
        item.Modulate = new Color(item.Modulate, 0);
    }

    public static void ShowItem(this CanvasItem item)
    {
        item.Modulate = new Color(item.Modulate, 1);
    }

}
