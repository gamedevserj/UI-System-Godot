using Godot;
using UISystem.PopupSystem.Enums;
using UISystem.PopupSystem.Popups.YesPopup;

namespace UISystem.PopupSystem.Controllers;
public class YesPopupController : PopupController<YesPopupView>
{

    public override PopupType PopupType => PopupType.Yes;
    public override PopupResult PressedReturnPopupResult => PopupResult.Yes;

    public YesPopupController(string prefab, PopupsManager popupsManager, SceneTree sceneTree) 
        : base(prefab, popupsManager, sceneTree)
    {
    }

    public override void Init(Node popupParent)
    {
        base.Init(popupParent);
        _view.YesButton.ButtonDown += () => _popupsManager.HidePopup(PopupResult.Yes);
    }

}
