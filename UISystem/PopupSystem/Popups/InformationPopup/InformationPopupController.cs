using Godot;
using UISystem.PopupSystem.Enums;
using UISystem.PopupSystem.Popups.InformationPopup;

namespace UISystem.PopupSystem.Controllers;
public class InformationPopupController : PopupController<InformationPopupView>
{

    public override PopupType PopupType => PopupType.Yes;
    public override PopupResult PressedReturnPopupResult => PopupResult.Yes;

    public InformationPopupController(string prefab, PopupsManager popupsManager, SceneTree sceneTree) 
        : base(prefab, popupsManager, sceneTree)
    {
    }

    public override void Init(Node popupParent)
    {
        base.Init(popupParent);
        _view.YesButton.ButtonDown += () => _popupsManager.HidePopup(PopupResult.Yes);
    }

}
