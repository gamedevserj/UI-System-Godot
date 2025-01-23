using Godot;
using System;
using UISystem.Constants;
using UISystem.Core.Constants;
using UISystem.Core.Elements.Interfaces;
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
internal class PauseMenuController : MenuController<string, PauseMenuView, IMenuModel, Node, IFocusableControl>
{

    private const float MainElementAnimationDuration = 0.25f;
    private const float SecondaryElementAnimationDuration = 0.5f;

    public override int Type => MenuType.Pause;

    private readonly IPopupsManager _popupsManager;
    private readonly ScreenFadeManager _screenFadeManager;
    private readonly MenuBackgroundController _menuBackgroundController;

    public PauseMenuController(string prefab, IMenuModel model, IMenusManager menusManager, Node parent,
        IPopupsManager popupsManager, ScreenFadeManager screenFadeManager, MenuBackgroundController menuBackgroundController)
        : base(prefab, model, menusManager, parent)
    {
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
        base.Hide(stackingType, () =>
        {
            if (stackingType != StackingType.Add)
                _menuBackgroundController.HideBackground(instant);

            onComplete?.Invoke();
        }, instant);
    }

    protected override void SetupElements()
    {
        _view.ResumeGameButton.ButtonDown += PressedResume;
        _view.OptionsButton.ButtonDown += PressedOptions;
        _view.ReturnToMainMenuButton.ButtonDown += PressedReturn;
        DefaultSelectedElement = _view.ResumeGameButton;
    }

    private void PressedResume()
    {
        _menusManager.ReturnToPreviousMenu();
    }

    private void PressedOptions()
    {
        _lastSelectedElement = _view.OptionsButton;
        _menusManager.ShowMenu(MenuType.Options);
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
                    _menusManager.ShowMenu(MenuType.Main, StackingType.Clear, null, true);
                });
            }
            else if (result == PopupResult.No)
            {
                SwitchFocusAvailability(true);
            }
        });

    }

    protected override void ProcessInput(InputEvent key)
    {
        if (key.IsPressed() && key.IsAction(InputsData.ReturnToPreviousMenu))
        {
            PressedResume();
        }
    }

    protected override IViewTransition CreateTransition()
    {
        return new MainElementDropTransition(_view, _view.FadeObjectsContainer, _view.ResumeGameButton,
            new[] { _view.OptionsButton, _view.ReturnToMainMenuButton },
            MainElementAnimationDuration, SecondaryElementAnimationDuration);
    }
}
