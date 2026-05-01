using UISystem.Core.PopupSystem;
using UISystem.Core.Views;
using UISystem.PopupSystem.Popups.Views;

namespace UISystem.PopupSystem.Popups.Controllers;

/// <summary>
/// Yes popup controller.
/// </summary>
internal class YesPopupController : PopupController<IViewCreator<YesPopupView>, YesPopupView>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="YesPopupController"/> class.
    /// </summary>
    /// <param name="viewCreator">View creator.</param>
    /// <param name="popupsManager">Popups manager.</param>
    public YesPopupController(IViewCreator<YesPopupView> viewCreator, IPopupsManager popupsManager)
        : base(viewCreator, popupsManager)
    {
    }

    /// <inheritdoc/>
    public override PopupResult PressedReturnPopupResult => PopupResult.Yes;

    /// <inheritdoc/>
    protected override void SetupElements()
    {
        View.YesButton.ButtonDown += () => PopupsManager.HidePopup(PopupResult.Yes);
    }
}
