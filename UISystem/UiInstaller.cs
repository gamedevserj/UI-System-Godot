using Godot;
using System;
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
        var yesPopupViewCreator = new ViewCreator<YesPopupView>(GetPopupPath(typeof(YesPopupController)), popupsParent);
        var yesNoPopupViewCreator = new ViewCreator<YesNoPopupView>(GetPopupPath(typeof(YesNoPopupController)), popupsParent);
        var yesNoCancelPopupViewCreator = new ViewCreator<YesNoCancelPopupView>(GetPopupPath(typeof(YesNoCancelPopupController)), popupsParent);
        var popups = new IPopupController<PopupResult>[]
        {
            new YesPopupController(yesPopupViewCreator, popupsManager),
            new YesNoPopupController(yesNoPopupViewCreator, popupsManager),
            new YesNoCancelPopupController(yesNoCancelPopupViewCreator, popupsManager)
        };
        popupsManager.Init(popups);

        var backgroundController = new MenuBackgroundController(GetTree(), menuBackground);

        var menusManager = new MenusManager();
        var mainMenuViewCreator = new ViewCreator<MainMenuView>(GetMenuPath(typeof(MainMenuController)), menusParent);
        var inGameMenuViewCreator = new ViewCreator<InGameMenuView>(GetMenuPath(typeof(InGameMenuController)), menusParent);
        var pauseViewCreator = new ViewCreator<PauseMenuView>(GetMenuPath(typeof(PauseMenuController)), menusParent);
        var optionsViewCreator = new ViewCreator<OptionsMenuView>(GetMenuPath(typeof(OptionsMenuController)), menusParent);
        var audioSettingsViewCreator = new ViewCreator<AudioSettingsMenuView>(GetMenuPath(typeof(AudioSettingsMenuController)), menusParent);
        var videoSettingsViewCreator = new ViewCreator<VideoSettingsMenuView>(GetMenuPath(typeof(VideoSettingsMenuController)), menusParent);
        var rebindKeysViewCreator = new ViewCreator<RebindKeysMenuView>(GetMenuPath(typeof(RebindKeysMenuController)), menusParent);
        var interfaceMenuViewCreator = new ViewCreator<InterfaceSettingsMenuView>(GetMenuPath(typeof(InterfaceSettingsMenuController)), menusParent);
        var menus = new IMenuController[]
        {
            new MainMenuController(mainMenuViewCreator, null, menusManager, tree, popupsManager, screenFadeManager, backgroundController),
            new InGameMenuController(inGameMenuViewCreator, new InGameMenuModel(), menusManager),
            new PauseMenuController(pauseViewCreator, null, menusManager, popupsManager, screenFadeManager, backgroundController),
            new OptionsMenuController(optionsViewCreator, null, menusManager),
            new AudioSettingsMenuController(audioSettingsViewCreator, new AudioSettingsMenuModel(settings), menusManager, popupsManager),
            new VideoSettingsMenuController(videoSettingsViewCreator, new VideoSettingsMenuModel(settings), menusManager, popupsManager),
            new RebindKeysMenuController(rebindKeysViewCreator, new RebindKeysMenuModel(settings), menusManager, popupsManager),
            new InterfaceSettingsMenuController(interfaceMenuViewCreator, new InterfaceSettingsMenuModel(settings), menusManager, popupsManager),
        };
        menusManager.Init(menus);
        menusManager.ShowMenu(typeof(MainMenuController), StackingType.Clear);
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
