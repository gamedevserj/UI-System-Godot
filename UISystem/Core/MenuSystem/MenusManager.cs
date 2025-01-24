﻿using System;
using System.Collections.Generic;
using UISystem.Core.MenuSystem.Enums;
using UISystem.Core.MenuSystem.Interfaces;

namespace UISystem.Core.MenuSystem;
public partial class MenusManager<TInputEvent> : IMenusManager<TInputEvent>
{

    private IMenuController<TInputEvent> _currentController;
    private Stack<IMenuController<TInputEvent>> _previousMenus = new();
    private Dictionary<int, IMenuController<TInputEvent>> _controllers = new();

    public void Init(IMenuController<TInputEvent>[] controllers)
    {
        for (int i = 0; i < controllers.Length; i++)
        {
            _controllers.Add(controllers[i].Type, controllers[i]);
        }
    }

    public void ProcessInput(TInputEvent @event)
    {
        if (_currentController == null || !_currentController.CanProcessInput)
            return;

        _currentController?.ProcessInput(@event);
    }

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
        var controller = _controllers[menuType];
        controller.Init();

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
            case StackingType.Replace:
                _currentController.DestroyView();
                if (_previousMenus.Count > 0)
                    _previousMenus.Pop();
                _previousMenus.Push(controller);
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
