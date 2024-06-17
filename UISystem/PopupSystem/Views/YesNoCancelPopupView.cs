using Godot;
using UISystem.Common.Elements;
using UISystem.Common.Interfaces;

namespace PopupSystem.Views;
public partial class YesNoCancelPopupView : YesNoPopupView
{

    [Export] private ButtonView cancelButton;

    public ButtonView CancelButton => cancelButton;

    public override Control DefaultSelectedElement => CancelButton;

    protected override void PopulateFocusableElements()
    {
        _focusableElements = new IFocusableControl[] { YesButton, NoButton, CancelButton };
    }
}
