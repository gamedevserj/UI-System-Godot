using Godot;
using UISystem.Core.MenuSystem;
using UISystem.Core.MenuSystem.Enums;
using UISystem.Core.MenuSystem.Interfaces;
using UISystem.Core.PhysicalInput;
using UISystem.Core.PopupSystem;
using UISystem.Core.PopupSystem.Interfaces;
using UISystem.MenuSystem;
using UISystem.MenuSystem.Constants;
using UISystem.MenuSystem.Controllers;
using UISystem.MenuSystem.Models;
using UISystem.MenuSystem.ViewHandlers;
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

    private IMenusManager<InputEvent> _menusManager;
    private IPopupsManager<InputEvent> _popupsManager;

    public override void _EnterTree()
    {
        Instance ??= this;
    }

    public override void _Input(InputEvent @event)
    {
        _menusManager?.ProcessInput(@event);
        _popupsManager?.ProcessInput(@event);
    }

    public void Init(GameSettings settings)
    {
        SceneTree tree = GetTree();

        IInputProcessor<InputEvent> inputProcessor = new InputProcessor();
        _popupsManager = new PopupsManager<InputEvent>();
        var yesPopupViewHandler = new YesPopupViewHandler<YesPopupView>(GetPopupPath(PopupType.Yes), popupsParent);
        var yesNoPopupViewHandler = new YesNoPopupViewHandler<YesNoPopupView>(GetPopupPath(PopupType.YesNo), popupsParent);
        var yesNoCancelPopupViewHandler = new YesNoCancelPopupViewHandler<YesNoCancelPopupView>(GetPopupPath(PopupType.YesNoCancel), popupsParent);
        var popups = new IPopupController<InputEvent>[]
        {
            new YesPopupController<YesPopupView, InputEvent>(yesPopupViewHandler, _popupsManager),
            new YesNoPopupController<YesNoPopupView, InputEvent>(yesNoPopupViewHandler, _popupsManager),
            new YesNoCancelPopupController<YesNoCancelPopupView, InputEvent>(yesNoCancelPopupViewHandler, _popupsManager)
        };
        _popupsManager.Init(popups, inputProcessor);

        var backgroundController = new MenuBackgroundController(GetTree(), menuBackground);
        _menusManager = new MenusManager<InputEvent>();
        var mainMenuViewHandler = new MainMenuViewHandler<MainMenuView>(GetMenuPath(MenuType.Main), menusParent);
        var inGameMenuViewHandler = new InGameMenuViewHandler<InGameMenuView>(GetMenuPath(MenuType.InGame), menusParent);
        var pauseViewHandler = new PauseMenuViewHandler<PauseMenuView>(GetMenuPath(MenuType.Pause), menusParent);
        var optionsViewHandler = new OptionsMenuViewHandler<OptionsMenuView>(GetMenuPath(MenuType.Options), menusParent);
        var audioSettingsViewHandler = new AudioSettingsMenuViewHandler<AudioSettingsMenuView>(GetMenuPath(MenuType.AudioSettings), menusParent);
        var videoSettingsViewHandler = new VideoSettingsMenuViewHandler<VideoSettingsMenuView>(GetMenuPath(MenuType.VideoSettings), menusParent);
        var rebindKeysViewHandler = new RebindKeysMenuViewHandler<RebindKeysMenuView>(GetMenuPath(MenuType.RebindKeys), menusParent);
        var interfaceMenuViewHandler = new InterfaceSettingsMenuViewHandler<InterfaceSettingsMenuView>(GetMenuPath(MenuType.InterfaceSettings), menusParent);
        var menus = new IMenuController<InputEvent>[]
        {
            new MainMenuController<MainMenuView, InputEvent>(mainMenuViewHandler, null, _menusManager, tree, _popupsManager, screenFadeManager, backgroundController),
            //new MainMenuController(GetMenuPath(MenuType.Main), new MainMenuModel(), _menusManager, menusParent, tree, _popupsManager, screenFadeManager, backgroundController),
            new InGameMenuController<InGameMenuView, InputEvent>(inGameMenuViewHandler, new InGameMenuModel(), _menusManager),
            new PauseMenuController<PauseMenuView, InputEvent>(pauseViewHandler, null, _menusManager, _popupsManager, screenFadeManager, backgroundController),
            new OptionsMenuController<OptionsMenuView, InputEvent>(optionsViewHandler, null, _menusManager),
            new AudioSettingsMenuController<AudioSettingsMenuView, InputEvent>(audioSettingsViewHandler, new AudioSettingsMenuModel(settings), _menusManager, _popupsManager),
            new VideoSettingsMenuController<VideoSettingsMenuView, InputEvent>(videoSettingsViewHandler, new VideoSettingsMenuModel(settings), _menusManager, _popupsManager),
            new RebindKeysMenuController<RebindKeysMenuView, InputEvent>(rebindKeysViewHandler, new RebindKeysMenuModel(settings), _menusManager, _popupsManager),
            new InterfaceSettingsMenuController<InterfaceSettingsMenuView, InputEvent>(interfaceMenuViewHandler, new InterfaceSettingsMenuModel(settings), _menusManager, _popupsManager),
        };
        _menusManager.Init(menus, inputProcessor);
        _menusManager.ShowMenu(MenuType.Main, StackingType.Clear);
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
