using Godot;
using UISystem.PopupSystem.Enums;
using UISystem.PopupSystem.Views;

namespace UISystem.PopupSystem.Controllers;
public class YesNoCancelPopupController : PopupController<YesNoCancelPopupView>
{

    public override PopupType PopupType => PopupType.YesNoCancel;
    public override PopupResult PressedReturnPopupResult => PopupResult.Cancel;

    public YesNoCancelPopupController(string prefab, PopupsManager popupsManager, SceneTree sceneTree) : base(prefab, popupsManager, sceneTree)
    {
    }

    public override void Init(Node popupParent)
    {
        base.Init(popupParent);
        _view.YesButton.ButtonDown += ()=> _popupsManager.HidePopup(PopupResult.Yes);
        _view.NoButton.ButtonDown += ()=> _popupsManager.HidePopup(PopupResult.No);
        _view.CancelButton.ButtonDown += ()=> _popupsManager.HidePopup(PopupResult.Cancel);
    }

}
