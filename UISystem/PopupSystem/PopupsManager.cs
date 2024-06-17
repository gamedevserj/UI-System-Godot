using Godot;
using System;
using System.Collections.Generic;
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
        _controllers = new Dictionary<PopupType, IPopupController>
        {
            { PopupType.Yes, new InformationPopupController(PopupViewsPaths.Info, this, tree)},
            { PopupType.YesNo, new YesNoPopupController(PopupViewsPaths.YesNo, this, tree)},
            { PopupType.YesNoCancel, new YesNoCancelPopupController(PopupViewsPaths.YesNoCancel, this, tree)},
        };
    }

    public void ShowPopup(PopupType popupType, string message, Action<PopupResult> onHideAction = null)
    {
        _currentController = _controllers[popupType];
        _currentController.Init(this);
        _currentController.Show(message, onHideAction);
    }

    public void HidePopup(PopupResult result)
    {
        _currentController?.Hide(result);
        _currentController = null;
    }

}
