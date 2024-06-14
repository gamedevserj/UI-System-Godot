using Godot;
using MenuSystem.Enums;
using MenuSystem.Views;
using PopupSystem;
using PopupSystem.Enums;
using System;
using UISystem.ScreenFade;
using MenuSystem.Models;
using UISystem.Constants;

namespace MenuSystem.Controllers;
public class MainMenuController : MenuControllerFade<MainMenuView, MainMenuModel>
{

    public override MenuType MenuType => MenuType.Main;

    private readonly PopupsManager _popupsManager;
    private readonly MenuBackgroundController _menuBackgroundController;
    private readonly ScreenFadeManager _screenFadeManager;

    public MainMenuController(string prefab, MainMenuModel model, MenusManager menusManager, SceneTree sceneTree, 
        PopupsManager popupsManager, ScreenFadeManager screenFadeManager, MenuBackgroundController menuBackgroundController) 
        : base(prefab, model, menusManager, sceneTree)
    {
        _popupsManager = popupsManager;
        _menuBackgroundController = menuBackgroundController;
        _screenFadeManager = screenFadeManager;
    }

    protected override void CreateView(Node menuParent)
    {
        base.CreateView(menuParent);
        SetupElements();
        _defaultSelectedElement = _view.PlayButton;
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
        _view.PlayButton.ButtonDown += PressedPlay;
        _view.OptionsButton.ButtonDown += PressedOptions;
        _view.QuitButton.ButtonDown += PressedQuit;
    }

    private void PressedPlay()
    {
        _lastSelectedElement = _view.PlayButton;
        _screenFadeManager.FadeOut(() =>
        {
            _menusManager.ChangeMenu(MenuType.InGame, MenuStackBehaviourEnum.ClearStack, null, true);
        });
    }

    private void PressedOptions()
    {
        _lastSelectedElement = _view.OptionsButton;
        _menusManager.ChangeMenu(MenuType.Options, MenuStackBehaviourEnum.AddToStack);
    }

    private void PressedQuit()
    {
        _lastSelectedElement = _view.QuitButton;
        SwitchFocusAvailability(false);
        _popupsManager.ShowPopup(PopupType.ConfirmationPopup, PopupMessages.QuitGame, (result)=>
        {
            if (result)
            {
                _sceneTree.Quit(); 
            }
            else
            {
                SwitchFocusAvailability(true);
            }
        });
    }

}
