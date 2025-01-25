using Godot;
using UISystem.Core.Elements.Interfaces;
using UISystem.Elements.ElementViews;

namespace UISystem.PopupSystem.Popups.Views;
internal partial class YesNoCancelPopupView : YesNoPopupView
{

    [Export] private ButtonView cancelButton;

    public ButtonView CancelButton => cancelButton;

    public override IFocusableControl DefaultSelectedElement => CancelButton;

    protected override void PopulateFocusableElements()
    {
        _focusableElements = new IFocusableControl[] { YesButton, NoButton, CancelButton };
    }

}
