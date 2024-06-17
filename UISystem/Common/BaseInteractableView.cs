using Godot;
using UISystem.Common.Interfaces;

namespace UISystem.Common;
public abstract partial class BaseInteractableView : Control
{

    protected IFocusableControl[] _focusableElements;

    public void Init()
    {
        PopulateFocusableElements();
    }

    public void SwitchFocusAwailability(bool enable)
    {
        if (_focusableElements != null)
        {
            for (int i = 0; i < _focusableElements.Length; i++)
            {
                if (enable)
                    _focusableElements[i].SwitchFocusAvailability(true);
                else
                    _focusableElements[i].SwitchFocusAvailability(false);
            }
        }
    }

    protected virtual void PopulateFocusableElements()
    {

    }

}
