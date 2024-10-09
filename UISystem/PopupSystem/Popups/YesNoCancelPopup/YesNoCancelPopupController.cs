using Godot;
using UISystem.Core.PopupSystem;
using UISystem.Core.PopupSystem.Controllers;
using UISystem.PopupSystem.Constants;
using UISystem.PopupSystem.Views;

namespace UISystem.PopupSystem.Controllers;
public class YesNoCancelPopupController : PopupController<YesNoCancelPopupView>
{

    public override int Type => PopupType.YesNoCancel;
    public override int PressedReturnPopupResult => PopupResult.Cancel;

    public YesNoCancelPopupController(string prefab, PopupsManager popupsManager, SceneTree sceneTree) : base(prefab, popupsManager, sceneTree)
    {
    }

    protected override void SetupElements()
    {
        _view.YesButton.ButtonDown += () => _popupsManager.HidePopup(PopupResult.Yes);
        _view.NoButton.ButtonDown += () => _popupsManager.HidePopup(PopupResult.No);
        _view.CancelButton.ButtonDown += () => _popupsManager.HidePopup(PopupResult.Cancel);
    }
}
