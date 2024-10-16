﻿using Godot;
using UISystem.Core.MenuSystem.Enums;
using UISystem.MenuSystem;
using UISystem.MenuSystem.Constants;
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
        menusManager.Init(settings, popupsManager, screenFadeManager, new MenuBackgroundController(GetTree(), menuBackground));
        menusManager.ShowMenu(MenuType.Main, StackingType.Clear);
    }

}
