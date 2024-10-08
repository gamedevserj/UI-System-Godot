using Godot;
using UISystem.Common.ElementViews;
using UISystem.Core.Elements.Interfaces;
using UISystem.PopupSystem.Popups.YesPopup;

namespace UISystem.PopupSystem.Views;
public partial class YesNoPopupView : YesPopupView
{

    [Export] private ButtonView noButton;

    public ButtonView NoButton => noButton;

    public override IFocusableControl DefaultSelectedElement => NoButton;

    protected override void PopulateFocusableElements()
    {
        _focusableElements = new IFocusableControl[] { YesButton, NoButton };
    }
}
