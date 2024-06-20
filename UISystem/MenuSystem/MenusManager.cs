using Godot;
using System;
using System.Collections.Generic;
using UISystem.MenuSystem.Constants;
using UISystem.MenuSystem.Controllers;
using UISystem.MenuSystem.Enums;
using UISystem.MenuSystem.Interfaces;
using UISystem.MenuSystem.Models;
using UISystem.PopupSystem;
using UISystem.ScreenFade;
using UISystem.UISystem.MenuSystem.Controllers;

namespace UISystem.MenuSystem;
public partial class MenusManager : Control
{
    
    private IMenuController _currentController;
    private Stack<IMenuController> _previousMenus;
    private Dictionary<MenuType, IMenuController> _controllers;

    public override void _Input(InputEvent @event)
    {
        if (@event.IsPressed())
        {
            
        }
        _currentController?.HandleInputPressedWhenActive(@event);
    }

    public void Init(ConfigFile config, GameSettings settings, PopupsManager popupsManager, ScreenFadeManager screenFadeManager, 
        MenuBackgroundController menuBackgroundController)
    {
        _previousMenus = new Stack<IMenuController>();
        _controllers = new Dictionary<MenuType, IMenuController>();

        SceneTree tree = GetTree();
        
        AddMenus(new IMenuController[]
        {
            new MainMenuController(MenuViewsPaths.Main, new MainMenuModel(), this, tree, popupsManager, screenFadeManager, menuBackgroundController),
            new InGameMenuController(MenuViewsPaths.InGame, new InGameMenuModel(), this),
            new PauseMenuController(MenuViewsPaths.Pause, new PauseMenuModel(), this, popupsManager, screenFadeManager, menuBackgroundController),
            new OptionsMenuController(MenuViewsPaths.Options, new OptionsMenuModel(), this),
            new AudioSettingsMenuController(MenuViewsPaths.AudioSettings, new AudioSettingsMenuModel(config), this, popupsManager),
            new VideoSettingsMenuController(MenuViewsPaths.VideoSettings, new VideoSettingsMenuModel(config), this, popupsManager),
            new RebindKeysMenuController(MenuViewsPaths.RebindKeys, new RebindKeysMenuModel(config), this, popupsManager),
            new InterfaceSettingsMenuController(MenuViewsPaths.InterfaceSettings, new InterfaceSettingsMenuModel(config, settings), this, popupsManager)
        });       
    }

    /// <summary>
    /// Shows new menu, <paramref name="stackBehaviour"/> is controlled by MenuController to dispose of the view
    /// </summary>
    /// <param name="menuType"></param>
    /// <param name="stackBehaviour"></param>
    public void ChangeMenu(MenuType menuType, MenuStackBehaviourEnum stackBehaviour,
        Action onNewMenuShown = null, bool instant = false)
    {
        if (_currentController?.MenuType == menuType)
            return;

        if (_currentController != null)
        {
            _currentController.Hide(stackBehaviour, () =>
            {
                ShowMenu(menuType, stackBehaviour, onNewMenuShown);
            }, instant);
        }
        else
        {
            ShowMenu(menuType, stackBehaviour, onNewMenuShown);
        }
    }

    public void ReturnToPreviousMenu(Action onComplete = null)
    {
        if (_currentController.CanReturnToPreviousMenu && _previousMenus.Count > 0)
        {
            ChangeMenu(_previousMenus.Peek().MenuType, MenuStackBehaviourEnum.RemoveFromStack, onComplete);
        }
    }

    private void AddMenus(IMenuController[] controllers)
    {
        for (int i = 0; i < controllers.Length; i++)
        {
            _controllers.Add(controllers[i].MenuType, controllers[i]);
        }
    }

    private void ShowMenu(MenuType menuType, MenuStackBehaviourEnum stackBehaviour,
        Action onNewMenuShown = null, bool instant = false)
    {
        IMenuController controller = _controllers[menuType];
        controller.Init(this);

        switch (stackBehaviour)
        {
            case MenuStackBehaviourEnum.AddToStack:
                _previousMenus.Push(_currentController);
                break;
            case MenuStackBehaviourEnum.RemoveFromStack:
                _currentController.DestroyView();
                _previousMenus.Pop();
                break;
            case MenuStackBehaviourEnum.ClearStack:
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
} 
