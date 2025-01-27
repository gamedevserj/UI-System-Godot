using Godot;
using UISystem.Core.Transitions;
using UISystem.Core.Views;
using UISystem.MenuSystem.Views;
using UISystem.Transitions;

namespace UISystem.MenuSystem.ViewHandlers;
internal class OptionsMenuViewHandler<TView> : MenuViewModel<OptionsMenuView>, IViewModel<OptionsMenuView>
{
    public OptionsMenuViewHandler(string prefab, Node parent) : base(prefab, parent)
    { }

}
