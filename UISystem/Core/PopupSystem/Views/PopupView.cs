using Godot;
using UISystem.Core.Elements.Interfaces;
using UISystem.Core.Views;

namespace UISystem.Core.PopupSystem.Views;
public abstract partial class PopupView : BaseInteractableWindow
{

    [Export] private Label message;

    public Label Message { get => message; set => message = value; }
    public abstract IFocusableControl DefaultSelectedElement { get; }

}
