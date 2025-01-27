using Godot;
using UISystem.Core.PopupSystem;
using UISystem.PopupSystem.Constants;
using UISystem.PopupSystem.Popups.Views;

namespace UISystem.PopupSystem.Popups.Controllers;
internal class YesNoCancelPopupController<TViewHandler, TInputEvent> : PopupControllerBase<PopupViewCreator<YesNoCancelPopupView>, YesNoCancelPopupView>
{

    public override int Type => PopupType.YesNoCancel;
    public override int PressedReturnPopupResult => PopupResult.Cancel;

    public YesNoCancelPopupController(PopupViewCreator<YesNoCancelPopupView> viewHandler, IPopupsManager<InputEvent> popupsManager) : base(viewHandler, popupsManager)
    { }

    protected override void SetupElements()
    {
        _view.YesButton.ButtonDown += () => _popupsManager.HidePopup(PopupResult.Yes);
        _view.NoButton.ButtonDown += () => _popupsManager.HidePopup(PopupResult.No);
        _view.CancelButton.ButtonDown += () => _popupsManager.HidePopup(PopupResult.Cancel);
    }

}
