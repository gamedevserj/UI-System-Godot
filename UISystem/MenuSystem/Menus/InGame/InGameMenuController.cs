using Godot;
using UISystem.Core.MenuSystem;
using UISystem.MenuSystem.Constants;
using UISystem.MenuSystem.Views;
using UISystem.Views;

namespace UISystem.MenuSystem.Controllers;
internal class InGameMenuController<TViewHandler, TInputEvent>  : MenuControllerBase<ViewCreator<InGameMenuView>, InGameMenuView>
{

    public override int Type => MenuType.InGame;

    public InGameMenuController(ViewCreator<InGameMenuView> viewHandler, IMenuModel model, IMenusManager<InputEvent> menusManager) : base(viewHandler, model, menusManager)
    { }

    public override void OnPauseButtonDown()
    {
        _menusManager.ShowMenu(MenuType.Pause);
    }

    protected override void SetupElements()
    {

    }

}