using Godot;
using System;
using System.Threading.Tasks;
using UISystem.Common.Elements;
using UISystem.Common.Interfaces;
using UISystem.Common.Structs;
using UISystem.Extensions;
using Color = Godot.Color;
using VisibilityManger = UISystem.Common.Helpers.CanvasItemVisibilityManager;

namespace UISystem.MenuSystem.Views;
public partial class InterfaceSettingsMenuView : SettingsMenuView
{

    private const float AnimationDuration = 1f;

    [Export] private DropdownView controllerIconsDropdown;
    [Export] private ButtonView saveSettingsButton;
    [Export] private ButtonView returnButton;
    [Export] private Control panel;

    private Vector2 _panelSize;
    private Vector2 _panelPosition;
    private SizeSettings _panelTweenSizeSettings;

    private SizeSettings _dropdownAnimationSettings;
    private SizeSettings _returnButtonAnimationSettings;
    private SizeSettings _saveButtonAnimationSettings;
    private SizeSettings _resetButtonAnimationSettings;
    private bool _initializedParameters;

    public ButtonView SaveSettingsButton => saveSettingsButton;
    public ButtonView ReturnButton => returnButton;
    public DropdownView ControllerIconsDropdown => controllerIconsDropdown;

    protected override void PopulateFocusableElements()
    {
        _focusableElements = new IFocusableControl[] { ReturnButton, ControllerIconsDropdown, SaveSettingsButton, ResetButton };
    }

    public override void Hide(Action onHidden, bool instant)
    {
        Tween tween = GetTree().CreateTween();
        tween.SetPauseMode(Tween.TweenPauseMode.Process);

        float duration = AnimationDuration * 0.5f;
        tween.SetEase(Tween.EaseType.Out);
        tween.SetTrans(Tween.TransitionType.Linear);
        tween.TweenControlSize(true, returnButton.ResizableizeControl, Vector2.Zero, duration, _returnButtonAnimationSettings);
        tween.TweenControlSize(true, saveSettingsButton.ResizableizeControl, Vector2.Zero, duration, _saveButtonAnimationSettings);
        tween.TweenControlSize(true, ResetButton.ResizableizeControl, Vector2.Zero, duration, _resetButtonAnimationSettings);
        tween.TweenControlSize(true, controllerIconsDropdown.ResizableizeControl, Vector2.Zero, duration, _dropdownAnimationSettings);
        tween.TweenCallback(Callable.From(() =>
        {
            VisibilityManger.HideItem(ControllerIconsDropdown);
            VisibilityManger.HideItem(SaveSettingsButton);
            VisibilityManger.HideItem(ReturnButton);
            VisibilityManger.HideItem(ResetButton);
        }));

        tween.SetEase(Tween.EaseType.In);
        tween.SetTrans(Tween.TransitionType.Back);
        tween.TweenControlSize(false, panel, Vector2.Zero, AnimationDuration, _panelTweenSizeSettings);

        //tween.SetEase(Tween.EaseType.Out);
        tween.SetTrans(Tween.TransitionType.Quad);
        tween.TweenCanvasItemModulate(false, fadeObjectsContainer, new Color(fadeObjectsContainer.Modulate, 0), 0.1f);
        tween.TweenCallback(Callable.From(() =>
        {
            onHidden?.Invoke();
        }));
        //tween.Finished += ()=> onHidden?.Invoke();

    }

    public override async void Show(Action onShown, bool instant)
    {
        if (!_initializedParameters)
            await InitElementParameters();
        base.Show(onShown, instant);
    }

    private async Task InitElementParameters()
    {
        await ToSignal(RenderingServer.Singleton, RenderingServerInstance.SignalName.FramePostDraw);
        _panelSize = panel.Size;
        _panelPosition = panel.Position;

        var horizontalDirection = Common.Enums.HorizontalControlSizeChangeDirection.FromCenter;
        var verticalDirection = Common.Enums.VerticalControlSizeChangeDirection.FromCenter;
        _saveButtonAnimationSettings = new SizeSettings(saveSettingsButton.ResizableizeControl.Position,
            saveSettingsButton.ResizableizeControl.Size, horizontalDirection, verticalDirection);
        _returnButtonAnimationSettings = new SizeSettings(ReturnButton.ResizableizeControl.Position,
            ReturnButton.ResizableizeControl.Size, horizontalDirection, verticalDirection);
        _resetButtonAnimationSettings = new SizeSettings(ResetButton.ResizableizeControl.Position,
            ResetButton.ResizableizeControl.Size, horizontalDirection, verticalDirection);
        _dropdownAnimationSettings = new SizeSettings(ControllerIconsDropdown.ResizableizeControl.Position, 
            ControllerIconsDropdown.ResizableizeControl.Size, horizontalDirection, verticalDirection);
        
        _panelTweenSizeSettings = new SizeSettings(_panelPosition, _panelSize, horizontalDirection, verticalDirection);
    }

}

