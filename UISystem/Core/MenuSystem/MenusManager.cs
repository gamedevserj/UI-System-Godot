using System;
using System.Collections.Generic;
using UISystem.Core.PhysicalInput;

namespace UISystem.Core.MenuSystem;
public partial class MenusManager<TInputEvent> : IMenusManager<TInputEvent>
{

    private IMenuController<TInputEvent> _currentController;
    private Stack<IMenuController<TInputEvent>> _previousMenus = new();
    private Dictionary<int, IMenuController<TInputEvent>> _controllers = new();
    private IInputProcessor<TInputEvent> _inputProcessor;

    public void Init(IMenuController<TInputEvent>[] controllers, IInputProcessor<TInputEvent> inputProcessor)
    {
        for (int i = 0; i < controllers.Length; i++)
        {
            _controllers.Add(controllers[i].Type, controllers[i]);
        }
        _inputProcessor = inputProcessor;
    }

    public void ProcessInput(TInputEvent inputEvent)
    {
        if (_currentController == null || !_currentController.CanReceivePhysicalInput)
            return;

        _inputProcessor.ProcessInput(inputEvent, _currentController);
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
            onNewMenuShown?.Invoke();
        }, instant);
    }

}
