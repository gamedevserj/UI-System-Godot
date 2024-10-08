using Godot;
using UISystem.Core.Common.Interfaces;

namespace UISystem.Core.Common;
/// <summary>
/// Base class for a window with interactable elements (menu, popup, etc.)
/// </summary>
public abstract partial class BaseInteractableWindow : Control
{

    protected IFocusableControl[] _focusableElements;

    public virtual void Init()
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
