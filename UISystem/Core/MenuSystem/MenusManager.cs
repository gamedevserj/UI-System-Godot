using Godot;
using System;
using System.Collections.Generic;
using UISystem.Core.MenuSystem.Enums;
using UISystem.Core.MenuSystem.Interfaces;

namespace UISystem.Core.MenuSystem;
public partial class MenusManager : Control
{

    private IMenuController _currentController;
    private Stack<IMenuController> _previousMenus = new();
    private Dictionary<int, IMenuController> _controllers = new();

    public override void _Input(InputEvent @event)
    {
        _currentController?.HandleInputPressedWhenActive(@event);
    }

    /// <summary>
    /// Shows new menu, <paramref name="stackBehaviour"/> is controlled by MenuController to dispose of the view
    /// </summary>
    /// <param name="menuType"></param>
    /// <param name="stackBehaviour"></param>
    public void ShowMenu(int menuType, MenuStackBehaviourEnum stackBehaviour,
        Action onNewMenuShown = null, bool instant = false)
    {
        if (_currentController?.Type == menuType)
            return;

        if (_currentController != null)
        {
            _currentController.Hide(stackBehaviour, () =>
            {
                ChangeMenu(menuType, stackBehaviour, onNewMenuShown, instant);
            }, instant);
        }
        else
        {
            ChangeMenu(menuType, stackBehaviour, onNewMenuShown, instant);
        }
    }

    public void ReturnToPreviousMenu(Action onComplete = null, bool instant = false)
    {
        if (_previousMenus.Count > 0)
        {
            ShowMenu(_previousMenus.Peek().Type, MenuStackBehaviourEnum.RemoveFromStack, onComplete, instant);
        }
    }

    private void ChangeMenu(int menuType, MenuStackBehaviourEnum stackBehaviour,
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
