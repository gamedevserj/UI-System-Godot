using Godot;
using UISystem.Core.Transitions.Interfaces;
using UISystem.Core.Views.Interfaces;
using UISystem.MenuSystem.Views;
using UISystem.Transitions;

namespace UISystem.MenuSystem.ViewHandlers;
internal class InGameMenuViewHandler<TView> : MenuViewHandler<InGameMenuView>, IViewHandler<InGameMenuView> 
{
    public InGameMenuViewHandler(string prefab, Node parent) : base(prefab, parent)
    { }

    public override IViewTransition CreateTransition()
    {
        return new FadeTransition(_view.FadeObjectsContainer);
    }
}
