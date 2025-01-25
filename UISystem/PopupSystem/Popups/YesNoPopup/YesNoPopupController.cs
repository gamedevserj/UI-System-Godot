using Godot;
using UISystem.Core.PopupSystem.Controllers;
using UISystem.Core.PopupSystem.Interfaces;
using UISystem.PopupSystem.Constants;
using UISystem.PopupSystem.Popups.ViewHandlers;
using UISystem.PopupSystem.Popups.Views;

namespace UISystem.PopupSystem.Popups.Controllers;
internal class YesNoPopupController<TViewHandler, TInputEvent> : PopupController<YesNoPopupViewHandler<YesNoPopupView>, InputEvent, YesNoPopupView>
{
    public override int Type => PopupType.YesNo;
    public override int PressedReturnPopupResult => PopupResult.No;

    public YesNoPopupController(YesNoPopupViewHandler<YesNoPopupView> viewHandler, IPopupsManager<InputEvent> popupsManager) : base(viewHandler, popupsManager)
    { }

    protected override void SetupElements()
    {
        _view.YesButton.ButtonDown += () => _popupsManager.HidePopup(PopupResult.Yes);
        _view.NoButton.ButtonDown += () => _popupsManager.HidePopup(PopupResult.No);
    }

}
