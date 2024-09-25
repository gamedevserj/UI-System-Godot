using Godot;
using System;
using System.Threading.Tasks;
using UISystem.Common.Elements;
using UISystem.Common.Interfaces;
using UISystem.Common.Structs;
using UISystem.Constants;
using UISystem.Extensions;
using Color = Godot.Color;
using VisibilityManger = UISystem.Common.Helpers.CanvasItemVisibilityManager;

namespace UISystem.MenuSystem.Views;
public partial class InterfaceSettingsMenuView : SettingsMenuView
{

    private const float AnimationDuration = 1;

    [Export] private OptionButtonView controllerIconsDropdown;
    [Export] private ButtonView saveSettingsButton;
    [Export] private ButtonView returnButton;
    [Export] private PanelContainer panel;

    private Vector2 _panelSize;
    private Vector2 _panelPosition;
    private Vector2 _dropdownSize;
    private Vector2 _saveButtonSize;
    private Vector2 _returnButtonSize;
    private Vector2 _resetButtonSize;
    private TweenSizeSettings _panelTweenSizeSettings;
    private bool _initializedParameters;

    public ButtonView SaveSettingsButton => saveSettingsButton;
    public ButtonView ReturnButton => returnButton;
    public OptionButtonView ControllerIconsDropdown => controllerIconsDropdown;

    protected override void PopulateFocusableElements()
    {
        _focusableElements = new IFocusableControl[] { ReturnButton, ControllerIconsDropdown, SaveSettingsButton, ResetButton };
    }

    public override void Hide(Action onHidden, bool instant)
    {
        Tween tween = GetTree().CreateTween();
        tween.SetPauseMode(Tween.TweenPauseMode.Process);

        tween.SetEase(Tween.EaseType.In);
        tween.SetTrans(Tween.TransitionType.Back);
        float duration = AnimationDuration * 0.5f;
        tween.Parallel().TweenProperty(ControllerIconsDropdown, PropertyConstants.Size, Vector2.Zero, duration);
        tween.Parallel().TweenProperty(ReturnButton, PropertyConstants.Size, Vector2.Zero, duration);
        Vector2 pos = ReturnButton.Position + ReturnButton.Size * 0.5f;
        tween.Parallel().TweenProperty(ReturnButton, PropertyConstants.Position, pos, duration);
        tween.Parallel().TweenProperty(SaveSettingsButton, PropertyConstants.Size, Vector2.Zero, duration);
        tween.Parallel().TweenProperty(ResetButton, PropertyConstants.Size, Vector2.Zero, duration);
        tween.TweenCallback(Callable.From(() => 
        {
            VisibilityManger.HideItem(ControllerIconsDropdown);
            VisibilityManger.HideItem(SaveSettingsButton);
            VisibilityManger.HideItem(ReturnButton);
            VisibilityManger.HideItem(ResetButton);
        }));

        tween.Parallel().TweenProperty(panel, PropertyConstants.Size, Vector2.Zero, AnimationDuration);
        tween.TweenControlSize(false, panel, Vector2.Zero, AnimationDuration, _panelTweenSizeSettings);

        tween.Parallel().TweenProperty(fadeObjectsContainer, PropertyConstants.Modulate, new Color(fadeObjectsContainer.Modulate, 0), AnimationDuration).SetDelay(AnimationDuration);
        tween.TweenCallback(Callable.From(() =>
        {
            onHidden?.Invoke();
        }));

    }

    public override async void Show(Action onShown, bool instant)
    {
        //if (!_initializedParameters)
        //    await InitElementParameters();
        base.Show(onShown, instant);
    }

    private async Task InitElementParameters()
    {
        await ToSignal(RenderingServer.Singleton, RenderingServerInstance.SignalName.FramePostDraw);
        _panelSize = panel.Size;
        _panelPosition = panel.Position;
        _dropdownSize = ControllerIconsDropdown.Size;
        _saveButtonSize = SaveSettingsButton.Size;
        _returnButtonSize = ReturnButton.Size;
        _resetButtonSize = ResetButton.Size;
        _panelTweenSizeSettings = new TweenSizeSettings(_panelPosition, _panelSize,
            Common.Enums.HorizontalControlSizeChangeDirection.FromCenter, 
            Common.Enums.VerticalControlSizeChangeDirection.FromCenter);
    }

}

