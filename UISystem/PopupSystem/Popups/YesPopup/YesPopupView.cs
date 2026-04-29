using Godot;
using UISystem.Core.Transitions;
using UISystem.Elements;
using UISystem.Elements.ElementViews;
using UISystem.Transitions;
using UISystem.Transitions.Interfaces;

namespace UISystem.PopupSystem.Popups.Views;
internal partial class YesPopupView : PopupView
{

    [Export] protected ButtonView yesButton;

    public ButtonView YesButton => yesButton;
    protected override IFocusableUiElement DefaultSelectedElement => YesButton;

    protected override IViewTransition CreateTransition()
    {
        return new PanelSizeTransition(this, FadeObjectsContainer, Panel, new ITweenableMenuElement[] { YesButton, MessageMask });
    }

    protected override void PopulateFocusableElements()
    {
        FocusableElements = new IFocusableUiElement[] { YesButton };
    }

}
