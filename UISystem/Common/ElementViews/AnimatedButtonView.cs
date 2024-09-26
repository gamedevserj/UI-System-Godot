using Godot;
using System.Threading.Tasks;
using UISystem.Common.Elements;
using UISystem.Common.Interfaces;
using UISystem.Common.Resources;

namespace UISystem.Common.ElementViews;
public partial class AnimatedButtonView : Control
{

    [Export] private ButtonHoverSettings settings;

    private ButtonView _button;
    private ITweener _tweener;

    public async Task Init(ButtonView button)
    {
        await ToSignal(RenderingServer.Singleton, RenderingServerInstance.SignalName.FramePostDraw);
        _button = button;
        _tweener = settings.SizeChangeSettings.CreateTweener(GetTree(), this);
        Subscribe();
    }

    public override void _ExitTree() => Unsubscribe();

    private void Subscribe()
    {
        _button.FocusEntered += OnFocusEntered;
        _button.FocusExited += OnFocusExited;
        _button.MouseEntered += OnMouseEntered;
        _button.MouseExited += OnMouseExited;
    }

    private void Unsubscribe()
    {
        _button.FocusEntered -= OnFocusEntered;
        _button.FocusExited -= OnFocusExited;
        _button.MouseEntered -= OnMouseEntered;
        _button.MouseExited -= OnMouseExited;
    }

    private void OnMouseEntered()
    {
        if (_button.HasFocus()) return;
        _tweener.OnMouseEntered();
    }
    private void OnMouseExited()
    {
        if (_button.HasFocus()) return;
        _tweener.OnMouseExited();
    }

    private void OnFocusEntered()
    {
        if (_button.Disabled) return;
        _tweener.OnFocusEntered();
    }
    private void OnFocusExited()
    {
        if (_button.Disabled) return;
        _tweener.OnFocusExited();
    }

}
