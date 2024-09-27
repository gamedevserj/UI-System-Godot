using Godot;
using UISystem.Common.ElementViews;
using UISystem.Common.Interfaces;

namespace UISystem.Common.Elements;
public partial class OptionButtonView : OptionButton, IFocusableControl
{

    [Export] private AnimatedButtonView animatedButtonView;

    public override void _EnterTree() => Subscribe();

    public override void _ExitTree() => Unsubscribe();

    private void Subscribe()
    {
        FocusEntered += OnFocusEntered;
        FocusExited += OnFocusExited;
        MouseEntered += OnMouseEntered;
        MouseExited += OnMouseExited;
    }

    private void Unsubscribe()
    {
        FocusEntered -= OnFocusEntered;
        FocusExited -= OnFocusExited;
        MouseEntered -= OnMouseEntered;
        MouseExited -= OnMouseExited;
    }

    private void OnMouseEntered()
    {
        if (HasFocus()) return;
        //_tweener.OnMouseEntered();
    }
    private void OnMouseExited()
    {
        if (HasFocus()) return;
        //_tweener.OnMouseExited();
    }

    private void OnFocusEntered()
    {
        if (Disabled) return;
        //_tweener.OnFocusEntered();
    }
    private void OnFocusExited()
    {
        if (Disabled) return;
        //_tweener.OnFocusExited();
    }

}
