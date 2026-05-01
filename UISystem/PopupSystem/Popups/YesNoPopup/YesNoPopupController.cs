using UISystem.Core.PopupSystem;
using UISystem.Core.Views;
using UISystem.PopupSystem.Popups.Views;

namespace UISystem.PopupSystem.Popups.Controllers;

/// <summary>
/// Yes/No popup controller.
/// </summary>
internal class YesNoPopupController : PopupController<IViewCreator<YesNoPopupView>, YesNoPopupView>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="YesNoPopupController"/> class.
    /// </summary>
    /// <param name="viewCreator">View creator.</param>
    /// <param name="popupsManager">Popups manager.</param>
    public YesNoPopupController(IViewCreator<YesNoPopupView> viewCreator, IPopupsManager popupsManager)
        : base(viewCreator, popupsManager)
    {
    }

    /// <inheritdoc/>
    public override PopupResult PressedReturnPopupResult => PopupResult.No;

    /// <inheritdoc/>
    protected override void SetupElements()
    {
        View.YesButton.ButtonDown += () => PopupsManager.HidePopup(PopupResult.Yes);
        View.NoButton.ButtonDown += () => PopupsManager.HidePopup(PopupResult.No);
    }
}
