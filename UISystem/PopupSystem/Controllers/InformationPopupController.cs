using Godot;
using PopupSystem.Views;
using UISystem.PopupSystem.Enums;

namespace PopupSystem.Controllers;
public class InformationPopupController : PopupController<PopupView>
{
    public InformationPopupController(string prefab, PopupsManager popupsManager, SceneTree sceneTree) : base(prefab, popupsManager, sceneTree)
    {
    }

    public override void Init(Node popupParent)
    {
        base.Init(popupParent);
        _view.YesButton.ButtonDown += () => _popupsManager.HidePopup(PopupResult.Yes);
    }

}
