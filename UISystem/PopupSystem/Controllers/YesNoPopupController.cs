using Godot;
using PopupSystem.Views;
using UISystem.PopupSystem.Enums;

namespace PopupSystem.Controllers;
public class YesNoPopupController : PopupController<YesNoPopupView>
{

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
