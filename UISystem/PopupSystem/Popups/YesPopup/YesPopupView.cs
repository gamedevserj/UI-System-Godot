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
internal partial class YesPopupView : PopupView
{
    [Export] private ButtonView _yesButton;

    /// <summary>
    /// Gets yes button.
    /// </summary>
    public ButtonView YesButton => _yesButton;

    /// <inheritdoc/>
    protected override IInteractableElement DefaultSelectedElement => YesButton;

    /// <inheritdoc/>
    protected override IViewTransition CreateTransition()
    {
        return new PanelSizeTransition(this, FadeObjectsContainer, Panel, new ITweenableMenuElement[] { YesButton, MessageMask });
    }

    /// <inheritdoc/>
    protected override void PopulateFocusableElements()
    {
        FocusableElements = new IInteractableElement[] { YesButton };
    }
}
