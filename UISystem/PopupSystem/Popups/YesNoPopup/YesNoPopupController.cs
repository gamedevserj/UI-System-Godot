using UISystem.Core.PopupSystem;
using UISystem.Core.Views;
using UISystem.PopupSystem.Popups.Views;

namespace UISystem.PopupSystem.Popups.Controllers;
internal class YesNoPopupController : PopupControllerBase<IViewCreator<YesNoPopupView>, YesNoPopupView>
{
    public override PopupType Type => PopupType.YesNo;
    public override PopupResult PressedReturnPopupResult => PopupResult.No;

    public YesNoPopupController(IViewCreator<YesNoPopupView> viewCreator, IPopupsManager<PopupType, PopupResult> popupsManager) : base(viewCreator, popupsManager)
    { }

    protected override void SetupElements()
    {
        _view.YesButton.ButtonDown += () => _popupsManager.HidePopup(PopupResult.Yes);
        _view.NoButton.ButtonDown += () => _popupsManager.HidePopup(PopupResult.No);
    }

}
