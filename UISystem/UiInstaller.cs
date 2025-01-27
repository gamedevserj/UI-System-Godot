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
using UISystem.PopupSystem;
using UISystem.PopupSystem.Constants;
using UISystem.PopupSystem.Popups.Controllers;
using UISystem.PopupSystem.Popups.ViewHandlers;
using UISystem.PopupSystem.Popups.Views;
using UISystem.ScreenFade;

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

        var popupsManager = new PopupsManager<InputEvent>();
        var yesPopupViewHandler = new YesPopupViewCreator<YesPopupView>(GetPopupPath(PopupType.Yes), popupsParent);
        var yesNoPopupViewHandler = new YesNoPopupViewCreator<YesNoPopupView>(GetPopupPath(PopupType.YesNo), popupsParent);
        var yesNoCancelPopupViewHandler = new YesNoCancelPopupViewCreator<YesNoCancelPopupView>(GetPopupPath(PopupType.YesNoCancel), popupsParent);
        var popups = new IPopupController<InputEvent>[]
        {
            new YesPopupController<YesPopupView, InputEvent>(yesPopupViewHandler, popupsManager),
            new YesNoPopupController<YesNoPopupView, InputEvent>(yesNoPopupViewHandler, popupsManager),
            new YesNoCancelPopupController<YesNoCancelPopupView, InputEvent>(yesNoCancelPopupViewHandler, popupsManager)
        };
        popupsManager.Init(popups);

        var backgroundController = new MenuBackgroundController(GetTree(), menuBackground);

        var menusManager = new MenusManager<InputEvent>();
        //var test = new MenuViewCreator<MainMenuView>(GetMenuPath(MenuType.Main), menusParent);
        var mainMenuViewHandler = new MenuViewCreator<MainMenuView>(GetMenuPath(MenuType.Main), menusParent);
        var inGameMenuViewHandler = new MenuViewCreator<InGameMenuView>(GetMenuPath(MenuType.InGame), menusParent);
        var pauseViewHandler = new MenuViewCreator<PauseMenuView>(GetMenuPath(MenuType.Pause), menusParent);
        var optionsViewHandler = new MenuViewCreator<OptionsMenuView>(GetMenuPath(MenuType.Options), menusParent);
        var audioSettingsViewHandler = new MenuViewCreator<AudioSettingsMenuView>(GetMenuPath(MenuType.AudioSettings), menusParent);
        var videoSettingsViewHandler = new MenuViewCreator<VideoSettingsMenuView>(GetMenuPath(MenuType.VideoSettings), menusParent);
        var rebindKeysViewHandler = new MenuViewCreator<RebindKeysMenuView>(GetMenuPath(MenuType.RebindKeys), menusParent);
        var interfaceMenuViewHandler = new MenuViewCreator<InterfaceSettingsMenuView>(GetMenuPath(MenuType.InterfaceSettings), menusParent);
        var menus = new IMenuController<InputEvent>[]
        {
            new MainMenuController<MainMenuView, InputEvent>(mainMenuViewHandler, null, menusManager, tree, popupsManager, screenFadeManager, backgroundController),
            new InGameMenuController<InGameMenuView, InputEvent>(inGameMenuViewHandler, new InGameMenuModel(), menusManager),
            new PauseMenuController<PauseMenuView, InputEvent>(pauseViewHandler, null, menusManager, popupsManager, screenFadeManager, backgroundController),
            new OptionsMenuController<OptionsMenuView, InputEvent>(optionsViewHandler, null, menusManager),
            new AudioSettingsMenuController<AudioSettingsMenuView, InputEvent>(audioSettingsViewHandler, new AudioSettingsMenuModel(settings), menusManager, popupsManager),
            new VideoSettingsMenuController<VideoSettingsMenuView, InputEvent>(videoSettingsViewHandler, new VideoSettingsMenuModel(settings), menusManager, popupsManager),
            new RebindKeysMenuController<RebindKeysMenuView, InputEvent>(rebindKeysViewHandler, new RebindKeysMenuModel(settings), menusManager, popupsManager),
            new InterfaceSettingsMenuController<InterfaceSettingsMenuView, InputEvent>(interfaceMenuViewHandler, new InterfaceSettingsMenuModel(settings), menusManager, popupsManager),
        };
        menusManager.Init(menus);
        menusManager.ShowMenu(MenuType.Main, StackingType.Clear);
    }

    private static string GetMenuPath(int menuType)
    {
        return MenuViewsPaths.Paths[menuType];
    }

    private static string GetPopupPath(int type)
    {
        return PopupViewsPaths.Paths[type];
    }

}
