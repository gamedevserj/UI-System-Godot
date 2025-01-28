using Godot;
using UISystem.Core.MenuSystem;
using UISystem.MenuSystem.Views;
using UISystem.Views;

namespace UISystem.MenuSystem.Controllers;
internal class InGameMenuController<TViewCreator, TInputEvent>  : MenuControllerBase<ViewCreator<InGameMenuView>, InGameMenuView>
{

    public override MenuType Type => MenuType.InGame;

    public InGameMenuController(ViewCreator<InGameMenuView> viewCreator, IMenuModel model, IMenusManager<InputEvent, MenuType> menusManager) : base(viewCreator, model, menusManager)
    { }

    public override void OnPauseButtonDown()
    {
        _menusManager.ShowMenu(MenuType.Pause);
    }

    protected override void SetupElements()
    {

    }

}