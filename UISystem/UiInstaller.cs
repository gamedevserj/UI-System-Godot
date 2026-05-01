using System;
using System.Collections.Generic;
using AsyncAwaitBestPractices;
using Godot;
using UISystem.Core.MenuSystem;
using UISystem.Core.PhysicalInput;
using UISystem.Core.PopupSystem;
using UISystem.MenuSystem;
using UISystem.MenuSystem.Constants;
using UISystem.MenuSystem.Controllers;
using UISystem.MenuSystem.Models;
using UISystem.MenuSystem.Views;
using UISystem.PhysicalInput;
using UISystem.PopupSystem.Constants;
using UISystem.PopupSystem.Popups.Controllers;
using UISystem.PopupSystem.Popups.Views;
using UISystem.ScreenFade;
using UISystem.Views;

namespace UISystem;

/// <summary>
/// UI installer.
/// </summary>
public partial class UiInstaller : Node
{
    [Export] private TextureRect _menuBackground;
    [Export] private Node _menusParent;
    [Export] private Node _popupsParent;
    [Export] private ScreenFadeManager _screenFadeManager;
    [Export] private GuiPanel3D _guiPanel3D;

    private IInputProcessor<InputEvent> _inputProcessor;

    /// <summary>
    /// Gets instance of the UiInstaller.
    /// </summary>
    public static UiInstaller Instance { get; private set; }

    /// <inheritdoc/>
    public override void _EnterTree()
    {
        Instance ??= this;
    }

    /// <inheritdoc/>
    public override void _Input(InputEvent @event)
    {
        _inputProcessor?.ProcessInput(@event);
    }

    /// <summary>
    /// Initializes UI.
    /// </summary>
    /// <param name="settings">Game settings.</param>
    public void Init(GameSettings settings)
    {
        SceneTree tree = GetTree();

        var popupsManager = new PopupsManager();
        var yesPopupViewCreator = new ViewCreator<YesPopupView>(GetPopupPath(typeof(YesPopupView)), _popupsParent);
        var yesNoPopupViewCreator = new ViewCreator<YesNoPopupView>(GetPopupPath(typeof(YesNoPopupView)), _popupsParent);
        var yesNoCancelPopupViewCreator = new ViewCreator<YesNoCancelPopupView>(GetPopupPath(typeof(YesNoCancelPopupView)), _popupsParent);
        var popups = new Dictionary<Type, IPopupController>
        {
            {
                typeof(YesPopupView),
                new YesPopupController(yesPopupViewCreator, popupsManager)
            },
            {
                typeof(YesNoPopupView),
                new YesNoPopupController(yesNoPopupViewCreator, popupsManager)
            },
            {
                typeof(YesNoCancelPopupView),
                new YesNoCancelPopupController(yesNoCancelPopupViewCreator, popupsManager)
            },
        };
        popupsManager.Init(popups);

        var backgroundController = new MenuBackgroundController(GetTree(), _menuBackground);

        var menusManager = new MenusManager();
        var mainMenuViewCreator = new ViewCreator<MainMenuView>(GetMenuPath(typeof(MainMenuView)), _menusParent);
        var inGameMenuViewCreator = new ViewCreator<InGameMenuView>(GetMenuPath(typeof(InGameMenuView)), _menusParent);
        var pauseViewCreator = new ViewCreator<PauseMenuView>(GetMenuPath(typeof(PauseMenuView)), _menusParent);
        var optionsViewCreator = new ViewCreator<OptionsMenuView>(GetMenuPath(typeof(OptionsMenuView)), _menusParent);
        var audioSettingsViewCreator = new ViewCreator<AudioSettingsMenuView>(GetMenuPath(typeof(AudioSettingsMenuView)), _menusParent);
        var videoSettingsViewCreator = new ViewCreator<VideoSettingsMenuView>(GetMenuPath(typeof(VideoSettingsMenuView)), _menusParent);
        var rebindKeysViewCreator = new ViewCreator<RebindKeysMenuView>(GetMenuPath(typeof(RebindKeysMenuView)), _menusParent);
        var interfaceMenuViewCreator = new ViewCreator<InterfaceSettingsMenuView>(GetMenuPath(typeof(InterfaceSettingsMenuView)), _menusParent);

        var menus = new Dictionary<Type, IMenuController>
        {
            {
                typeof(MainMenuView),
                new MainMenuController(
                    mainMenuViewCreator,
                    menusManager,
                    tree,
                    popupsManager,
                    _screenFadeManager,
                    backgroundController)
            },
            {
                typeof(InGameMenuView),
                new InGameMenuController(inGameMenuViewCreator, menusManager)
            },
            {
                typeof(PauseMenuView),
                new PauseMenuController(
                    pauseViewCreator,
                    menusManager,
                    popupsManager,
                    _screenFadeManager,
                    backgroundController)
            },
            {
                typeof(OptionsMenuView),
                new OptionsMenuController(optionsViewCreator, menusManager)
            },
            {
                typeof(AudioSettingsMenuView),
                new AudioSettingsMenuController(
                    audioSettingsViewCreator,
                    menusManager,
                    new AudioSettingsMenuModel(settings),
                    popupsManager)
            },
            {
                typeof(VideoSettingsMenuView),
                new VideoSettingsMenuController(
                    videoSettingsViewCreator,
                    menusManager,
                    new VideoSettingsMenuModel(settings),
                    popupsManager)
            },
            {
                typeof(RebindKeysMenuView),
                new RebindKeysMenuController(
                    rebindKeysViewCreator,
                    menusManager,
                    new RebindKeysMenuModel(settings),
                    popupsManager)
            },
            {
                typeof(InterfaceSettingsMenuView),
                new InterfaceSettingsMenuController(
                    interfaceMenuViewCreator,
                    menusManager,
                    new InterfaceSettingsMenuModel(settings),
                    popupsManager)
            },
        };
        menusManager.Init(menus);
        menusManager.ShowMenu(typeof(MainMenuView), StackingType.Clear).SafeFireAndForget();
        _inputProcessor = new InputProcessor(menusManager, popupsManager);
    }

    private static string GetMenuPath(Type menuType) => MenuViewsPaths.Paths[menuType];

    private static string GetPopupPath(Type type) => PopupViewsPaths.Paths[type];
}
