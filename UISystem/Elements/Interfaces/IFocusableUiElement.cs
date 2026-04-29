using Godot;
using UISystem.Extensions;
using static Godot.Control;

namespace UISystem.Elements;

/// <summary>
/// Defines contract for focusable UI element.
/// </summary>
public interface IFocusableUiElement
{
    /// <summary>
    /// Gets the Control instance.
    /// </summary>
    private Control Instance => (Control)this;

    /// <summary>
    /// Switches element focus.
    /// </summary>
    /// <param name="focus">Whether element should have focus.</param>
    void SwitchFocus(bool focus)
    {
        if (focus)
            Instance.GrabFocus();
        else
            Instance.ReleaseFocus();
    }

    /// <summary>
    /// Checks whether this element is valid.
    /// </summary>
    /// <returns>True if element is valid, otherwise - false.</returns>
    bool? IsValidElement()
    {
        return Instance?.IsValid();
    }

    /// <summary>
    /// Switches elements focus mode and mouse filter.
    /// </summary>
    /// <param name="focusable">Whether element should be allowed to have focus/be interactable.</param>
    void SwitchFocusAvailability(bool focusable)
    {
        Instance.FocusMode = focusable ? FocusModeEnum.All : FocusModeEnum.None;
        Instance.MouseFilter = focusable ? MouseFilterEnum.Stop : MouseFilterEnum.Ignore;

        if (!focusable && Instance.HasFocus())
            SwitchFocus(false);
    }
}
