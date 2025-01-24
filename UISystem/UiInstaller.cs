﻿using Godot;
using UISystem.Core.PhysicalInput;
using UISystem.Core.MenuSystem;
using UISystem.Core.MenuSystem.Enums;
using UISystem.Core.MenuSystem.Interfaces;
using UISystem.Core.PopupSystem;
using UISystem.Core.PopupSystem.Interfaces;
using UISystem.PhysicalInput;
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
        var popups = new IPopupController<InputEvent>[]
        {
            new YesPopupController(GetPopupPath(PopupType.Yes), _popupsManager, popupsParent),
            new YesNoPopupController(GetPopupPath(PopupType.YesNo), _popupsManager, popupsParent),
            new YesNoCancelPopupController(GetPopupPath(PopupType.YesNoCancel), _popupsManager, popupsParent)
        };
        _popupsManager.Init(popups, inputProcessor);

        var backgroundController = new MenuBackgroundController(GetTree(), menuBackground);
        _menusManager = new MenusManager<InputEvent>();
        var menus = new IMenuController<InputEvent>[]
        {
            new MainMenuController(GetMenuPath(MenuType.Main), new MainMenuModel(), _menusManager, menusParent, tree, _popupsManager, screenFadeManager, backgroundController),
            new InGameMenuController(GetMenuPath(MenuType.InGame), new InGameMenuModel(), _menusManager, menusParent),
            new PauseMenuController(GetMenuPath(MenuType.Pause), new PauseMenuModel(), _menusManager, menusParent, _popupsManager, screenFadeManager, backgroundController),
            new OptionsMenuController(GetMenuPath(MenuType.Options), new OptionsMenuModel(), _menusManager, menusParent),
            new AudioSettingsMenuController(GetMenuPath(MenuType.AudioSettings), new AudioSettingsMenuModel(settings), _menusManager, menusParent, _popupsManager),
            new VideoSettingsMenuController(GetMenuPath(MenuType.VideoSettings), new VideoSettingsMenuModel(settings), _menusManager, menusParent, _popupsManager),
            new RebindKeysMenuController(GetMenuPath(MenuType.RebindKeys), new RebindKeysMenuModel(settings), _menusManager, menusParent, _popupsManager),
            new InterfaceSettingsMenuController(GetMenuPath(MenuType.InterfaceSettings), new InterfaceSettingsMenuModel(settings), _menusManager, menusParent, _popupsManager),
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
