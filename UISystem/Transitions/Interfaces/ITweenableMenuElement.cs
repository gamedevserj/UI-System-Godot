using System.Threading.Tasks;
using Godot;

namespace UISystem.Transitions.Interfaces;

/// <summary>
/// Defines contract for tweenable menu element.
/// </summary>
public interface ITweenableMenuElement
{
    /// <summary>
    /// Gets control responsible for position.
    /// </summary>
    Control PositionControl { get; }

    /// <summary>
    /// Gets control responsible for resizing.
    /// </summary>
    Control ResizableControl { get; }

    /// <summary>
    /// Resets hover.
    /// </summary>
    Task ResetHover();
}
