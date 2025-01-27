using Godot;
using System;
using UISystem.Core.Extensions;
using UISystem.Core.Transitions;
using UISystem.Core.Views;
using UISystem.Elements;

namespace UISystem.Views;
/// <summary>
/// Base class for a window with interactable elements (menu, popup, etc.)
/// </summary>
public abstract partial class ViewBase : Control, IView
{

    protected IViewTransition _transition;
    protected IFocusableControl[] _focusableElements;

    public bool IsValid => this.IsValid();

    public virtual void Init(IViewTransition transition)
    {
        _transition = transition;
        PopulateFocusableElements();
    }

    public void SwitchFocusAvailability(bool enable)
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

    public void Show(Action onShown, bool instant = false)
    {
        SwitchFocusAvailability(false);
        Visible = true;
        _transition.Show(()=>
        {
            SwitchFocusAvailability(true);
            onShown?.Invoke();
        }, instant);
    }

    public void Hide(Action onHidden, bool instant = false)
    {
        SwitchFocusAvailability(false);
        _transition.Hide(() => { 
            onHidden?.Invoke();
            Visible = false; // need to switch off visibility to allow GuiPanel3D to receive mouse events
        }, instant);
    }

    public void DestroyView() => this.SafeQueueFree();
    public abstract void FocusElement();
    protected abstract void PopulateFocusableElements();

}
