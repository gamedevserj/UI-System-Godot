using Godot;
using System;
using UISystem.Core.Common;
using UISystem.Core.Common.ElementViews;
using UISystem.Core.Common.Interfaces;

namespace UISystem.Core.PopupSystem.Views;

public abstract partial class PopupView : BaseInteractableWindow
{

    [Export] private Label message;
    [Export] protected ButtonView yesButton;

    public Label Message { get => message; set => message = value; }
    public ButtonView YesButton => yesButton;
    public virtual IFocusableControl DefaultSelectedElement => YesButton;

    protected override void PopulateFocusableElements()
    {
        _focusableElements = new IFocusableControl[] { YesButton };
    }

    public abstract void Show(Action onShown, bool instant = false);
    public abstract void Hide(Action onHidden, bool instant = false);

}
