using Godot;
using UISystem.Core.MenuSystem;
using UISystem.Elements;
using UISystem.MenuSystem.Constants;
using UISystem.MenuSystem.ViewHandlers;
using UISystem.MenuSystem.Views;

namespace UISystem.MenuSystem.Controllers;
internal class InGameMenuController<TViewHandler, TInputEvent> 
    : MenuController<InGameMenuViewHandler<InGameMenuView>, InGameMenuView, IMenuModel, InputEvent, IFocusableControl>
{

    public override int Type => MenuType.InGame;

    public InGameMenuController(InGameMenuViewHandler<InGameMenuView> viewHandler, IMenuModel model, IMenusManager<InputEvent> menusManager) : base(viewHandler, model, menusManager)
    { }

    public override void OnPauseButtonDown()
    {
        _menusManager.ShowMenu(MenuType.Pause);
    }

    protected override void SetupElements()
    {

    }

}