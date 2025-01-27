using Godot;
using UISystem.Core.Transitions;
using UISystem.Core.Views;
using UISystem.MenuSystem.Views;
using UISystem.Transitions;

namespace UISystem.MenuSystem.ViewHandlers;
internal class InGameMenuViewHandler<TView> : MenuViewModel<InGameMenuView>, IViewModel<InGameMenuView> 
{
    public InGameMenuViewHandler(string prefab, Node parent) : base(prefab, parent)
    { }

    
}
