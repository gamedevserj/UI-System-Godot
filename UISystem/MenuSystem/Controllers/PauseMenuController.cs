using Godot;
using MenuSystem.Constants;
using MenuSystem.Enums;
using MenuSystem.Models;
using MenuSystem.Views;
using PopupSystem;
using PopupSystem.Enums;
using System;
using UISystem.Constants;
using UISystem.PopupSystem.Enums;
using UISystem.ScreenFade;

namespace MenuSystem.Controllers;
public class PauseMenuController : MenuControllerFade<PauseMenuView, PauseMenuModel>
{

    public override MenuType MenuType => MenuType.Pause;

    private readonly PopupsManager _popupsManager;
    private readonly ScreenFadeManager _screenFadeManager;
    private readonly MenuBackgroundController _menuBackgroundController;

    public PauseMenuController(string prefab, PauseMenuModel model, MenusManager menusManager, SceneTree sceneTree, 
        PopupsManager popupsManager, ScreenFadeManager screenFadeManager, MenuBackgroundController menuBackgroundController) 
        : base(prefab, model, menusManager, sceneTree)
    {
        _popupsManager = popupsManager;
        _screenFadeManager = screenFadeManager;
        _menuBackgroundController = menuBackgroundController;
    }

    protected override void CreateView(Node menuParent)
    {
        base.CreateView(menuParent);
        SetupElements();
        _defaultSelectedElement = _view.ResumeGameButton;
    }

    public override void HandleInputPressedWhenActive(InputEvent key)
    {
        if (key.IsPressed())
        {
            if (key.IsAction(InputsData.PauseButton))
            {
                PressedResume();
            }
        }
    }

    public override void Show(Action onComplete = null, bool instant = false)
    {
        base.Show(onComplete, instant);
        _menuBackgroundController.ShowBackground(GetDuration(instant));
    }

    public override void Hide(MenuStackBehaviourEnum stackBehaviour, Action onComplete = null, bool instant = false)
    {
        base.Hide(stackBehaviour, onComplete, instant);
        
        if (stackBehaviour != MenuStackBehaviourEnum.AddToStack)
        {
            _menuBackgroundController.HideBackground(GetDuration(instant));
        }
    }

    private void SetupElements()
    {
        _view.ResumeGameButton.ButtonDown += PressedResume;
        _view.OptionsButton.ButtonDown += PressedOptions;
        _view.ReturnToMainMenuButton.ButtonDown += PressedReturn;
    }

    private void PressedResume()
    {
        _menusManager.ReturnToPreviousMenu();
    }

    private void PressedOptions()
    {
        _lastSelectedElement = _view.OptionsButton;
        _menusManager.ChangeMenu(MenuType.Options, MenuStackBehaviourEnum.AddToStack);
    }

    private void PressedReturn()
    {
        _lastSelectedElement = _view.ReturnToMainMenuButton;
        SwitchFocusAvailability(false);

        _popupsManager.ShowPopup(PopupType.YesNo, PopupMessages.QuitToMainMenu, (result) =>
        {
            if (result == PopupResult.Yes)
            {
                _screenFadeManager.FadeOut(() => 
                {
                    _menusManager.ChangeMenu(MenuType.Main, MenuStackBehaviourEnum.ClearStack, null, true);
                });
                
            }
            else if (result == PopupResult.No) 
            {
                SwitchFocusAvailability(true);
            }
        });
        
    }
}
