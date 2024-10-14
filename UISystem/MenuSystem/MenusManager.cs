using Godot;
using System;
using System.Collections.Generic;
using UISystem.Core.MenuSystem.Enums;
using UISystem.Core.MenuSystem.Interfaces;
using UISystem.Core.PopupSystem.Interfaces;
using UISystem.MenuSystem.Constants;
using UISystem.MenuSystem.Controllers;
using UISystem.MenuSystem.Models;
using UISystem.ScreenFade;

namespace UISystem.MenuSystem;
public partial class MenusManager : Control, IMenusManager
{

    private IMenuController _currentController;
    private Stack<IMenuController> _previousMenus = new();
    private Dictionary<int, IMenuController> _controllers = new();

    public override void _Input(InputEvent @event)
    {
        _currentController?.HandleInputPressedWhenActive(@event);
    }

    public void Init(GameSettings settings, IPopupsManager popupsManager, ScreenFadeManager screenFadeManager, MenuBackgroundController menuBackgroundController)
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
            _controllers.Add(controllers[i].Type, controllers[i]);
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="menuType"></param>
    /// <param name="menuChangeType">Is used to show/hide background</param>
    public void ShowMenu(int menuType, StackingType stackingType = StackingType.Add, Action onNewMenuShown = null, bool instant = false)
    {
        if (_currentController?.Type == menuType)
            return;

        if (_currentController != null)
        {
            _currentController.Hide(stackingType, () => ChangeMenu(menuType, stackingType, onNewMenuShown, instant), instant);
        }
        else
        {
            ChangeMenu(menuType, stackingType, onNewMenuShown, instant);
        }
    }

    public void ReturnToPreviousMenu(Action onComplete = null, bool instant = false)
    {
        if (_previousMenus.Count > 0)
        {
            ShowMenu(_previousMenus.Peek().Type, StackingType.Remove, onComplete, instant);
        }
    }

    private void ChangeMenu(int menuType, StackingType stackingType, Action onNewMenuShown = null, bool instant = false)
    {
        IMenuController controller = _controllers[menuType];
        controller.Init(this);

        switch (stackingType)
        {
            case StackingType.Add:
                _previousMenus.Push(_currentController);
                break;
            case StackingType.Remove:
                _currentController.DestroyView();
                _previousMenus.Pop();
                break;
            case StackingType.Clear:
                foreach (var menuController in _previousMenus)
                {
                    menuController.DestroyView();
                }
                _previousMenus.Clear();
                _currentController?.DestroyView();
                break;
            default:
                break;
        }
        _currentController = controller;

        _currentController.Show(() =>
        {
            onNewMenuShown?.Invoke();
        }, instant);
    }

    private static string GetMenuView(int menuType)
    {
        return MenuViewsPaths.Paths[menuType];
    }

}
