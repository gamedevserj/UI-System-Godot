using System.Threading.Tasks;
using Godot;
using UISystem.Transitions.Interfaces;

namespace UISystem.Elements.ElementViews;

/// <summary>
/// Base class for controls that need to be resized (like some labels during transitions).
/// </summary>
public partial class ResizableControlView : Control, ITweenableMenuElement
{
    /// <summary>
    /// Gets the control responsible for position.
    /// </summary>
    public Control PositionControl => this;

    /// <summary>
    /// Gets the control responsible for resizing.
    /// </summary>
    public Control ResizableControl => this;

    /// <inheritdoc/>
    public async Task ResetHover() => await Task.CompletedTask;
}
