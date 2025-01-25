using Godot;
using UISystem.Core.Elements.Interfaces;
using UISystem.Core.Views;

namespace UISystem.Core.PopupSystem.Views;
public abstract partial class PopupView : BaseInteractableWindow
{

    [Export] protected Control fadeObjectsContainer;
    [Export] protected Control panel;
    [Export] private Label message;

    public Control FadeObjectsContainer => fadeObjectsContainer;
    public Control Panel => panel;
    public Label Message { get => message; set => message = value; }
    public abstract IFocusableControl DefaultSelectedElement { get; }

    public override void FocusElement()
    {
        if (DefaultSelectedElement?.IsValidElement() == true)
        {
            DefaultSelectedElement.SwitchFocus(true);
        }
    }

}
