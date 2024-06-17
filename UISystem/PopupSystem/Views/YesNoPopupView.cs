using Godot;
using UISystem.Common.Elements;
using UISystem.Common.Interfaces;

namespace UISystem.PopupSystem.Views;
public partial class YesNoPopupView : PopupView
{

    [Export] private ButtonView noButton;

    public ButtonView NoButton => noButton;

    public override Control DefaultSelectedElement => NoButton;

    protected override void PopulateFocusableElements()
    {
        _focusableElements = new IFocusableControl[] { YesButton, NoButton };
    }
}
