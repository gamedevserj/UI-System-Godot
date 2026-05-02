using Godot;
using UISystem.Core.Elements;
using UISystem.Core.Transitions;
using UISystem.Transitions;

namespace UISystem.MenuSystem.Views;

/// <summary>
/// In-game menu view.
/// </summary>
public partial class InGameMenuView : MenuView
{
    [Export] private Control _fadeObjectsContainer;

    /// <inheritdoc/>
    protected override IInteractableElement DefaultSelectedElement => null;

    /// <inheritdoc/>
    protected override IViewTransition CreateTransition()
    {
        return new FadeTransition(_fadeObjectsContainer);
    }

    /// <inheritdoc/>
    protected override void PopulateFocusableElements()
    {
    }
}
