using System;
using System.Threading.Tasks;
using UISystem.Constants;
using UISystem.Core.MenuSystem;
using UISystem.Core.PopupSystem;
using UISystem.Core.Views;
using UISystem.MenuSystem.Views;
using UISystem.PopupSystem;
using UISystem.PopupSystem.Popups.Views;
using UISystem.ScreenFade;

namespace UISystem.MenuSystem.Controllers;
internal class PauseMenuController : MenuControllerBase<IViewCreator<PauseMenuView>, PauseMenuView>
{

    private readonly IPopupsManager<PopupResult> _popupsManager;
    private readonly ScreenFadeManager _screenFadeManager;
    private readonly MenuBackgroundController _menuBackgroundController;

    public PauseMenuController(IViewCreator<PauseMenuView> viewCreator, IMenusManager menusManager,
        IPopupsManager<PopupResult> popupsManager, ScreenFadeManager screenFadeManager, MenuBackgroundController menuBackgroundController) 
        : base(viewCreator, menusManager)
    {
        _popupsManager = popupsManager;
        _screenFadeManager = screenFadeManager;
        _menuBackgroundController = menuBackgroundController;
    }

    public override async Task Show(Action onComplete = null, bool instant = false)
    {
        _menuBackgroundController.ShowBackground(instant);
        await base.Show(onComplete, instant);
    }

    public override async Task Hide(StackingType stackingType, Action onComplete = null, bool instant = false)
    {
        await base.Hide(stackingType, () =>
        {
            
        }, instant);
        if (stackingType != StackingType.Add)
            _menuBackgroundController.HideBackground(instant);

        onComplete?.Invoke();
    }

    protected override void SetupElements()
    {
        View.ResumeGameButton.ButtonDown += OnReturnButtonDown;
        View.OptionsButton.ButtonDown += PressedOptions;
        View.ReturnToMainMenuButton.ButtonDown += PressedReturn;
    }

    private void PressedOptions()
    {
        View.SetLastSelectedElement(View.OptionsButton);
        MenusManager.ShowMenu(typeof(OptionsMenuView));
    }

    private void PressedReturn()
    {
        View.SetLastSelectedElement(View.ReturnToMainMenuButton);
        SwitchInteractability(false);

        _popupsManager.ShowPopup(typeof(YesNoPopupView), PopupMessages.QuitToMainMenu, (result) =>
        {
            if (result == PopupResult.Yes)
            {
                _screenFadeManager.FadeOut(() =>
                {
                    MenusManager.ShowMenu(typeof(MainMenuView), StackingType.Clear, null, true);
                });
            }
            else if (result == PopupResult.No)
            {
                SwitchInteractability(true);
            }
        });
    }

}
