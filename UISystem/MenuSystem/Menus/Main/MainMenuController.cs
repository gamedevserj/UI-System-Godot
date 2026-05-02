using System;
using System.Threading.Tasks;
using AsyncAwaitBestPractices;
using Godot;
using UISystem.Constants;
using UISystem.Core.MenuSystem;
using UISystem.Core.PopupSystem;
using UISystem.Core.Views;
using UISystem.MenuSystem.Views;
using UISystem.PopupSystem.Popups.Views;
using UISystem.ScreenFade;

namespace UISystem.MenuSystem.Controllers;

/// <summary>
/// Main menu controller.
/// </summary>
internal class MainMenuController : MenuController<IViewCreator<MainMenuView>, MainMenuView>
{
    private readonly SceneTree _sceneTree;
    private readonly IPopupsManager _popupsManager;
    private readonly MenuBackgroundController _menuBackgroundController;
    private readonly ScreenFadeManager _screenFadeManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainMenuController"/> class.
    /// </summary>
    /// <param name="viewCreator">View creator.</param>
    /// <param name="menusManager">Menus manager.</param>
    /// <param name="sceneTree">Scene tree.</param>
    /// <param name="popupsManager">Popups manager.</param>
    /// <param name="screenFadeManager">Screen fade manager.</param>
    /// <param name="menuBackgroundController">Menu background controller.</param>
    public MainMenuController(
        IViewCreator<MainMenuView> viewCreator,
        IMenusManager menusManager,
        SceneTree sceneTree,
        IPopupsManager popupsManager,
        ScreenFadeManager screenFadeManager,
        MenuBackgroundController menuBackgroundController)
        : base(viewCreator, menusManager)
    {
        _sceneTree = sceneTree;
        _popupsManager = popupsManager;
        _screenFadeManager = screenFadeManager;
        _menuBackgroundController = menuBackgroundController;
    }

    /// <inheritdoc/>
    public override async Task Show(Action onComplete = null, bool instant = false)
    {
        _menuBackgroundController.ShowBackground(instant).SafeFireAndForget();
        await base.Show(onComplete, instant);
    }

    /// <inheritdoc/>
    public override async Task Hide(StackingType stackingType, Action onComplete = null, bool instant = false)
    {
        if (stackingType != StackingType.Add)
            _menuBackgroundController.HideBackground(instant).SafeFireAndForget();
        await base.Hide(stackingType, onComplete, instant);
    }

    /// <inheritdoc/>
    public override void OnReturnButtonDown()
    {
        if (CanReturnToPreviousMenu)
            ShowQuitPopup().SafeFireAndForget();
    }

    /// <inheritdoc/>
    protected override void SetupElements()
    {
        View.PlayButton.ButtonDown += async () => await PressedPlay();
        View.OptionsButton.ButtonDown += PressedOptions;
        View.QuitButton.ButtonDown += PressedQuit;
    }

    private async Task PressedPlay()
    {
        View.SetLastSelectedElement(View.PlayButton);
        await _screenFadeManager.FadeOut();
        await MenusManager.ShowMenu(typeof(InGameMenuView), StackingType.Clear, instant: true);
        await _screenFadeManager.FadeIn();
    }

    private void PressedOptions()
    {
        View.SetLastSelectedElement(View.OptionsButton);
        MenusManager.ShowMenu(typeof(OptionsMenuView)).SafeFireAndForget();
    }

    private void PressedQuit()
    {
        View.SetLastSelectedElement(View.QuitButton);
        ShowQuitPopup().SafeFireAndForget();
    }

    private async Task ShowQuitPopup()
    {
        SwitchInteractability(false);
        await _popupsManager.ShowPopup(typeof(YesNoPopupView), PopupMessages.QuitGame, (result) =>
        {
            if (result == PopupResult.Yes)
                _sceneTree.Quit();
            else if (result == PopupResult.No)
                SwitchInteractability(true);
        });
    }
}
