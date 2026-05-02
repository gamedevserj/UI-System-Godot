using Godot;
using UISystem.Core.Elements;
using UISystem.Core.Transitions;
using UISystem.Elements.ElementViews;
using UISystem.Transitions;
using UISystem.Transitions.Interfaces;

namespace UISystem.PopupSystem.Popups.Views;

/// <summary>
/// Yes/No popup view.
/// </summary>
internal partial class YesNoPopupView : PopupView
{
    [Export] private ButtonView _yesButton;
    [Export] private ButtonView _noButton;

    /// <summary>
    /// Gets yes button.
    /// </summary>
    public ButtonView YesButton => _yesButton;

    /// <summary>
    /// Gets no button.
    /// </summary>
    public ButtonView NoButton => _noButton;

    /// <inheritdoc/>
    protected override IInteractableElement DefaultSelectedElement => NoButton;

    /// <inheritdoc/>
    protected override IViewTransition CreateTransition()
    {
        return new PanelSizeTransition(
            this,
            FadeObjectsContainer,
            Panel,
            new ITweenableMenuElement[] { YesButton, NoButton, MessageMask });
    }

    /// <inheritdoc/>
    protected override void PopulateFocusableElements()
    {
        FocusableElements = new IInteractableElement[] { YesButton, NoButton };
    }
}
