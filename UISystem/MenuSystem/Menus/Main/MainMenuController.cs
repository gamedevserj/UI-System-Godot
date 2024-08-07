using Godot;
using System;
using UISystem.Constants;
using UISystem.MenuSystem.Enums;
using UISystem.MenuSystem.Models;
using UISystem.MenuSystem.Views;
using UISystem.PopupSystem;
using UISystem.PopupSystem.Enums;
using UISystem.ScreenFade;

namespace UISystem.MenuSystem.Controllers;
public class MainMenuController : MenuController<MainMenuView, MainMenuModel>
{

    public override MenuType MenuType => MenuType.Main;

    private readonly SceneTree _sceneTree;
    private readonly PopupsManager _popupsManager;
    private readonly MenuBackgroundController _menuBackgroundController;
    private readonly ScreenFadeManager _screenFadeManager;

    public MainMenuController(string prefab, MainMenuModel model, MenusManager menusManager, SceneTree sceneTree,
        PopupsManager popupsManager, ScreenFadeManager screenFadeManager, MenuBackgroundController menuBackgroundController) : 
        base(prefab, model, menusManager)
    {
        _sceneTree = sceneTree;
        _popupsManager = popupsManager;
        _menuBackgroundController = menuBackgroundController;
        _screenFadeManager = screenFadeManager;
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
        _view.PlayButton.ButtonDown += PressedPlay;
        _view.OptionsButton.ButtonDown += PressedOptions;
        _view.QuitButton.ButtonDown += PressedQuit;
        _defaultSelectedElement = _view.PlayButton;
    }

    protected override void OnReturnToPreviousMenuButtonDown()
    {
        PressedQuit();
    }

    private void PressedPlay()
    {
        _lastSelectedElement = _view.PlayButton;
        _screenFadeManager.FadeOut(() =>
        {
            _menusManager.ShowMenu(MenuType.InGame, MenuStackBehaviourEnum.ClearStack, null, true);
        });
    }

    private void PressedOptions()
    {
        _lastSelectedElement = _view.OptionsButton;
        _menusManager.ShowMenu(MenuType.Options, MenuStackBehaviourEnum.AddToStack);
    }

    private void PressedQuit()
    {
        _lastSelectedElement = _view.QuitButton;
        SwitchFocusAvailability(false);
        _popupsManager.ShowPopup(PopupType.YesNo, this, PopupMessages.QuitGame, (result)=>
        {
            if (result == PopupResult.Yes)
                _sceneTree.Quit(); 
            else if (result == PopupResult.No) 
                SwitchFocusAvailability(true);
        });
    }

}
