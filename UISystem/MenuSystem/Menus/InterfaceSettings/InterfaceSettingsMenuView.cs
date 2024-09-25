using Godot;
using System;
using System.Threading.Tasks;
using UISystem.Common.Elements;
using UISystem.Common.ElementViews;
using UISystem.Common.Interfaces;
using UISystem.Common.Structs;
using UISystem.Constants;
using UISystem.Extensions;
using Color = Godot.Color;
using VisibilityManger = UISystem.Common.Helpers.CanvasItemVisibilityManager;

namespace UISystem.MenuSystem.Views;
public partial class InterfaceSettingsMenuView : SettingsMenuView
{

    private const float AnimationDuration = 7f;

    [Export] private OptionButtonView controllerIconsDropdown;
    [Export] private ButtonView saveSettingsButton;
    [Export] private ButtonView returnButton;
    [Export] private PanelContainer panel;

    [Export] private AnimatedButtonView animReturnButton;
    [Export] private AnimatedButtonView animSaveButton;
    [Export] private AnimatedButtonView animResetButton;

    private Vector2 _panelSize;
    private Vector2 _panelPosition;
    private TweenSizeSettings _panelTweenSizeSettings;

    private TweenSizeSettings _dropdownAnimationSettings;
    private TweenSizeSettings _returnButtonAnimationSettings;
    private TweenSizeSettings _saveButtonAnimationSettings;
    private TweenSizeSettings _resetButtonAnimationSettings;
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

        float duration = AnimationDuration * 0.5f;
        //tween.Parallel().TweenProperty(ControllerIconsDropdown, PropertyConstants.Size, Vector2.Zero, duration);

        tween.SetEase(Tween.EaseType.Out);
        tween.SetTrans(Tween.TransitionType.Linear);
        //tween.ControlSize(true, animReturnButton.AnimatedNode, Vector2.Zero, duration, _returnButtonAnimationSettings);
        //tween.ControlSize(true, animSaveButton.AnimatedNode, Vector2.Zero, duration, _saveButtonAnimationSettings);
        //tween.ControlSize(true, animResetButton.AnimatedNode, Vector2.Zero, duration, _resetButtonAnimationSettings);
        tween.ControlSize(true, ControllerIconsDropdown, Vector2.Zero, duration, _dropdownAnimationSettings);

        //tween.Parallel().TweenProperty(SaveSettingsButton, PropertyConstants.Size, Vector2.Zero, duration);
        //tween.Parallel().TweenProperty(ResetButton, PropertyConstants.Size, Vector2.Zero, duration);
        //tween.TweenCallback(Callable.From(() => 
        //{
        //    VisibilityManger.HideItem(ControllerIconsDropdown);
        //    VisibilityManger.HideItem(SaveSettingsButton);
        //    VisibilityManger.HideItem(ReturnButton);
        //    VisibilityManger.HideItem(ResetButton);
        //}));
        
        //tween.ControlSize(true, panel, Vector2.Zero, AnimationDuration, _panelTweenSizeSettings);

        //tween.SetEase(Tween.EaseType.Out);
        //tween.SetTrans(Tween.TransitionType.Quad);
        //tween.TweenProperty(fadeObjectsContainer, PropertyConstants.Modulate, new Color(fadeObjectsContainer.Modulate, 0), duration);
        //tween.TweenCallback(Callable.From(() =>
        //{
        //    onHidden?.Invoke();
        //}));
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
        //_panelSize = panel.Size;
        //_panelPosition = panel.Position;

        //var horizontalDirection = Common.Enums.HorizontalControlSizeChangeDirection.FromCenter;
        //var verticalDirection = Common.Enums.VerticalControlSizeChangeDirection.FromCenter;
        //_saveButtonAnimationSettings = new TweenSizeSettings(animSaveButton.OriginalPosition, animSaveButton.OriginalSize, horizontalDirection, verticalDirection);
        //_returnButtonAnimationSettings = new TweenSizeSettings(animReturnButton.OriginalPosition, animReturnButton.OriginalSize, horizontalDirection, verticalDirection);
        //_resetButtonAnimationSettings = new TweenSizeSettings(animResetButton.OriginalPosition, animResetButton.OriginalSize, horizontalDirection, verticalDirection);
        //_dropdownAnimationSettings = new TweenSizeSettings(ControllerIconsDropdown.Position, ControllerIconsDropdown.Size, horizontalDirection, verticalDirection);
        //_panelTweenSizeSettings = new TweenSizeSettings(_panelPosition, _panelSize, horizontalDirection, verticalDirection);
    }

}

