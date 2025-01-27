using UISystem.Core.MenuSystem;
using UISystem.Elements;
using UISystem.Views;

namespace UISystem.MenuSystem;
public abstract partial class MenuView : ViewBase, IMenuView<IFocusableControl>
{

    private IFocusableControl _lastSelectedElement;
    protected abstract IFocusableControl DefaultSelectedElement { get; }

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

    public void SetLastSelectedElement(IFocusableControl lastSelectedElement)
    {
        _lastSelectedElement = lastSelectedElement;
    }
}
