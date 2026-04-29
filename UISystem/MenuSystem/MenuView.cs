using UISystem.Core.MenuSystem;
using UISystem.Elements;
using UISystem.Views;

namespace UISystem.MenuSystem;

/// <summary>
/// Base class for menu views.
/// </summary>
public abstract partial class MenuView : ViewBase, IMenuView<IFocusableUiElement>
{

    private IFocusableUiElement _lastSelectedElement;

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
    public void SetLastSelectedElement(IFocusableUiElement lastSelectedElement)
    {
        _lastSelectedElement = lastSelectedElement;
    }
}
