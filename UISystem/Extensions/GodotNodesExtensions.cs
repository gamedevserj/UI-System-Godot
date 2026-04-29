using Godot;
using UISystem.Elements;

namespace UISystem.Extensions
{
    /// <summary>
    /// Class containing extensions for godot nodes.
    /// </summary>
    public static class GodotNodesExtensions
    {
        /// <summary>
        /// Checks whether godot object is valid. That includes checking if node is queued for deletion.
        /// </summary>
        /// <typeparam name="T">Type of godot object. Must be <see cref="GodotObject"/>.</typeparam>
        /// <param name="node">Node instance.</param>
        /// <returns>True if object is valid, false otherwise.</returns>
        public static bool IsValid<T>(this T node)
            where T : GodotObject
        {
            return node != null
                && GodotObject.IsInstanceValid(node)
                && !node.IsQueuedForDeletion();
        }

        /// <summary>
        /// Safely frees node checking if it is valid beforehand.
        /// </summary>
        /// <param name="node">Node to be freed.</param>
        public static void SafeQueueFree(this Node node)
        {
            if (node.IsValid())
                node.QueueFree();
        }

        /// <summary>
        /// Adds multiple items to the option button.
        /// </summary>
        /// <param name="optionButton">Target option button.</param>
        /// <param name="items">Items to add.</param>
        public static void AddMultipleItems(this OptionButton optionButton, OptionButtonItem[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                optionButton.AddItem(items[i].Label, items[i].Id);
            }
        }

        /// <summary>
        /// Sets control's size and position.
        /// </summary>
        /// <param name="control">Target control.</param>
        /// <param name="size">Size.</param>
        /// <param name="position">Position.</param>
        public static void SetSizeAndPosition(this Control control, Vector2 size, Vector2 position)
        {
            control.Size = size;
            control.Position = position;
        }

        /// <summary>
        /// Hides canvas item and its children.
        /// </summary>
        /// <param name="item">Item to hide.</param>
        public static void HideItem(this CanvasItem item)
        {
            item.Modulate = new Color(item.Modulate, 0);
        }

        /// <summary>
        /// Shows canvas item and its children.
        /// </summary>
        /// <param name="item">Item to show.</param>
        public static void ShowItem(this CanvasItem item)
        {
            item.Modulate = new Color(item.Modulate, 1);
        }
    }
}
