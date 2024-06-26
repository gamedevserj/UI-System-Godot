﻿using Godot;
using UISystem.PopupSystem.Enums;
using UISystem.PopupSystem.Views;

namespace UISystem.PopupSystem.Controllers;
public class InformationPopupController : PopupController<PopupView>
{

    public override PopupType PopupType => PopupType.Yes;
    
    public InformationPopupController(string prefab, PopupsManager popupsManager, SceneTree sceneTree) : base(prefab, popupsManager, sceneTree)
    {
    }

    public override void Init(Node popupParent)
    {
        base.Init(popupParent);
        _view.YesButton.ButtonDown += () => _popupsManager.HidePopup(PopupResult.Yes);
    }

}
