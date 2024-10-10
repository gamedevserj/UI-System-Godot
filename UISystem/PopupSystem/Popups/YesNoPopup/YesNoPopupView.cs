using Godot;
using UISystem.Common.ElementViews;
using UISystem.Common.Transitions.Interfaces;
using UISystem.Core.Elements.Interfaces;
using UISystem.PopupSystem.Popups.YesPopup;

namespace UISystem.PopupSystem.Views;
internal partial class YesNoPopupView : YesPopupView
{

    [Export] private ButtonView noButton;

    public ButtonView NoButton => noButton;

    public override IFocusableControl DefaultSelectedElement => NoButton;

    protected override void PopulateFocusableElements()
    {
        _focusableElements = new IFocusableControl[] { YesButton, NoButton };
    }

    protected override ITweenableMenuElement[] GetTweenableElements()
    {
        return new ITweenableMenuElement[] { YesButton, noButton, messageMask };
    }

}
