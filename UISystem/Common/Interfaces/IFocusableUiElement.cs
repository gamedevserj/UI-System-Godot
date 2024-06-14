using Godot;
using GodotExtensions;
using static Godot.Control;

namespace UISystem.Common.Interfaces;
public interface IFocusableUiElement<T> where T : Control
{

    private T Instance => (T)this;

    void FocusElement()
    {
        Instance.GrabFocus();
    }

    bool IsValidElement()
    {
        return Instance.IsValid();
    }

    void SwitchFocusAvailability(bool focusable)
    {
        Instance.FocusMode = focusable ? FocusModeEnum.All : FocusModeEnum.None;
        Instance.MouseFilter = focusable ? MouseFilterEnum.Stop : MouseFilterEnum.Ignore;
    }

}
