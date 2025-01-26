using Godot;
using UISystem.Core.Elements.Interfaces;
using UISystem.Core.PopupSystem;
using UISystem.Core.Views;

namespace UISystem.PopupSystem;
public abstract partial class PopupView : BaseInteractableWindow, IPopupView
{

    [Export] protected Control fadeObjectsContainer;
    [Export] protected Control panel;
    [Export] private Label messageLabel;

    public Control FadeObjectsContainer => fadeObjectsContainer;
    public Control Panel => panel;
    public Label Message { set => messageLabel = value; }
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
