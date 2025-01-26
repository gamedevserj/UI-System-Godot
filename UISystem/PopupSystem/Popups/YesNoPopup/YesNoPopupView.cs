using Godot;
using UISystem.Elements;
using UISystem.Elements.ElementViews;

namespace UISystem.PopupSystem.Popups.Views;
internal partial class YesNoPopupView : YesPopupView
{

    [Export] private ButtonView noButton;

    public ButtonView NoButton => noButton;

    public override IFocusableControl DefaultSelectedElement => NoButton;

    protected override void PopulateFocusableElements()
    {
        _focusableElements = new IFocusableControl[] { YesButton, NoButton };
    }

}
