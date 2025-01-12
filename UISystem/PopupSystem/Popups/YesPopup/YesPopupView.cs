using Godot;
using UISystem.Core.Elements.Interfaces;
using UISystem.Core.PopupSystem.Views;
using UISystem.Elements.ElementViews;

namespace UISystem.PopupSystem.Views;
internal partial class YesPopupView : PopupView
{

    [Export] protected Control fadeObjectsContainer;
    [Export] protected Control panel;
    [Export] protected ButtonView yesButton;
    [Export] protected ResizableControlView messageMask;

    public ButtonView YesButton => yesButton;
    public Control FadeObjectsContainer => fadeObjectsContainer;
    public Control Panel => panel;
    public ResizableControlView MessageMask => messageMask;
    public override IFocusableControl DefaultSelectedElement => YesButton;

    protected override void PopulateFocusableElements()
    {
        _focusableElements = new IFocusableControl[] { YesButton };
    }

}
