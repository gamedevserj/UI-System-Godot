using Godot;
using UISystem.Core.Transitions;
using UISystem.Core.Views;
using UISystem.MenuSystem.Views;
using UISystem.Transitions;

namespace UISystem.MenuSystem.ViewHandlers;
internal class MainMenuViewHandler<TView> : MenuViewModel<MainMenuView>, IViewModel<MainMenuView>
{
    public MainMenuViewHandler(string prefab, Node parent) : base(prefab, parent)
    { }

    
}
