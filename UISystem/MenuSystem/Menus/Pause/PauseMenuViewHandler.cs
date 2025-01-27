using Godot;
using UISystem.Core.Transitions;
using UISystem.Core.Views;
using UISystem.MenuSystem.Views;
using UISystem.Transitions;

namespace UISystem.MenuSystem.ViewHandlers;
internal class PauseMenuViewHandler<TView> : MenuViewModel<PauseMenuView>, IViewModel<PauseMenuView>
{
    public PauseMenuViewHandler(string prefab, Node parent) : base(prefab, parent)
    { }

}
