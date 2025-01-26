using Godot;
using UISystem.Elements;
using UISystem.Elements.ElementViews;

namespace UISystem.PopupSystem.Popups.Views;
internal partial class YesPopupView : PopupView
{

    [Export] protected ButtonView yesButton;
    [Export] protected ResizableControlView messageMask;

    public ButtonView YesButton => yesButton;
    public ResizableControlView MessageMask => messageMask;
    public override IFocusableControl DefaultSelectedElement => YesButton;

    protected override void PopulateFocusableElements()
    {
        _focusableElements = new IFocusableControl[] { YesButton };
    }

}
