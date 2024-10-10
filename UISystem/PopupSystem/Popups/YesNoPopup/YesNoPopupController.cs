using Godot;
using UISystem.Core.PopupSystem.Controllers;
using UISystem.PopupSystem.Constants;
using UISystem.PopupSystem.Views;

namespace UISystem.PopupSystem.Controllers;
internal class YesNoPopupController : PopupController<YesNoPopupView>
{

    public override int Type => PopupType.YesNo;
    public override int PressedReturnPopupResult => PopupResult.No;

    public YesNoPopupController(string prefab, PopupsManager popupsManager, SceneTree sceneTree) : base(prefab, popupsManager, sceneTree)
    {

    }

    protected override void SetupElements()
    {
        _view.YesButton.ButtonDown += () => _popupsManager.HidePopup(PopupResult.Yes);
        _view.NoButton.ButtonDown += () => _popupsManager.HidePopup(PopupResult.No);
    }
}
