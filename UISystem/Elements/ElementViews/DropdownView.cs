using System.Threading.Tasks;
using Godot;
using UISystem.Core.Elements;
using UISystem.Elements.HoverSettings;
using UISystem.Extensions;
using UISystem.Hovering;
using UISystem.Transitions.Interfaces;

namespace UISystem.Elements.ElementViews;

/// <summary>
/// Base class for dropdown view.
/// </summary>
public partial class DropdownView : OptionButton, IInteractableElement, ITweenableMenuElement
{
    [Export] private ButtonHoverSettings buttonHoverSettings;
    [Export] private Control resizableControl;
    [Export] private Control innerColor;
    [Export] private Control border;
    [Export] private Label label;

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
    public Control ResizableControl => resizableControl;

    /// <inheritdoc/>
    public override async void _EnterTree()
    {
        if (buttonHoverSettings == null)
            return;

        await ToSignal(RenderingServer.Singleton, RenderingServerInstance.SignalName.FramePostDraw);

        _hoverTweener = buttonHoverSettings.CreateTweener(resizableControl, innerColor, border, label);
        Subscribe();
    }

    /// <inheritdoc/>
    public override void _ExitTree() => Unsubscribe();

    /// <summary>
    /// Resets dropdown hover state.
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

    /// <summary>
    /// Selects an item in the dropdown. Needs to be a separate method to update label when selecting is called from code,
    /// because view awaits one frame before subscribing when entering tree to allow controls to setup their transforms.
    /// </summary>
    /// <param name="index">Item index.</param>
    public void SelectItem(long index)
    {
        Select((int)index);
        UpdateText((int)index);
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
        ItemSelected += UpdateText;
    }

    private void Unsubscribe()
    {
        if (buttonHoverSettings == null)
            return;
        FocusEntered -= OnFocusEntered;
        FocusExited -= OnFocusExited;
        MouseEntered -= OnMouseEntered;
        MouseExited -= OnMouseExited;
        ItemSelected -= UpdateText;
    }

    private void UpdateText(long index)
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
