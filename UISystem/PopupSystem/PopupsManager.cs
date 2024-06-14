using Godot;
using PopupSystem.Constants;
using PopupSystem.Controllers;
using PopupSystem.Enums;
using PopupSystem.Interfaces;
using System;
using System.Collections.Generic;

namespace PopupSystem;
public partial class PopupsManager : Control
{

    private IPopupController _currentController;

    private Dictionary<PopupType, IPopupController> _controllers;

    public void Init()
    {
        SceneTree tree = GetTree();
        _controllers = new Dictionary<PopupType, IPopupController>
        {
            { PopupType.InformationPopup, new InformationPopupController(PopupViewsPaths.Info, this, tree)},
            { PopupType.ConfirmationPopup, new ConfirmationPopupController(PopupViewsPaths.Confirmation, this, tree)},
        };
    }

    public void ShowPopup(PopupType popupType, string message, Action<bool> onHideAction = null)
    {
        _currentController = _controllers[popupType];
        _currentController.Init(this);
        _currentController.Show(message, onHideAction);
    }

    public void HidePopup(bool result)
    {
        _currentController?.Hide(result);
        _currentController = null;
    }

}
