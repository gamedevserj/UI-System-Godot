using System;
using System.Threading.Tasks;
using AsyncAwaitBestPractices;
using UISystem.Constants;
using UISystem.Core.MenuSystem;
using UISystem.Core.PopupSystem;
using UISystem.Core.Views;
using UISystem.MenuSystem.Views;
using UISystem.PopupSystem.Popups.Views;
using UISystem.ScreenFade;

namespace UISystem.MenuSystem.Controllers;

/// <summary>
/// Pause menu controller.
/// </summary>
internal class PauseMenuController : MenuController<IViewCreator<PauseMenuView>, PauseMenuView>
{
    private readonly IPopupsManager _popupsManager;
    private readonly ScreenFadeManager _screenFadeManager;
    private readonly MenuBackgroundController _menuBackgroundController;

    /// <summary>
    /// Initializes a new instance of the <see cref="PauseMenuController"/> class.
    /// </summary>
    /// <param name="viewCreator">View creator.</param>
    /// <param name="menusManager">Menus manager.</param>
    /// <param name="popupsManager">Popups manager.</param>
    /// <param name="screenFadeManager">Screen fade manager.</param>
    /// <param name="menuBackgroundController">Menu background controller.</param>
    public PauseMenuController(
        IViewCreator<PauseMenuView> viewCreator,
        IMenusManager menusManager,
        IPopupsManager popupsManager,
        ScreenFadeManager screenFadeManager,
        MenuBackgroundController menuBackgroundController)
        : base(viewCreator, menusManager)
    {
        _popupsManager = popupsManager;
        _screenFadeManager = screenFadeManager;
        _menuBackgroundController = menuBackgroundController;
    }

    /// <inheritdoc/>
    public override async Task Show(bool instant = false)
    {
        _menuBackgroundController.ShowBackground(instant).SafeFireAndForget();
        await base.Show(instant);
    }

    /// <inheritdoc/>
    protected override void SetupElements()
    {
        View.ResumeGameButton.ButtonDown += OnReturnButtonDown;
        View.OptionsButton.ButtonDown += PressedOptions;
        View.ReturnToMainMenuButton.ButtonDown += PressedReturn;
    }

    private void PressedOptions()
    {
        View.SetLastSelectedElement(View.OptionsButton);
        MenusManager.ShowMenu<OptionsMenuView>().SafeFireAndForget();
    }

    private void PressedReturn()
    {
        View.SetLastSelectedElement(View.ReturnToMainMenuButton);
        SwitchInteractability(false);

        _popupsManager
            .ShowPopup<YesNoPopupView>(PopupMessages.QuitToMainMenu, async (result) =>
            {
                if (result == PopupResult.Yes)
                {
                    await _screenFadeManager.FadeOut();
                    await MenusManager.ShowMenu<MainMenuView>(StackingType.Clear, instant: true);
                    await _screenFadeManager.FadeIn();
                }
                else if (result == PopupResult.No)
                {
                    SwitchInteractability(true);
                }
            })
            .SafeFireAndForget();
    }
}
