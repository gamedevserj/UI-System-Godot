using Godot;
using PopupSystem.Views;

namespace PopupSystem.Controllers;
public class ConfirmationPopupController : PopupController<ConfirmationPopupView>
{

    public ConfirmationPopupController(string prefab, PopupsManager popupsManager, SceneTree sceneTree) : base(prefab, popupsManager, sceneTree)
    {

    }

    public override void Init(Node popupParent)
    {
        base.Init(popupParent);
        _view.ConfirmButton.ButtonDown += PressedConfirm;
        _view.CancelButton.ButtonDown += PressedCancel;
    }

    private void PressedConfirm()
    {
        _popupsManager.HidePopup(true);
    }

    private void PressedCancel()
    {
        _popupsManager.HidePopup(false);
    }

}
