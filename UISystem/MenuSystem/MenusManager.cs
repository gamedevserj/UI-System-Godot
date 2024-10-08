using Godot;
using UISystem.Core.MenuSystem.Enums;
using UISystem.Core.MenuSystem.Interfaces;
using UISystem.Core.PopupSystem;
using UISystem.MenuSystem.Constants;
using UISystem.MenuSystem.Controllers;
using UISystem.MenuSystem.Models;
using UISystem.ScreenFade;

namespace UISystem.Core.MenuSystem;
public partial class MenusManager : Control
{

    public void Init(GameSettings settings, PopupsManager popupsManager, ScreenFadeManager screenFadeManager, MenuBackgroundController menuBackgroundController)
    {
        SceneTree tree = GetTree();

        var controllers = new IMenuController[]
        {
            new MainMenuController(GetMenuView(MenuType.Main), new MainMenuModel(), this, tree, popupsManager, screenFadeManager, menuBackgroundController),
            new InGameMenuController(GetMenuView(MenuType.InGame), new InGameMenuModel(), this),
            new PauseMenuController(GetMenuView(MenuType.Pause), new PauseMenuModel(), this, popupsManager, screenFadeManager, menuBackgroundController),
            new OptionsMenuController(GetMenuView(MenuType.Options), new OptionsMenuModel(), this),
            new AudioSettingsMenuController(GetMenuView(MenuType.AudioSettings), new AudioSettingsMenuModel(settings), this, popupsManager),
            new VideoSettingsMenuController(GetMenuView(MenuType.VideoSettings), new VideoSettingsMenuModel(settings), this, popupsManager),
            new RebindKeysMenuController(GetMenuView(MenuType.RebindKeys), new RebindKeysMenuModel(settings), this, popupsManager),
            new InterfaceSettingsMenuController(GetMenuView(MenuType.InterfaceSettings), new InterfaceSettingsMenuModel(settings), this, popupsManager)
        };

        for (int i = 0; i < controllers.Length; i++)
        {
            _controllers.Add(controllers[i].MenuType, controllers[i]);
        }
    }

    private static string GetMenuView(MenuType menuType)
    {
        return MenuViewsPaths.Paths[menuType];
    }

}
