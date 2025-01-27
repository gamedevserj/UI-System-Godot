using Godot;
using UISystem.Core.Views;
using UISystem.MenuSystem.Views;

namespace UISystem.MenuSystem.ViewHandlers;
internal class VideoSettingsMenuViewHandler<TView> : MenuViewModel<VideoSettingsMenuView>, IViewModel<VideoSettingsMenuView>
{
    public VideoSettingsMenuViewHandler(string prefab, Node parent) : base(prefab, parent)
    { }

}
