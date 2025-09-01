using Godot;
using System;
using UISystem.Constants;
using UISystem.Core.MenuSystem;
using UISystem.Core.PopupSystem;
using UISystem.Core.Views;
using UISystem.MenuSystem.Views;
using UISystem.PopupSystem;
using UISystem.PopupSystem.Popups.Controllers;
using UISystem.ScreenFade;

namespace UISystem.MenuSystem.Controllers;
internal class PauseMenuController : MenuControllerBase<IViewCreator<PauseMenuView>, PauseMenuView>
{

    private readonly IPopupsManager<PopupResult> _popupsManager;
    private readonly ScreenFadeManager _screenFadeManager;
    private readonly MenuBackgroundController _menuBackgroundController;

    public PauseMenuController(IViewCreator<PauseMenuView> viewCreator, IMenuModel model, IMenusManager menusManager,
        IPopupsManager<PopupResult> popupsManager, ScreenFadeManager screenFadeManager, MenuBackgroundController menuBackgroundController) 
        : base(viewCreator, model, menusManager)
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
        _view.ResumeGameButton.ButtonDown += OnReturnButtonDown;
        _view.OptionsButton.ButtonDown += PressedOptions;
        _view.ReturnToMainMenuButton.ButtonDown += PressedReturn;
    }

    private void PressedOptions()
    {
        _view.SetLastSelectedElement(_view.OptionsButton);
        _menusManager.ShowMenu(typeof(OptionsMenuController));
    }

    private void PressedReturn()
    {
        _view.SetLastSelectedElement(_view.ReturnToMainMenuButton);
        SwitchInteractability(false);

        _popupsManager.ShowPopup(typeof(YesNoPopupController), PopupMessages.QuitToMainMenu, (result) =>
        {
            if (result == PopupResult.Yes)
            {
                _screenFadeManager.FadeOut(() =>
                {
                    _menusManager.ShowMenu(typeof(MainMenuController), StackingType.Clear, null, true);
                });
            }
            else if (result == PopupResult.No)
            {
                SwitchInteractability(true);
            }
        });
    }

}
