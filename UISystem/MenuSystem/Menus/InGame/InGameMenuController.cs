using Godot;
using UISystem.Core.Constants;
using UISystem.Core.MenuSystem;
using UISystem.Core.MenuSystem.Controllers;
using UISystem.Core.MenuSystem.Enums;
using UISystem.MenuSystem.Constants;
using UISystem.MenuSystem.Models;
using UISystem.MenuSystem.Views;

namespace UISystem.MenuSystem.Controllers;
public class InGameMenuController : MenuController<InGameMenuView, InGameMenuModel>
{

    public override int Type => MenuType.InGame;

    public InGameMenuController(string prefab, InGameMenuModel model, MenusManager menusManager) : base(prefab, model, menusManager)
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
        _menusManager.ShowMenu(MenuType.Pause, MenuStackBehaviourEnum.AddToStack);
    }

    protected override void SetupElements()
    {

    }
}