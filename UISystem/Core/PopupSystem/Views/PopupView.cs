using Godot;
using System;
using UISystem.Core.Common;
using UISystem.Core.Common.Interfaces;

namespace UISystem.Core.PopupSystem.Views;
public abstract partial class PopupView : BaseInteractableWindow
{

    [Export] private Label message;

    public Label Message { get => message; set => message = value; }
    public abstract IFocusableControl DefaultSelectedElement { get; }

    public abstract void Show(Action onShown, bool instant = false);
    public abstract void Hide(Action onHidden, bool instant = false);

}
