using Godot;
using UISystem.Core.Elements;
using UISystem.Elements.ElementViews;

namespace UISystem.MenuSystem.SettingsMenu;

/// <summary>
/// Base class for menus controlling game settings.
/// </summary>
public abstract partial class SettingsMenuView : MenuView
{
    [Export] private Control _fadeObjectsContainer;
    [Export] private ButtonView _returnButton;
    [Export] private ButtonView _resetButton;

    /// <summary>
    /// Gets fade objects container.
    /// </summary>
    public Control FadeObjectsContainer => _fadeObjectsContainer;

    /// <summary>
    /// Gets return button.
    /// </summary>
    public ButtonView ReturnButton => _returnButton;

    /// <summary>
    /// Gets reset button.
    /// </summary>
    public ButtonView ResetButton => _resetButton;

    /// <inheritdoc/>
    protected override IInteractableElement DefaultSelectedElement => ReturnButton;
}
