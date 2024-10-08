using Godot;

namespace UISystem.Core.Extensions
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

    }
}
