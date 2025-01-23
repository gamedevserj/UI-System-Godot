using Godot;
using System;
using UISystem.Constants;
using UISystem.Core.MenuSystem.Enums;
using UISystem.Core.MenuSystem.Interfaces;
using UISystem.Core.PopupSystem.Interfaces;
using UISystem.Core.Transitions.Interfaces;
using UISystem.MenuSystem.Constants;
using UISystem.MenuSystem.Views;
using UISystem.PopupSystem;
using UISystem.PopupSystem.Constants;
using UISystem.ScreenFade;
using UISystem.Transitions;

namespace UISystem.MenuSystem.Controllers;
internal class MainMenuController : MenuController<MainMenuView, IMenuModel>
{

    private const float MainElementAnimationDuration = 0.25f;
    private const float SecondaryElementAnimationDuration = 0.5f;

    public override int Type => MenuType.Main;

    private readonly SceneTree _sceneTree;
    private readonly IPopupsManager _popupsManager;
    private readonly MenuBackgroundController _menuBackgroundController;
    private readonly ScreenFadeManager _screenFadeManager;

    public MainMenuController(string prefab, IMenuModel model, IMenusManager menusManager, Node parent, SceneTree sceneTree,
        IPopupsManager popupsManager, ScreenFadeManager screenFadeManager, MenuBackgroundController menuBackgroundController) :
        base(prefab, model, menusManager, parent)
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
        DefaultSelectedElement = _view.PlayButton;
    }

    protected override void OnReturnToPreviousMenuButtonDown(Action onComplete, bool instant = false)
    {
        if (CanReturnToPreviousMenu)
            ShowQuitPopup();
    }

    private void PressedPlay()
    {
        _lastSelectedElement = _view.PlayButton;
        _screenFadeManager.FadeOut(() =>
        {
            _menusManager.ShowMenu(MenuType.InGame, StackingType.Clear, instant: true);
        });
    }

    private void PressedOptions()
    {
        _lastSelectedElement = _view.OptionsButton;
        _menusManager.ShowMenu(MenuType.Options);
    }

    private void PressedQuit()
    {
        _lastSelectedElement = _view.QuitButton;
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

    protected override IViewTransition CreateTransition()
    {
        return new MainElementDropTransition(_view, _view.FadeObjectsContainer, _view.PlayButton,
            new[] { _view.OptionsButton, _view.QuitButton },
            MainElementAnimationDuration, SecondaryElementAnimationDuration);
    }
}
