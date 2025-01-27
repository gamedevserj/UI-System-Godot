using Godot;
using UISystem.Core.Transitions;
using UISystem.Elements;
using UISystem.Elements.ElementViews;
using UISystem.Transitions;
using UISystem.Transitions.Interfaces;

namespace UISystem.PopupSystem.Popups.Views;
internal partial class YesNoCancelPopupView : PopupView
{

    [Export] protected ButtonView yesButton;
    [Export] private ButtonView noButton;
    [Export] private ButtonView cancelButton;

    public ButtonView YesButton => yesButton;
    public ButtonView NoButton => noButton;
    public ButtonView CancelButton => cancelButton;

    public override IFocusableControl DefaultSelectedElement => CancelButton;
    protected override IViewTransition CreateTransition()
    {
        return new PanelSizeTransition(this, FadeObjectsContainer, Panel, 
            new ITweenableMenuElement[] { YesButton, NoButton, CancelButton, MessageMask });
    }
    protected override void PopulateFocusableElements()
    {
        _focusableElements = new IFocusableControl[] { YesButton, NoButton, CancelButton };
    }

}
