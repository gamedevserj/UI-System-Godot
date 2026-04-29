using UISystem.Core.PopupSystem;
using UISystem.Core.Views;
using UISystem.PopupSystem.Popups.Views;

namespace UISystem.PopupSystem.Popups.Controllers;
internal class YesNoPopupController : PopupControllerBase<IViewCreator<YesNoPopupView>, YesNoPopupView>
{
    public override PopupResult PressedReturnPopupResult => PopupResult.No;

    public YesNoPopupController(IViewCreator<YesNoPopupView> viewCreator, IPopupsManager<PopupResult> popupsManager) 
        : base(viewCreator, popupsManager)
    { }

    protected override void SetupElements()
    {
        View.YesButton.ButtonDown += () => PopupsManager.HidePopup(PopupResult.Yes);
        View.NoButton.ButtonDown += () => PopupsManager.HidePopup(PopupResult.No);
    }

}
