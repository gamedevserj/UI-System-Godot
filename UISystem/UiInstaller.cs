using Godot;
using UISystem.Core.MenuSystem;
using UISystem.Core.MenuSystem.Enums;
using UISystem.Core.MenuSystem.Interfaces;
using UISystem.Core.PopupSystem;
using UISystem.Core.PopupSystem.Interfaces;
using UISystem.MenuSystem;
using UISystem.MenuSystem.Constants;
using UISystem.MenuSystem.Controllers;
using UISystem.MenuSystem.Models;
using UISystem.PopupSystem;
using UISystem.PopupSystem.Constants;
using UISystem.PopupSystem.Controllers;
using UISystem.ScreenFade;

namespace UISystem;
public partial class UiInstaller : Node
{

    public static UiInstaller Instance { get; private set; }

    [Export] private TextureRect menuBackground;
    [Export] private Node menusParent;
    [Export] private PopupsManager popupsManager;
    [Export] private ScreenFadeManager screenFadeManager;
    [Export] private GuiPanel3D guiPanel3D;

    private IMenusManager _menusManager;

    public override void _EnterTree()
    {
        Instance ??= this;
    }

    public override void _Input(InputEvent @event)
    {
        _menusManager?.ProcessInput(@event);
    }

    public void Init(GameSettings settings)
    {
        SceneTree tree = GetTree();

        var popups = new IPopupController[]
        {
            new YesPopupController(GetPopupPath(PopupType.Yes), popupsManager, popupsManager, tree),
            new YesNoPopupController(GetPopupPath(PopupType.YesNo), popupsManager, popupsManager, tree),
            new YesNoCancelPopupController(GetPopupPath(PopupType.YesNoCancel), popupsManager, popupsManager, tree)
        };
        popupsManager.Init(popups);

        var backgroundController = new MenuBackgroundController(GetTree(), menuBackground);
        _menusManager = new MenusManager();
        var menus = new IMenuController[]
        {
            new MainMenuController(GetMenuPath(MenuType.Main), new MainMenuModel(), _menusManager, menusParent, tree, popupsManager, screenFadeManager, backgroundController),
            new InGameMenuController(GetMenuPath(MenuType.InGame), new InGameMenuModel(), _menusManager, menusParent),
            new PauseMenuController(GetMenuPath(MenuType.Pause), new PauseMenuModel(), _menusManager, menusParent, popupsManager, screenFadeManager, backgroundController),
            new OptionsMenuController(GetMenuPath(MenuType.Options), new OptionsMenuModel(), _menusManager, menusParent),
            new AudioSettingsMenuController(GetMenuPath(MenuType.AudioSettings), new AudioSettingsMenuModel(settings), _menusManager, menusParent, popupsManager),
            new VideoSettingsMenuController(GetMenuPath(MenuType.VideoSettings), new VideoSettingsMenuModel(settings), _menusManager, menusParent, popupsManager),
            new RebindKeysMenuController(GetMenuPath(MenuType.RebindKeys), new RebindKeysMenuModel(settings), _menusManager, menusParent, popupsManager),
            new InterfaceSettingsMenuController(GetMenuPath(MenuType.InterfaceSettings), new InterfaceSettingsMenuModel(settings), _menusManager, menusParent, popupsManager),
        };
        _menusManager.Init(menus);
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
