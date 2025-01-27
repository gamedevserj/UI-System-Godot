using Godot;
using UISystem.Core.Transitions;
using UISystem.Core.Views;
using UISystem.Elements;
using UISystem.Elements.ElementViews;
using UISystem.Transitions.Interfaces;
using UISystem.Transitions;

namespace UISystem.PopupSystem.Popups.Views;
internal partial class YesPopupView : PopupView
{

    [Export] protected ButtonView yesButton;

    public ButtonView YesButton => yesButton;
    public override IFocusableControl DefaultSelectedElement => YesButton;

    protected override IViewTransition CreateTransition()
    {
        return new PanelSizeTransition(this, FadeObjectsContainer, Panel, new ITweenableMenuElement[] { YesButton, MessageMask });
    }

    protected override void PopulateFocusableElements()
    {
        _focusableElements = new IFocusableControl[] { YesButton };
    }

}
