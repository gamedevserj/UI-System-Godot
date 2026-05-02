using Godot;
using UISystem.Core.Elements;
using UISystem.Core.Transitions;
using UISystem.Elements.ElementViews;
using UISystem.Transitions;
using UISystem.Transitions.Interfaces;

namespace UISystem.PopupSystem.Popups.Views;

/// <summary>
/// Yes/No/Cancel popup view.
/// </summary>
internal partial class YesNoCancelPopupView : PopupView
{
    [Export] private ButtonView _yesButton;
    [Export] private ButtonView _noButton;
    [Export] private ButtonView _cancelButton;

    /// <summary>
    /// Gets yes button.
    /// </summary>
    public ButtonView YesButton => _yesButton;

    /// <summary>
    /// Gets no button.
    /// </summary>
    public ButtonView NoButton => _noButton;

    /// <summary>
    /// Gets cancel button.
    /// </summary>
    public ButtonView CancelButton => _cancelButton;

    /// <inheritdoc/>
    protected override IInteractableElement DefaultSelectedElement => CancelButton;

    /// <inheritdoc/>
    protected override IViewTransition CreateTransition()
    {
        return new PanelSizeTransition(
            this,
            FadeObjectsContainer,
            Panel,
            new ITweenableMenuElement[] { YesButton, NoButton, CancelButton, MessageMask });
    }

    /// <inheritdoc/>
    protected override void PopulateFocusableElements()
    {
        FocusableElements = new IInteractableElement[] { YesButton, NoButton, CancelButton };
    }
}
