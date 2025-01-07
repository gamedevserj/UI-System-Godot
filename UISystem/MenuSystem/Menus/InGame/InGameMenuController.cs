﻿using Godot;
using UISystem.Core.Constants;
using UISystem.Core.MenuSystem.Controllers;
using UISystem.Core.MenuSystem.Interfaces;
using UISystem.MenuSystem.Constants;
using UISystem.MenuSystem.Views;

namespace UISystem.MenuSystem.Controllers;
internal class InGameMenuController : MenuController<InGameMenuView, IMenuModel>
{

    public override int Type => MenuType.InGame;

    public InGameMenuController(string prefab, IMenuModel model, IMenusManager menusManager, Node parent) 
        : base(prefab, model, menusManager, parent)
    { }

    public override void HandleInputPressedWhenActive(InputEvent key)
    {
        if (key.IsPressed() && key.IsAction(InputsData.PauseButton))
            PauseGame();
    }

    private void PauseGame()
    {
        _menusManager.ShowMenu(MenuType.Pause);
    }

    protected override void SetupElements()
    {

    }
}