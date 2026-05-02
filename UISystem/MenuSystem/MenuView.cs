using UISystem.Core.Elements;
using UISystem.Core.MenuSystem;
using UISystem.Views;

namespace UISystem.MenuSystem;

/// <summary>
/// Base class for menu views.
/// </summary>
public abstract partial class MenuView : ViewBase, IMenuView
{
    private IInteractableElement _lastSelectedElement;

    /// <inheritdoc/>
    public override void FocusElement()
    {
        if (_lastSelectedElement?.IsValidElement() == true)
        {
            _lastSelectedElement.SwitchFocus(true);
        }
        else if (DefaultSelectedElement?.IsValidElement() == true)
        {
            DefaultSelectedElement.SwitchFocus(true);
        }
    }

    /// <inheritdoc/>
    public void SetLastSelectedElement(IInteractableElement lastSelectedElement)
    {
        _lastSelectedElement = lastSelectedElement;
    }
}
