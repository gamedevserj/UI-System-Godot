using Godot;
using MenuSystem.Constants;
using MenuSystem.Enums;
using MenuSystem.Models;
using MenuSystem.Views;

namespace MenuSystem.Controllers;
public class InGameMenuController : MenuControllerFade<InGameMenuView, InGameMenuModel>
{

    public override MenuType MenuType => MenuType.InGame;

    public InGameMenuController(string prefab, InGameMenuModel model, MenusManager menusManager, SceneTree sceneTree) : base(prefab, model, menusManager, sceneTree)
    {

    }

    public override void HandleInputPressedWhenActive(InputEvent key)
    {
        base.HandleInputPressedWhenActive(key);

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
        _menusManager.ChangeMenu(MenuType.Pause, MenuStackBehaviourEnum.AddToStack);
    }

}