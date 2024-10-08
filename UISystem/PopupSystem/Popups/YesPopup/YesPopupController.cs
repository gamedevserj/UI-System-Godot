using Godot;
using UISystem.Core.PopupSystem;
using UISystem.Core.PopupSystem.Controllers;
using UISystem.Core.PopupSystem.Enums;
using UISystem.PopupSystem.Popups.YesPopup;

namespace UISystem.PopupSystem.Controllers;
public class YesPopupController : PopupController<YesPopupView>
{

    public override int Popup => PopupType.Yes;
    public override PopupResult PressedReturnPopupResult => PopupResult.Yes;

    public YesPopupController(string prefab, PopupsManager popupsManager, SceneTree sceneTree)
        : base(prefab, popupsManager, sceneTree)
    {
    }

    protected override void SetupElements()
    {
        _view.YesButton.ButtonDown += () => _popupsManager.HidePopup(PopupResult.Yes);
    }
}
