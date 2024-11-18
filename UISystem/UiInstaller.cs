using Godot;
using UISystem.Core.MenuSystem.Enums;
using UISystem.Core.MenuSystem.Interfaces;
using UISystem.Core.MenuSystem;
using UISystem.MenuSystem;
using UISystem.MenuSystem.Constants;
using UISystem.MenuSystem.Controllers;
using UISystem.MenuSystem.Models;
using UISystem.PopupSystem;
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
        popupsManager.Init();

        var backgroundController = new MenuBackgroundController(GetTree(), menuBackground);
        SceneTree tree = GetTree();
        var controllers = new IMenuController[]
        {
            new MainMenuController(GetMenuView(MenuType.Main), new MainMenuModel(), menusManager, tree, popupsManager, screenFadeManager, backgroundController),
            new InGameMenuController(GetMenuView(MenuType.InGame), new InGameMenuModel(), menusManager),
            new PauseMenuController(GetMenuView(MenuType.Pause), new PauseMenuModel(), menusManager, popupsManager, screenFadeManager, backgroundController),
            new OptionsMenuController(GetMenuView(MenuType.Options), new OptionsMenuModel(), menusManager),
            new AudioSettingsMenuController(GetMenuView(MenuType.AudioSettings), new AudioSettingsMenuModel(settings), menusManager, popupsManager),
            new VideoSettingsMenuController(GetMenuView(MenuType.VideoSettings), new VideoSettingsMenuModel(settings), menusManager, popupsManager),
            new RebindKeysMenuController(GetMenuView(MenuType.RebindKeys), new RebindKeysMenuModel(settings), menusManager, popupsManager),
            new InterfaceSettingsMenuController(GetMenuView(MenuType.InterfaceSettings), new InterfaceSettingsMenuModel(settings), menusManager, popupsManager)
        };
        menusManager.Init(controllers);
        menusManager.ShowMenu(MenuType.Main, StackingType.Clear);
    }

    private static string GetMenuView(int menuType)
    {
        return MenuViewsPaths.Paths[menuType];
    }

}
