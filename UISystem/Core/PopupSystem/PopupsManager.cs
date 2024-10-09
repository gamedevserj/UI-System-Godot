using Godot;
using System;
using System.Collections.Generic;
using UISystem.Core.MenuSystem.Interfaces;
using UISystem.Core.PopupSystem.Interfaces;

namespace UISystem.Core.PopupSystem;
public partial class PopupsManager : Control
{

    private IPopupController _currentController;

    private Dictionary<int, IPopupController> _controllers;

    public override void _Input(InputEvent @event)
    {
        _currentController?.HandleInputPressedWhenActive(@event);
    }

    public void ShowPopup(int popupType, IMenuController caller, string message, Action<int> onHideAction = null, bool instant = false)
    {
        _currentController = _controllers[popupType];
        _currentController.Init(this);
        _currentController.Show(caller, message, onHideAction, instant);
    }

    public void HidePopup(int result)
    {
        _currentController?.Hide(result);
        _currentController = null;
    }

    private void AddPopups(IPopupController[] popupControllers)
    {
        for (int i = 0; i < popupControllers.Length; i++)
        {
            _controllers.Add(popupControllers[i].Type, popupControllers[i]);
        }
    }

}
