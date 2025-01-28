using Godot;
using UISystem.Core.PopupSystem;
using UISystem.PopupSystem.Constants;
using UISystem.PopupSystem.Popups.Views;
using UISystem.Views;

namespace UISystem.PopupSystem.Popups.Controllers;
internal class YesNoPopupController<TViewCreator, TInputEvent> : PopupControllerBase<ViewCreator<YesNoPopupView>, YesNoPopupView>
{
    public override int Type => PopupType.YesNo;
    public override int PressedReturnPopupResult => PopupResult.No;

    public YesNoPopupController(ViewCreator<YesNoPopupView> viewCreator, IPopupsManager<InputEvent> popupsManager) : base(viewCreator, popupsManager)
    { }

    protected override void SetupElements()
    {
        _view.YesButton.ButtonDown += () => _popupsManager.HidePopup(PopupResult.Yes);
        _view.NoButton.ButtonDown += () => _popupsManager.HidePopup(PopupResult.No);
    }

}
