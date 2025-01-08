using Godot;
using System;
using UISystem.Core.Elements.Interfaces;

namespace UISystem.Core.Views;
/// <summary>
/// Base class for a window with interactable elements (menu, popup, etc.)
/// </summary>
public abstract partial class BaseInteractableWindow : BaseWindowView
{

    protected IFocusableControl[] _focusableElements;

    public override void Init()
    {
        PopulateFocusableElements();
    }

    public override void SwitchFocusAwailability(bool enable)
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

    public override void Show(Action onShown, bool instant = false)
    {
        SwitchFocusAwailability(false);
        Visible = true;
        Engine.TimeScale = 0.5f;
        _transition.Show(()=>
        {
            SwitchFocusAwailability(true);
            onShown?.Invoke();
        }, instant);
    }

    public override void Hide(Action onHidden, bool instant = false)
    {
        SwitchFocusAwailability(false);
        _transition.Hide(() => { 
            onHidden?.Invoke();
            Visible = false;
        }, instant);
    }

}
