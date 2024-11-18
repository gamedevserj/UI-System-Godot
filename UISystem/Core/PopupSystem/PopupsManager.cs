using Godot;
using System;
using System.Collections.Generic;
using UISystem.Core.MenuSystem.Interfaces;
using UISystem.Core.PopupSystem.Interfaces;
using UISystem.PopupSystem.Constants;

namespace UISystem.Core.PopupSystem;
public partial class PopupsManager : Control, IPopupsManager
{

    private IPopupController _currentController;
    private Dictionary<int, IPopupController> _controllers = new();

    public override void _Input(InputEvent @event)
    {
        _currentController?.HandleInputPressedWhenActive(@event);
    }

    public void Init(IPopupController[] controllers)
    {
        for (int i = 0; i < controllers.Length; i++)
        {
            _controllers.Add(controllers[i].Type, controllers[i]);
        }
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

    private static string GetPopupPath(int type)
    {
        return PopupViewsPaths.Paths[type];
    }

}
