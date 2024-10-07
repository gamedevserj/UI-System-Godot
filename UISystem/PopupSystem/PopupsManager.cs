using Godot;
using System;
using System.Collections.Generic;
using UISystem.MenuSystem.Interfaces;
using UISystem.PopupSystem.Constants;
using UISystem.PopupSystem.Controllers;
using UISystem.PopupSystem.Enums;
using UISystem.PopupSystem.Interfaces;

namespace UISystem.PopupSystem;
public partial class PopupsManager : Control
{

    private IPopupController _currentController;

    private Dictionary<PopupType, IPopupController> _controllers;

    public void Init()
    {
        SceneTree tree = GetTree();

        _controllers = new Dictionary<PopupType, IPopupController>();
        AddPopups(new IPopupController[]
        {
            new YesPopupController(GetPopupPath(PopupType.Yes), this, tree),
            new YesNoPopupController(GetPopupPath(PopupType.YesNo), this, tree),
            new YesNoCancelPopupController(GetPopupPath(PopupType.YesNoCancel), this, tree)
        });
    }

    public override void _Input(InputEvent @event)
    {
        _currentController?.HandleInputPressedWhenActive(@event);
    }

    public void ShowPopup(PopupType popupType, IMenuController caller, string message, Action<PopupResult> onHideAction = null, bool instant = false)
    {
        _currentController = _controllers[popupType];
        _currentController.Init(this);
        _currentController.Show(caller, message, onHideAction, instant);
    }

    public void HidePopup(PopupResult result)
    {
        _currentController?.Hide(result);
        _currentController = null;
    }

    private void AddPopups(IPopupController[] popupControllers)
    {
        for (int i = 0; i < popupControllers.Length; i++)
        {
            _controllers.Add(popupControllers[i].PopupType, popupControllers[i]);
        }
    }

    private static string GetPopupPath(PopupType type)
    {
        return PopupViewsPaths.Paths[type];
    }

}
