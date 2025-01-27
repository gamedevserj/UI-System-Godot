using Godot;
using UISystem.Core.PopupSystem;
using UISystem.Elements;
using UISystem.Elements.ElementViews;
using UISystem.Views;

namespace UISystem.PopupSystem;
public abstract partial class PopupView : ViewBase, IPopupView
{

    [Export] protected Control fadeObjectsContainer;
    [Export] protected Control panel;
    [Export] private Label messageLabel;
    [Export] protected ResizableControlView messageMask;

    public Control FadeObjectsContainer => fadeObjectsContainer;
    public Control Panel => panel;
    public Label Message { set => messageLabel = value; }
    public ResizableControlView MessageMask => messageMask;
    public abstract IFocusableControl DefaultSelectedElement { get; }

    public override void FocusElement()
    {
        if (DefaultSelectedElement?.IsValidElement() == true)
        {
            DefaultSelectedElement.SwitchFocus(true);
        }
    }

    public void SetMessage(string message)
    {
        messageLabel.Text = message;
    }
}
