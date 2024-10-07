using Godot;
using System.Threading.Tasks;
using UISystem.Common.Enums;
using UISystem.Common.Interfaces;
using UISystem.Common.HoverSettings;

namespace UISystem.Common.Elements;
public partial class DropdownView : OptionButton, IFocusableControl, ISizeTweenable
{

    [Export] private ButtonHoverSettings buttonHoverSettings;
    [Export] private Control resizableControl;
    [Export] private Control border;
    [Export] private Label label;

    private ITweener _hoverTweener;
    private bool _mouseOver;
    private Tween _tween;

    public Control ResizableControl => resizableControl;

    public override async void _EnterTree()
    {
        if (buttonHoverSettings == null) return;

        await ToSignal(RenderingServer.Singleton, RenderingServerInstance.SignalName.FramePostDraw);

        _hoverTweener = buttonHoverSettings.CreateTweener(resizableControl, border);
        Subscribe();
    }

    public override void _ExitTree() => Unsubscribe();

    public async Task ResetHover()
    {
        _tween?.Kill();
        _tween = GetTree().CreateTween();
        _hoverTweener.Reset(_tween);
        await ToSignal(_tween, Tween.SignalName.Finished);
    }

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
        ItemSelected -= OnItemSelected;
    }

    private void OnItemSelected(long index)
    {
        label.Text = GetItemText((int)index);
    }

    private void OnMouseEntered()
    {
        _mouseOver = true;
        HoverTween();
    }
    private void OnMouseExited()
    {
        _mouseOver = false;
        HoverTween();
    }

    private void OnFocusEntered() => HoverTween();
    private void OnFocusExited() => HoverTween();

    private void HoverTween()
    {
        _tween?.Kill();
        _tween = GetTree().CreateTween();
        _hoverTweener.Tween(_tween, GetDrawingMode());
    }

    private ControlDrawMode GetDrawingMode()
    {
        if (Disabled) return ControlDrawMode.Disabled;
        if (HasFocus())
        {
            return _mouseOver ? ControlDrawMode.HoverFocus : ControlDrawMode.Focus;
        }
        else
            return _mouseOver ? ControlDrawMode.Hover : ControlDrawMode.Normal;
    }

    

}
