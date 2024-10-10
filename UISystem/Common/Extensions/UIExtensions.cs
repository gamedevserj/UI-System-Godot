using Godot;
using UISystem.Common.ElementViews;
using UISystem.Common.Structs;

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

}
