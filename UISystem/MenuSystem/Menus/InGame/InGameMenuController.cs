using Godot;
using UISystem.Core.MenuSystem;
using UISystem.Core.Views;
using UISystem.MenuSystem.Views;

namespace UISystem.MenuSystem.Controllers;
internal class InGameMenuController : MenuControllerBase<IViewCreator<InGameMenuView>, InGameMenuView>
{

    public override MenuType Type => MenuType.InGame;

    public InGameMenuController(IViewCreator<InGameMenuView> viewCreator, IMenuModel model, IMenusManager<MenuType> menusManager) : base(viewCreator, model, menusManager)
    { }

    public override void OnPauseButtonDown()
    {
        _menusManager.ShowMenu(MenuType.Pause);
    }

    protected override void SetupElements()
    {

    }

}