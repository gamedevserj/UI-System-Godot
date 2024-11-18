using Godot;
using UISystem.Core.Constants;
using UISystem.Core.MenuSystem.Controllers;
using UISystem.Core.MenuSystem.Interfaces;
using UISystem.MenuSystem.Constants;
using UISystem.MenuSystem.Models;
using UISystem.MenuSystem.Views;

namespace UISystem.MenuSystem.Controllers;
internal class InGameMenuController : MenuController<InGameMenuView, InGameMenuModel>
{

    public override int Type => MenuType.InGame;

    public InGameMenuController(string prefab, InGameMenuModel model, IMenusManager menusManager) : base(prefab, model, menusManager)
    {

    }

    public override void HandleInputPressedWhenActive(InputEvent key)
    {
        if (key.IsPressed())
        {
            if (key.IsAction(InputsData.PauseButton))
            {
                PauseGame();
            }
        }
    }

    private void PauseGame()
    {
        _menusManager.ShowMenu(MenuType.Pause);
    }

    protected override void SetupElements()
    {

    }
}