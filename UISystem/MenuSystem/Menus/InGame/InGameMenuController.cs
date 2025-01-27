using Godot;
using UISystem.Core.MenuSystem;
using UISystem.MenuSystem.Constants;
using UISystem.MenuSystem.Views;

namespace UISystem.MenuSystem.Controllers;
internal class InGameMenuController<TViewHandler, TInputEvent>  : MenuControllerBase<MenuViewCreator<InGameMenuView>, InGameMenuView>
{

    public override int Type => MenuType.InGame;

    public InGameMenuController(MenuViewCreator<InGameMenuView> viewHandler, IMenuModel model, IMenusManager<InputEvent> menusManager) : base(viewHandler, model, menusManager)
    { }

    public override void OnPauseButtonDown()
    {
        _menusManager.ShowMenu(MenuType.Pause);
    }

    protected override void SetupElements()
    {

    }

}