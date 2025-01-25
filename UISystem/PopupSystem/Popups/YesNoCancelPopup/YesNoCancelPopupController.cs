using Godot;
using UISystem.Core.PopupSystem.Controllers;
using UISystem.Core.PopupSystem.Interfaces;
using UISystem.PopupSystem.Constants;
using UISystem.PopupSystem.Popups.ViewHandlers;
using UISystem.PopupSystem.Popups.Views;

namespace UISystem.PopupSystem.Popups.Controllers;
internal class YesNoCancelPopupController<TViewHandler, TInputEvent> : PopupController<YesNoCancelPopupViewHandler<YesNoCancelPopupView>, InputEvent, YesNoCancelPopupView>
{

    public override int Type => PopupType.YesNoCancel;
    public override int PressedReturnPopupResult => PopupResult.Cancel;

    public YesNoCancelPopupController(YesNoCancelPopupViewHandler<YesNoCancelPopupView> viewHandler, IPopupsManager<InputEvent> popupsManager) : base(viewHandler, popupsManager)
    { }

    protected override void SetupElements()
    {
        View.YesButton.ButtonDown += () => _popupsManager.HidePopup(PopupResult.Yes);
        View.NoButton.ButtonDown += () => _popupsManager.HidePopup(PopupResult.No);
        View.CancelButton.ButtonDown += () => _popupsManager.HidePopup(PopupResult.Cancel);
    }

}
