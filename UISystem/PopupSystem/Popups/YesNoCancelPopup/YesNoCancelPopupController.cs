using Godot;
using UISystem.Core.PopupSystem;
using UISystem.PopupSystem.Popups.Views;
using UISystem.Views;

namespace UISystem.PopupSystem.Popups.Controllers;
internal class YesNoCancelPopupController<TViewCreator, TInputEvent> : PopupControllerBase<ViewCreator<YesNoCancelPopupView>, YesNoCancelPopupView>
{

    public override PopupType Type => PopupType.YesNoCancel;
    public override PopupResult PressedReturnPopupResult => PopupResult.Cancel;

    public YesNoCancelPopupController(ViewCreator<YesNoCancelPopupView> viewCreator, IPopupsManager<InputEvent, PopupType, PopupResult> popupsManager) : base(viewCreator, popupsManager)
    { }

    protected override void SetupElements()
    {
        _view.YesButton.ButtonDown += () => _popupsManager.HidePopup(PopupResult.Yes);
        _view.NoButton.ButtonDown += () => _popupsManager.HidePopup(PopupResult.No);
        _view.CancelButton.ButtonDown += () => _popupsManager.HidePopup(PopupResult.Cancel);
    }

}
