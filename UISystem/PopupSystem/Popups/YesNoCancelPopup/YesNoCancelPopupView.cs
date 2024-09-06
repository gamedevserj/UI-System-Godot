using Godot;
using UISystem.Common.Elements;
using UISystem.Common.Interfaces;
using UISystem.UISystem.PopupSystem.Views;

namespace UISystem.PopupSystem.Views;
public partial class YesNoCancelPopupView : PopupViewFade
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
