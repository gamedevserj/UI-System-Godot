using Godot;
using UISystem.Core.PopupSystem;
using UISystem.Core.PopupSystem.Controllers;
using UISystem.Core.PopupSystem.Enums;
using UISystem.PopupSystem.Views;

namespace UISystem.PopupSystem.Controllers;
public class YesNoPopupController : PopupController<YesNoPopupView>
{

    public override int Popup => PopupType.YesNo;
    public override PopupResult PressedReturnPopupResult => PopupResult.No;

    public YesNoPopupController(string prefab, PopupsManager popupsManager, SceneTree sceneTree) : base(prefab, popupsManager, sceneTree)
    {

    }

    public override void Init(Node popupParent)
    {
        base.Init(popupParent);
        _view.YesButton.ButtonDown += () => _popupsManager.HidePopup(PopupResult.Yes);
        _view.NoButton.ButtonDown += () => _popupsManager.HidePopup(PopupResult.No);
    }

}
