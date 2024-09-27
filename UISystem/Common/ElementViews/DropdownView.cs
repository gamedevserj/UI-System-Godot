using Godot;
using UISystem.Common.ElementViews;
using UISystem.Common.Interfaces;
using UISystem.Common.Resources;

namespace UISystem.Common.Elements;
public partial class DropdownView : OptionButton, IFocusableControl
{

    [Export] private ButtonHoverSettings buttonHoverSettings;
    [Export] private AnimatedButtonView animatedButtonView;

    private ITweener _tweener;

    public Control ResizableizeControl => animatedButtonView.ResizableControl;
    private Control Border => animatedButtonView.Border;

    public override async void _EnterTree()
    {
        if (buttonHoverSettings == null) return;

        await ToSignal(RenderingServer.Singleton, RenderingServerInstance.SignalName.FramePostDraw);

        _tweener = buttonHoverSettings.CreateTweener(GetTree(), ResizableizeControl, Border);
        Subscribe();
    }

    public override void _ExitTree() => Unsubscribe();

    private void Subscribe()
    {
        FocusEntered += OnFocusEntered;
        FocusExited += OnFocusExited;
        MouseEntered += OnMouseEntered;
        MouseExited += OnMouseExited;
        ItemSelected += OnItemSelected;
        OnItemSelected(GetSelectedId());
    }

    private void Unsubscribe()
    {
        FocusEntered -= OnFocusEntered;
        FocusExited -= OnFocusExited;
        MouseEntered -= OnMouseEntered;
        MouseExited -= OnMouseExited;
    }

    private void OnItemSelected(long index)
    {
        animatedButtonView.Label.Text = GetItemText((int)index);
    }

    private void OnMouseEntered()
    {
        if (HasFocus()) return;
        _tweener.OnMouseEntered();
    }
    private void OnMouseExited()
    {
        if (HasFocus()) return;
        _tweener.OnMouseExited();
    }

    private void OnFocusEntered()
    {
        if (Disabled) return;
        _tweener.OnFocusEntered();
    }
    private void OnFocusExited()
    {
        if (Disabled) return;
        _tweener.OnFocusExited();
    }

}
