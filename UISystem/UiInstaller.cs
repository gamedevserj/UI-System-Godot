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
public partial class UiInstaller : Control
{

    public static UiInstaller Instance { get; private set; }

    [Export] private TextureRect menuBackground;
    [Export] private MenusManager menusManager;
    [Export] private PopupsManager popupsManager;
    [Export] private ScreenFadeManager screenFadeManager;

    public override void _EnterTree()
    {
        Instance ??= this;
    }

    public void Init(GameSettings settings)
    {
        SceneTree tree = GetTree();

        var popups = new IPopupController[]
        {
            new YesPopupController(GetPopupPath(PopupType.Yes), popupsManager, tree),
            new YesNoPopupController(GetPopupPath(PopupType.YesNo), popupsManager, tree),
            new YesNoCancelPopupController(GetPopupPath(PopupType.YesNoCancel), popupsManager, tree)
        };
        popupsManager.Init(popups);

        var backgroundController = new MenuBackgroundController(GetTree(), menuBackground);
        var menus = new IMenuController[]
        {
            new MainMenuController(GetMenuPath(MenuType.Main), new MainMenuModel(), menusManager, menusManager, tree, popupsManager, screenFadeManager, backgroundController),
            new InGameMenuController(GetMenuPath(MenuType.InGame), new InGameMenuModel(), menusManager, menusManager),
            new PauseMenuController(GetMenuPath(MenuType.Pause), new PauseMenuModel(), menusManager, menusManager, popupsManager, screenFadeManager, backgroundController),
            new OptionsMenuController(GetMenuPath(MenuType.Options), new OptionsMenuModel(), menusManager, menusManager),
            new AudioSettingsMenuController(GetMenuPath(MenuType.AudioSettings), new AudioSettingsMenuModel(settings), menusManager, menusManager, popupsManager),
            new VideoSettingsMenuController(GetMenuPath(MenuType.VideoSettings), new VideoSettingsMenuModel(settings), menusManager, menusManager, popupsManager),
            new RebindKeysMenuController(GetMenuPath(MenuType.RebindKeys), new RebindKeysMenuModel(settings), menusManager, menusManager, popupsManager),
            new InterfaceSettingsMenuController(GetMenuPath(MenuType.InterfaceSettings), new InterfaceSettingsMenuModel(settings), menusManager, menusManager, popupsManager)
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
