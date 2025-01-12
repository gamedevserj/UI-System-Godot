using Godot;
using UISystem.Core.Elements.Interfaces;
using UISystem.Elements.ElementViews;

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

}
