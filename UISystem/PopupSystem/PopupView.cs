using Godot;
using UISystem.Core.PopupSystem;
using UISystem.Elements.ElementViews;
using UISystem.Views;

namespace UISystem.PopupSystem;

/// <summary>
/// Base class for popup views.
/// </summary>
public abstract partial class PopupView : ViewBase, IPopupView
{
    [Export] protected Control fadeObjectsContainer;
    [Export] protected Control panel;
    [Export] private Label messageLabel;
    [Export] protected ResizableControlView messageMask;

    /// <summary>
    /// Gets fade objects container.
    /// </summary>
    public Control FadeObjectsContainer => fadeObjectsContainer;

    /// <summary>
    /// Gets panel.
    /// </summary>
    public Control Panel => panel;

    /// <summary>
    /// Gets message mask resizable control.
    /// </summary>
    public ResizableControlView MessageMask => messageMask;

    /// <inheritdoc/>
    public override void FocusElement()
    {
        if (DefaultSelectedElement?.IsValidElement() == true)
        {
            DefaultSelectedElement.SwitchFocus(true);
        }
    }

    /// <inheritdoc/>
    public void SetMessage(string message)
    {
        messageLabel.Text = message;
    }
}
