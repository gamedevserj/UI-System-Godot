using Godot;
using UISystem.Common.Elements;
using UISystem.Common.Structs;

namespace GodotExtensions
{
    public static class Extensions
    {

        public static bool IsValid<T>(this T node) where T : GodotObject
        {
            return node != null
                && GodotObject.IsInstanceValid(node)
                && !node.IsQueuedForDeletion();
        }

        public static void SafeQueueFree(this Node node)
        {
            if (node.IsValid()) node.QueueFree();
        }

        public static void AddMultipleItems(this DropdownView optionButton, OptionButtonItem[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                optionButton.AddItem(items[i].Label, items[i].Id);
            }
        }

    }
}
