using Godot;
using PopupSystem.Views;

namespace PopupSystem.Controllers;
public class InformationPopupController : PopupController<PopupView>
{
    public InformationPopupController(string prefab, PopupsManager popupsManager, SceneTree sceneTree) : base(prefab, popupsManager, sceneTree)
    {
    }

    public override void Init(Node popupParent)
    {
        base.Init(popupParent);
        _view.ConfirmButton.ButtonDown += PressedConfirm;
    }

    private void PressedConfirm()
    {
        _popupsManager.HidePopup(true);
    }
}
