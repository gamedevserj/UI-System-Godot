using Godot;
using System;
using UISystem.Constants;
using UISystem.Core.Elements.Interfaces;
using UISystem.Core.MenuSystem.Controllers;
using UISystem.Core.MenuSystem.Enums;
using UISystem.Core.MenuSystem.Interfaces;
using UISystem.Core.PopupSystem.Interfaces;
using UISystem.MenuSystem.Constants;
using UISystem.MenuSystem.ViewHandlers;
using UISystem.MenuSystem.Views;
using UISystem.PopupSystem;
using UISystem.PopupSystem.Constants;
using UISystem.ScreenFade;

namespace UISystem.MenuSystem.Controllers;
internal class MainMenuController<TViewHandler, TInputEvent> 
    : MenuController<MainMenuViewHandler<MainMenuView>, MainMenuView, IMenuModel, InputEvent, IFocusableControl>
{

    public override int Type => MenuType.Main;

    private readonly SceneTree _sceneTree;
    private readonly IPopupsManager<InputEvent> _popupsManager;
    private readonly MenuBackgroundController _menuBackgroundController;
    private readonly ScreenFadeManager _screenFadeManager;

    public MainMenuController(MainMenuViewHandler<MainMenuView> viewHandler, IMenuModel model, IMenusManager<InputEvent> menusManager,
        SceneTree sceneTree, IPopupsManager<InputEvent> popupsManager, ScreenFadeManager screenFadeManager, MenuBackgroundController menuBackgroundController) 
        : base(viewHandler, model, menusManager)
    {
        _sceneTree = sceneTree;
        _popupsManager = popupsManager;
        _screenFadeManager = screenFadeManager;
        _menuBackgroundController = menuBackgroundController;
    }

    public override void Show(Action onComplete = null, bool instant = false)
    {
        base.Show(onComplete, instant);
        _menuBackgroundController.ShowBackground(instant);
    }

    public override void Hide(StackingType stackingType, Action onComplete = null, bool instant = false)
    {
        base.Hide(stackingType, onComplete, instant);
        if (stackingType != StackingType.Add)
            _menuBackgroundController.HideBackground(instant);
    }

    protected override void SetupElements()
    {
        _view.PlayButton.ButtonDown += PressedPlay;
        _view.OptionsButton.ButtonDown += PressedOptions;
        _view.QuitButton.ButtonDown += PressedQuit;
    }

    public override void OnReturnButtonDown()
    {
        if (CanReturnToPreviousMenu)
            ShowQuitPopup();
    }

    private void PressedPlay()
    {
        _view.SetLastSelectedElement(_view.PlayButton);
        _screenFadeManager.FadeOut(() =>
        {
            _menusManager.ShowMenu(MenuType.InGame, StackingType.Clear, instant: true);
        });
    }

    private void PressedOptions()
    {
        _view.SetLastSelectedElement(_view.OptionsButton);
        _menusManager.ShowMenu(MenuType.Options);
    }

    private void PressedQuit()
    {
        _view.SetLastSelectedElement(_view.QuitButton);
        ShowQuitPopup();
    }

    private void ShowQuitPopup()
    {
        SwitchFocusAvailability(false);
        _popupsManager.ShowPopup(PopupType.YesNo, this, PopupMessages.QuitGame, (result) =>
        {
            if (result == PopupResult.Yes)
                _sceneTree.Quit();
            else if (result == PopupResult.No)
                SwitchFocusAvailability(true);
        });
    }

}
