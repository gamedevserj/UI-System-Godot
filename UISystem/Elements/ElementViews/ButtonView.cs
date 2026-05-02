using System.Threading.Tasks;
using Godot;
using UISystem.Core.Elements;
using UISystem.Elements.HoverSettings;
using UISystem.Extensions;
using UISystem.Hovering;
using UISystem.Transitions.Interfaces;

namespace UISystem.Elements.ElementViews;

/// <summary>
/// Base class for button view.
/// </summary>
public partial class ButtonView : BaseButton, IInteractableElement, ITweenableMenuElement
{
    [Export] private ButtonHoverSettings _buttonHoverSettings;
    [Export] private Control _resizableControl;
    [Export] private Control _innerColor;
    [Export] private Control _border;
    [Export] private Label _label;

    private IHoverTweener _hoverTweener;
    private bool _mouseOver;
    private Tween _tween;

    /// <summary>
    /// Gets the control responsible for button position.
    /// </summary>
    public Control PositionControl => this;

    /// <summary>
    /// Gets the control responsible for resizing the button.
    /// </summary>
    public Control ResizableControl => _resizableControl;

    /// <inheritdoc/>
    public override async void _EnterTree()
    {
        if (_buttonHoverSettings == null)
            return;

        await ToSignal(RenderingServer.Singleton, RenderingServerInstance.SignalName.FramePostDraw);

        _hoverTweener = _buttonHoverSettings.CreateTweener(_resizableControl, _innerColor, _border, _label);
        Subscribe();
    }

    /// <inheritdoc/>
    public override void _ExitTree() => Unsubscribe();

    /// <summary>
    /// Resets button hover state.
    /// </summary>
    public async Task ResetHover()
    {
        if (_hoverTweener == null)
            await Task.CompletedTask;

        _tween?.Kill();
        _tween = GetTree().CreateTween();
        _hoverTweener.Reset(_tween);
        await ToSignal(_tween, Tween.SignalName.Finished);
    }

    /// <summary>
    /// Switches button on/off.
    /// There is no OnDisabled event in BaseButton, so it should be disabled via this method to change appearance.
    /// </summary>
    /// <param name="disable">Whether button should be disabled.</param>
    public void SwitchButton(bool disable)
    {
        Disabled = disable;
        HoverTween();
    }

    /// <inheritdoc/>
    public void SwitchFocus(bool focus)
    {
        if (focus)
            GrabFocus();
        else
            ReleaseFocus();
    }

    /// <inheritdoc/>
    public bool IsValidElement() => this.IsValid();

    /// <inheritdoc/>
    public void SwitchFocusAvailability(bool focusable)
    {
        FocusMode = focusable ? FocusModeEnum.All : FocusModeEnum.None;
        MouseFilter = focusable ? MouseFilterEnum.Stop : MouseFilterEnum.Ignore;

        if (!focusable && HasFocus())
            SwitchFocus(false);
    }

    private void Subscribe()
    {
        FocusEntered += OnFocusEntered;
        FocusExited += OnFocusExited;
        MouseEntered += OnMouseEntered;
        MouseExited += OnMouseExited;
    }

    private void Unsubscribe()
    {
        if (_buttonHoverSettings == null)
            return;
        FocusEntered -= OnFocusEntered;
        FocusExited -= OnFocusExited;
        MouseEntered -= OnMouseEntered;
        MouseExited -= OnMouseExited;
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
        if (_hoverTweener == null)
            return;

        _tween?.Kill();
        _tween = GetTree().CreateTween();
        _hoverTweener.Tween(_tween, GetDrawingMode());
    }

    private ControlDrawMode GetDrawingMode()
    {
        if (Disabled)
            return ControlDrawMode.Disabled;
        if (HasFocus())
        {
            return _mouseOver ? ControlDrawMode.HoverFocus : ControlDrawMode.Focus;
        }
        else
        {
            return _mouseOver ? ControlDrawMode.Hover : ControlDrawMode.Normal;
        }
    }
}
