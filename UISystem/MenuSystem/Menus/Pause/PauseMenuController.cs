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
internal class PauseMenuController<TViewHandler, TInputEvent>
    : MenuController<PauseMenuViewHandler<PauseMenuView>, PauseMenuView, IMenuModel, InputEvent, IFocusableControl>
{

    public override int Type => MenuType.Pause;

    private readonly IPopupsManager<InputEvent> _popupsManager;
    private readonly ScreenFadeManager _screenFadeManager;
    private readonly MenuBackgroundController _menuBackgroundController;

    public PauseMenuController(PauseMenuViewHandler<PauseMenuView> viewHandler, IMenuModel model, IMenusManager<InputEvent> menusManager,
        IPopupsManager<InputEvent> popupsManager, ScreenFadeManager screenFadeManager, MenuBackgroundController menuBackgroundController) 
        : base(viewHandler, model, menusManager)
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
        _view.ResumeGameButton.ButtonDown += OnCancelButtonDown;
        _view.OptionsButton.ButtonDown += PressedOptions;
        _view.ReturnToMainMenuButton.ButtonDown += PressedReturn;
    }

    private void PressedOptions()
    {
        _view.SetLastSelectedElement(_view.OptionsButton);
        _menusManager.ShowMenu(MenuType.Options);
    }

    private void PressedReturn()
    {
        _view.SetLastSelectedElement(_view.ReturnToMainMenuButton);
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

}
