using System;
using UISystem.Core.Transitions;
using UISystem.Elements;

namespace UISystem.Views;
/// <summary>
/// Base class for a window with interactable elements (menu, popup, etc.)
/// </summary>
public abstract partial class BaseInteractableWindow : BaseWindowView
{

    protected IFocusableControl[] _focusableElements;

    public override void Init(IViewTransition transition)
    {
        _transition = transition;
        PopulateFocusableElements();
    }

    public override void SwitchFocusAvailability(bool enable)
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

    public override void Show(Action onShown, bool instant = false)
    {
        SwitchFocusAvailability(false);
        Visible = true;
        _transition.Show(()=>
        {
            SwitchFocusAvailability(true);
            onShown?.Invoke();
        }, instant);
    }

    public override void Hide(Action onHidden, bool instant = false)
    {
        SwitchFocusAvailability(false);
        _transition.Hide(() => { 
            onHidden?.Invoke();
            Visible = false; // need to switch off visibility to allow GuiPanel3D to receive mouse events
        }, instant);
    }

    protected abstract void PopulateFocusableElements();

}
