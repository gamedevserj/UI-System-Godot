using System;
using UISystem.Core.PopupSystem;
using UISystem.Core.Views;
using UISystem.PopupSystem.Popups.Views;

namespace UISystem.PopupSystem.Popups.Controllers;
internal class YesNoCancelPopupController : PopupControllerBase<IViewCreator<YesNoCancelPopupView>, YesNoCancelPopupView>
{

    public override PopupResult PressedReturnPopupResult => PopupResult.Cancel;

    public YesNoCancelPopupController(IViewCreator<YesNoCancelPopupView> viewCreator, IPopupsManager<PopupResult> popupsManager) : base(viewCreator, popupsManager)
    { }

    protected override void SetupElements()
    {
        View.YesButton.ButtonDown += () => PopupsManager.HidePopup(PopupResult.Yes);
        View.NoButton.ButtonDown += () => PopupsManager.HidePopup(PopupResult.No);
        View.CancelButton.ButtonDown += () => PopupsManager.HidePopup(PopupResult.Cancel);
    }

}
