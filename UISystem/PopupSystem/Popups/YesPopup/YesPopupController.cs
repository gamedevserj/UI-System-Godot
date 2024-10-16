using Godot;
using UISystem.Core.PopupSystem.Controllers;
using UISystem.PopupSystem.Constants;
using UISystem.PopupSystem.Views;

namespace UISystem.PopupSystem.Controllers;
internal class YesPopupController : PopupController<YesPopupView>
{

    public override int Type => PopupType.Yes;
    public override int PressedReturnPopupResult => PopupResult.Yes;

    public YesPopupController(string prefab, PopupsManager popupsManager, SceneTree sceneTree)
        : base(prefab, popupsManager, sceneTree)
    {
    }

    protected override void SetupElements()
    {
        _view.YesButton.ButtonDown += () => _popupsManager.HidePopup(PopupResult.Yes);
    }
}
