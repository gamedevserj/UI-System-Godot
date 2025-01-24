using Godot;
using UISystem.Core.Elements.Interfaces;
using UISystem.Core.MenuSystem.Interfaces;
using UISystem.Core.PhysicalInput;
using UISystem.Core.Transitions.Interfaces;
using UISystem.MenuSystem.Constants;
using UISystem.MenuSystem.Views;
using UISystem.Transitions;

namespace UISystem.MenuSystem.Controllers;
internal class InGameMenuController : MenuController<string, InGameMenuView, IMenuModel, Node, IFocusableControl>
{

    public override int Type => MenuType.InGame;

    public InGameMenuController(string prefab, IMenuModel model, IMenusManager<InputEvent> menusManager, Node parent) 
        : base(prefab, model, menusManager, parent)
    { }

    public override void OnPauseButtonDown()
    {
        _menusManager.ShowMenu(MenuType.Pause);
    }

    protected override void SetupElements()
    {

    }

    protected override IViewTransition CreateTransition() => new FadeTransition(_view.FadeObjectsContainer);
}