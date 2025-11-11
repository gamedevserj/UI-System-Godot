using Godot;
using System;
using System.Collections.Generic;
using UISystem.Core.MenuSystem;
using UISystem.Core.PhysicalInput;
using UISystem.Core.PopupSystem;
using UISystem.MenuSystem;
using UISystem.MenuSystem.Constants;
using UISystem.MenuSystem.Controllers;
using UISystem.MenuSystem.Models;
using UISystem.MenuSystem.Views;
using UISystem.PhysicalInput;
using UISystem.PopupSystem;
using UISystem.PopupSystem.Constants;
using UISystem.PopupSystem.Popups.Controllers;
using UISystem.PopupSystem.Popups.Views;
using UISystem.ScreenFade;
using UISystem.Views;

namespace UISystem;
public partial class UiInstaller : Node
{

    public static UiInstaller Instance { get; private set; }

    [Export] private TextureRect menuBackground;
    [Export] private Node menusParent;
    [Export] private Node popupsParent;
    [Export] private ScreenFadeManager screenFadeManager;
    [Export] private GuiPanel3D guiPanel3D;

    IInputProcessor<InputEvent> _inputProcessor;

    public override void _EnterTree()
    {
        Instance ??= this;
    }

    public override void _Input(InputEvent inputEvent)
    {
        _inputProcessor?.ProcessInput(inputEvent);
    }

    public void Init(GameSettings settings)
    {
        SceneTree tree = GetTree();

        _inputProcessor = new InputProcessor();

        var popupsManager = new PopupsManager<PopupResult>();
        var yesPopupViewCreator = new ViewCreator<YesPopupView>(GetPopupPath(typeof(YesPopupView)), popupsParent);
        var yesNoPopupViewCreator = new ViewCreator<YesNoPopupView>(GetPopupPath(typeof(YesNoPopupView)), popupsParent);
        var yesNoCancelPopupViewCreator = new ViewCreator<YesNoCancelPopupView>(GetPopupPath(typeof(YesNoCancelPopupView)), popupsParent);
        var popups = new Dictionary<Type, IPopupController<PopupResult>>
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

        var backgroundController = new MenuBackgroundController(GetTree(), menuBackground);

        var menusManager = new MenusManager();
        var mainMenuViewCreator = new ViewCreator<MainMenuView>(GetMenuPath(typeof(MainMenuView)), menusParent);
        var inGameMenuViewCreator = new ViewCreator<InGameMenuView>(GetMenuPath(typeof(InGameMenuView)), menusParent);
        var pauseViewCreator = new ViewCreator<PauseMenuView>(GetMenuPath(typeof(PauseMenuView)), menusParent);
        var optionsViewCreator = new ViewCreator<OptionsMenuView>(GetMenuPath(typeof(OptionsMenuView)), menusParent);
        var audioSettingsViewCreator = new ViewCreator<AudioSettingsMenuView>(GetMenuPath(typeof(AudioSettingsMenuView)), menusParent);
        var videoSettingsViewCreator = new ViewCreator<VideoSettingsMenuView>(GetMenuPath(typeof(VideoSettingsMenuView)), menusParent);
        var rebindKeysViewCreator = new ViewCreator<RebindKeysMenuView>(GetMenuPath(typeof(RebindKeysMenuView)), menusParent);
        var interfaceMenuViewCreator = new ViewCreator<InterfaceSettingsMenuView>(GetMenuPath(typeof(InterfaceSettingsMenuView)), menusParent);

        var menus = new Dictionary<Type, IMenuController>
            {
                {
                    typeof(MainMenuView),
                    new MainMenuController(
                        mainMenuViewCreator, 
                        null, 
                        menusManager, 
                        tree, 
                        popupsManager, 
                        screenFadeManager, 
                        backgroundController)
                },
                {
                    typeof(InGameMenuView),
                    new InGameMenuController(inGameMenuViewCreator, new InGameMenuModel(), menusManager)
                },
                {
                    typeof(PauseMenuView),
                    new PauseMenuController(
                        pauseViewCreator,
                        null,
                        menusManager,
                        popupsManager,
                        screenFadeManager,
                        backgroundController)
                },
                {
                    typeof(OptionsMenuView),
                    new OptionsMenuController(optionsViewCreator, null, menusManager)
                },
                {
                    typeof(AudioSettingsMenuView),
                    new AudioSettingsMenuController(
                        audioSettingsViewCreator,
                        new AudioSettingsMenuModel(settings),
                        menusManager,
                        popupsManager)
                },
                {
                    typeof(VideoSettingsMenuView),
                    new VideoSettingsMenuController(
                        videoSettingsViewCreator,
                        new VideoSettingsMenuModel(settings),
                        menusManager,
                        popupsManager)
                },
                {
                    typeof(RebindKeysMenuView),
                    new RebindKeysMenuController(
                        rebindKeysViewCreator,
                        new RebindKeysMenuModel(settings),
                        menusManager,
                        popupsManager)
                },
                {
                    typeof(InterfaceSettingsMenuView),
                    new InterfaceSettingsMenuController(
                        interfaceMenuViewCreator,
                        new InterfaceSettingsMenuModel(settings),
                        menusManager,
                        popupsManager)
                },
            };
        menusManager.Init(menus);
        menusManager.ShowMenu(typeof(MainMenuView), StackingType.Clear);
    }

    private static string GetMenuPath(Type menuType)
    {
        return MenuViewsPaths.Paths[menuType];
    }

    private static string GetPopupPath(Type type)
    {
        return PopupViewsPaths.Paths[type];
    }

}
