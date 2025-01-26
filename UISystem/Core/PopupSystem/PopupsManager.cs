﻿using System;
using System.Collections.Generic;
using UISystem.Core.MenuSystem;
using UISystem.Core.PhysicalInput;

namespace UISystem.Core.PopupSystem;
public partial class PopupsManager<TInputEvent> : IPopupsManager<TInputEvent>
{

    private IPopupController<TInputEvent> _currentController;
    private Dictionary<int, IPopupController<TInputEvent>> _controllers = new();
    private IInputProcessor<TInputEvent> _inputProcessor;

    public void ProcessInput(TInputEvent inputEvent)
    {
        if (_currentController == null || !_currentController.CanReceivePhysicalInput)
            return;

        _inputProcessor.ProcessInput(inputEvent, _currentController);
    }

    public void Init(IPopupController<TInputEvent>[] controllers, IInputProcessor<TInputEvent> inputProcessor)
    {
        for (int i = 0; i < controllers.Length; i++)
        {
            _controllers.Add(controllers[i].Type, controllers[i]);
        }
        _inputProcessor = inputProcessor;
    }

    public void ShowPopup(int popupType, IMenuController<TInputEvent> caller, string message, Action<int> onHideAction = null, bool instant = false)
    {
        _currentController = _controllers[popupType];
        _currentController.Init();
        _currentController.Show(caller, message, onHideAction, instant);
    }

    public void HidePopup(int result)
    {
        _currentController?.Hide(result);
        _currentController = null;
    }

}
