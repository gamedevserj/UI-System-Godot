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
    [Export] private Control _fadeObjectsContainer;
    [Export] private Control _panel;
    [Export] private Label _messageLabel;
    [Export] private ResizableControlView _messageMask;

    /// <summary>
    /// Gets fade objects container.
    /// </summary>
    public Control FadeObjectsContainer => _fadeObjectsContainer;

    /// <summary>
    /// Gets panel.
    /// </summary>
    public Control Panel => _panel;

    /// <summary>
    /// Gets message mask resizable control.
    /// </summary>
    public ResizableControlView MessageMask => _messageMask;

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
        _messageLabel.Text = message;
    }
}
