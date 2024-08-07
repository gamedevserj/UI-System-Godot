﻿using Godot;
using System;
using UISystem.Common.Constants;
using UISystem.Constants;
using UISystem.MenuSystem.Enums;
using UISystem.MenuSystem.Models;
using UISystem.MenuSystem.Views;
using UISystem.PopupSystem;
using UISystem.PopupSystem.Enums;
using UISystem.ScreenFade;

namespace UISystem.MenuSystem.Controllers;
public class PauseMenuController : MenuController<PauseMenuView, PauseMenuModel>
{

    public override MenuType MenuType => MenuType.Pause;

    private readonly PopupsManager _popupsManager;
    private readonly ScreenFadeManager _screenFadeManager;
    private readonly MenuBackgroundController _menuBackgroundController;

    public PauseMenuController(string prefab, PauseMenuModel model, MenusManager menusManager, 
        PopupsManager popupsManager, ScreenFadeManager screenFadeManager, MenuBackgroundController menuBackgroundController) 
        : base(prefab, model, menusManager)
    {
        _popupsManager = popupsManager;
        _screenFadeManager = screenFadeManager;
        _menuBackgroundController = menuBackgroundController;
    }

    public override void HandleInputPressedWhenActive(InputEvent key)
    {
        if (key.IsPressed())
        {
            if (key.IsAction(InputsData.ReturnToPreviousMenu))
            {
                PressedResume();
            }
        }
    }

    public override void Show(Action onComplete = null, bool instant = false)
    {
        base.Show(onComplete, instant);
        _menuBackgroundController.ShowBackground(instant);
    }

    public override void Hide(MenuStackBehaviourEnum stackBehaviour, Action onComplete = null, bool instant = false)
    {
        base.Hide(stackBehaviour, onComplete, instant);
        
        if (stackBehaviour != MenuStackBehaviourEnum.AddToStack)
        {
            _menuBackgroundController.HideBackground(instant);
        }
    }

    protected override void SetupElements()
    {
        _view.ResumeGameButton.ButtonDown += PressedResume;
        _view.OptionsButton.ButtonDown += PressedOptions;
        _view.ReturnToMainMenuButton.ButtonDown += PressedReturn;
        _defaultSelectedElement = _view.ResumeGameButton;
    }

    private void PressedResume()
    {
        _menusManager.ReturnToPreviousMenu();
    }

    private void PressedOptions()
    {
        _lastSelectedElement = _view.OptionsButton;
        _menusManager.ShowMenu(MenuType.Options, MenuStackBehaviourEnum.AddToStack);
    }

    private void PressedReturn()
    {
        _lastSelectedElement = _view.ReturnToMainMenuButton;
        SwitchFocusAvailability(false);

        _popupsManager.ShowPopup(PopupType.YesNo, this, PopupMessages.QuitToMainMenu, (result) =>
        {
            if (result == PopupResult.Yes)
            {
                _screenFadeManager.FadeOut(() => 
                {
                    _menusManager.ShowMenu(MenuType.Main, MenuStackBehaviourEnum.ClearStack, null, true);
                });
                
            }
            else if (result == PopupResult.No) 
            {
                SwitchFocusAvailability(true);
            }
        });
        
    }
}
