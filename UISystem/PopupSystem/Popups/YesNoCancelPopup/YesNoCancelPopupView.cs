using Godot;
using UISystem.Common.ElementViews;
using UISystem.Core.Elements.Interfaces;
using UISystem.PopupSystem.Popups.YesPopup;

namespace UISystem.PopupSystem.Views;
internal partial class YesNoCancelPopupView : YesPopupView
{

    [Export] private ButtonView noButton;
    [Export] private ButtonView cancelButton;

    public ButtonView NoButton => noButton;
    public ButtonView CancelButton => cancelButton;

    public override IFocusableControl DefaultSelectedElement => CancelButton;

    protected override void PopulateFocusableElements()
    {
        _focusableElements = new IFocusableControl[] { YesButton, NoButton, CancelButton };
    }
}
