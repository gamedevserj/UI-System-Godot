using UISystem.Core.MenuSystem;
using UISystem.Core.Views;
using UISystem.MenuSystem.Views;

namespace UISystem.MenuSystem.Controllers;
internal class InGameMenuController : MenuControllerBase<IViewCreator<InGameMenuView>, InGameMenuView>
{

    public InGameMenuController(IViewCreator<InGameMenuView> viewCreator, IMenusManager menusManager) 
        : base(viewCreator, menusManager)
    { }

    public override void OnPauseButtonDown()
    {
        MenusManager.ShowMenu(typeof(PauseMenuView));
    }

    protected override void SetupElements()
    {

    }

}
