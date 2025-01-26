using System;
using System.Collections.Generic;
using UISystem.Core.PhysicalInput;

namespace UISystem.Core.MenuSystem;
public partial class MenusManager<TInputEvent> : Manager<IMenuController<TInputEvent>, TInputEvent>, IMenusManager<TInputEvent>
{

    public static Action<IInputReceiver<TInputEvent>> OnControllerSwitch;

    private Stack<IMenuController<TInputEvent>> _previousMenus = new();

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
                _previousMenus.Pop();
                break;
            case StackingType.Clear:
                foreach (var menuController in _previousMenus)
                {
                    menuController.ProcessStacking(stackingType);
                }
                _previousMenus.Clear();
                break;
            case StackingType.Replace:
                break;
            default:
                break;
        }
        _currentController?.ProcessStacking(stackingType);
        _currentController = controller;
        
        _currentController.Show(() =>
        {
            OnControllerSwitch?.Invoke(_currentController);
            onNewMenuShown?.Invoke();
        }, instant);
    }

}
