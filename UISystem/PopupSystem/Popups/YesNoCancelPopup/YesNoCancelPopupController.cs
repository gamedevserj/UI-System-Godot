using UISystem.Core.PopupSystem;
using UISystem.Core.Views;
using UISystem.PopupSystem.Popups.Views;

namespace UISystem.PopupSystem.Popups.Controllers;

/// <summary>
/// Yes/No/Cancel popup controller.
/// </summary>
internal class YesNoCancelPopupController : PopupController<IViewCreator<YesNoCancelPopupView>, YesNoCancelPopupView>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="YesNoCancelPopupController"/> class.
    /// </summary>
    /// <param name="viewCreator">View creator.</param>
    /// <param name="popupsManager">Popups manager.</param>
    public YesNoCancelPopupController(IViewCreator<YesNoCancelPopupView> viewCreator, IPopupsManager popupsManager)
        : base(viewCreator, popupsManager)
    {
    }

    /// <inheritdoc/>
    public override PopupResult PressedReturnPopupResult => PopupResult.Cancel;

    /// <inheritdoc/>
    protected override void SetupElements()
    {
        View.YesButton.ButtonDown += () => PopupsManager.HidePopup(PopupResult.Yes);
        View.NoButton.ButtonDown += () => PopupsManager.HidePopup(PopupResult.No);
        View.CancelButton.ButtonDown += () => PopupsManager.HidePopup(PopupResult.Cancel);
    }
}
