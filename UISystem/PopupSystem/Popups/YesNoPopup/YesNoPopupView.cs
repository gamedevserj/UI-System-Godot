using Godot;
using UISystem.Core.Transitions;
using UISystem.Elements;
using UISystem.Elements.ElementViews;
using UISystem.Transitions;
using UISystem.Transitions.Interfaces;

namespace UISystem.PopupSystem.Popups.Views;
internal partial class YesNoPopupView : PopupView
{

    [Export] protected ButtonView yesButton;
    [Export] private ButtonView noButton;

    public ButtonView YesButton => yesButton;
    public ButtonView NoButton => noButton;

    public override IFocusableControl DefaultSelectedElement => NoButton;
    protected override IViewTransition CreateTransition()
    {
        return new PanelSizeTransition(this, FadeObjectsContainer, Panel,
        new ITweenableMenuElement[] { YesButton, NoButton, MessageMask });
    }

    protected override void PopulateFocusableElements()
    {
        _focusableElements = new IFocusableControl[] { YesButton, NoButton };
    }

}
