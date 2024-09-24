using Godot;
using System;
using System.Threading.Tasks;
using UISystem.Common.Elements;
using UISystem.Common.Interfaces;
using UISystem.Constants;

namespace UISystem.MenuSystem.Views;

/// <summary>
/// uncomment lines with Fader and remove everything else in Show() and Hide() is you want to use simple fading transitions
/// </summary>
public partial class MainMenuView : MenuView
{

    private const float AnimationDuration = 0.5f;

    [Export] private ButtonView playButton;
    [Export] private ButtonView optionsButton;
    [Export] private ButtonView quitButton;
    [Export] private Control fadeObjectsContainer;

    public ButtonView PlayButton => playButton;
    public ButtonView OptionsButton => optionsButton;
    public ButtonView QuitButton => quitButton;

    private Vector2 _playButtonSize;
    private Vector2 _optionsButtonPosition;
    private Vector2 _quitButtonPosition;
    private bool _initializedSize;

    protected override void PopulateFocusableElements()
    {
        _focusableElements = new IFocusableControl[] { PlayButton, OptionsButton, QuitButton };
    }

    public override void Init()
    {
        base.Init();
        //Fader.Init(fadeObjectsContainer);
    }

    public override void Hide(Action onHidden, bool instant)
    {
        //Fader.Hide(GetTree(), fadeObjectsContainer, onHidden, instant);
        if (instant)
        {
            HideItem(fadeObjectsContainer);
            onHidden?.Invoke();
            return;
        }

        Tween tween = GetTree().CreateTween();
        tween.SetPauseMode(Tween.TweenPauseMode.Process);

        tween.SetEase(Tween.EaseType.In);
        tween.SetTrans(Tween.TransitionType.Back);
        tween.Parallel().TweenProperty(OptionsButton, PropertyConstants.Position, Vector2.Zero, AnimationDuration);
        tween.Parallel().TweenProperty(QuitButton, PropertyConstants.Position, Vector2.Zero, AnimationDuration);
        tween.TweenCallback(Callable.From(() => 
        {
            HideItem(OptionsButton);
            HideItem(QuitButton);
        }));

        Vector2 size = new(0, PlayButton.Size.Y);
        float duration = AnimationDuration * 0.5f;
        tween.SetTrans(Tween.TransitionType.Linear);
        tween.Parallel().TweenProperty(PlayButton, PropertyConstants.Modulate, new Color(PlayButton.Modulate, 0), duration);
        tween.SetEase(Tween.EaseType.Out);
        tween.SetTrans(Tween.TransitionType.Quad);
        tween.Parallel().TweenProperty(PlayButton, PropertyConstants.Size, size, duration);
        tween.Parallel().TweenProperty(fadeObjectsContainer, PropertyConstants.Modulate, new Color(fadeObjectsContainer.Modulate, 0), duration).SetDelay(duration);
        tween.TweenCallback(Callable.From(() =>
        {
            
            onHidden?.Invoke();
        }));
    }

    public override async void Show(Action onShown, bool instant)
    {
        //Fader.Show(GetTree(), fadeObjectsContainer, onShown, instant);
        if (!_initializedSize)
            await GetButtonSizes();

        if (instant)
        {
            ShowItem(PlayButton);
            ShowItem(OptionsButton);
            ShowItem(QuitButton);
            onShown?.Invoke();
            return;
        }

        PlayButton.Size = new(0, PlayButton.Size.Y);
        ShowItem(PlayButton);
        HideItem(OptionsButton);
        HideItem(QuitButton);
        OptionsButton.Position = Vector2.Zero;
        QuitButton.Position = Vector2.Zero;

        Tween tween = GetTree().CreateTween();
        tween.SetPauseMode(Tween.TweenPauseMode.Process);
        tween.Parallel().TweenProperty(fadeObjectsContainer, PropertyConstants.Modulate, new Color(fadeObjectsContainer.Modulate, 1), 0.1f);

        tween.SetEase(Tween.EaseType.Out);
        tween.SetTrans(Tween.TransitionType.Quad);
        tween.Parallel().TweenProperty(PlayButton, PropertyConstants.Size, _playButtonSize, AnimationDuration * 0.5f);
        tween.TweenCallback(Callable.From(() =>
        {
            ShowItem(OptionsButton);
            ShowItem(QuitButton);
        }));

        tween.SetTrans(Tween.TransitionType.Back);
        tween.Parallel().TweenProperty(OptionsButton, PropertyConstants.Position, _optionsButtonPosition, AnimationDuration);
        tween.Parallel().TweenProperty(QuitButton, PropertyConstants.Position, _quitButtonPosition, AnimationDuration);
        tween.TweenCallback(Callable.From(() =>
        {
            onShown?.Invoke();
        }));
        
    }

    private async Task GetButtonSizes()
    {
        await ToSignal(RenderingServer.Singleton, RenderingServerInstance.SignalName.FramePostDraw);
        _playButtonSize = PlayButton.Size;
        _optionsButtonPosition = OptionsButton.Position;
        _quitButtonPosition = QuitButton.Position;
        _initializedSize = true;
    }

    private static void HideItem(CanvasItem item)
    {
        SwitchItemVisibility(item, 0);
    }

    private static void ShowItem(CanvasItem item)
    {
        SwitchItemVisibility(item, 1);
    }

    private static void SwitchItemVisibility(CanvasItem item, float visibility)
    {
        item.Modulate = new Color(item.Modulate, visibility);
    }

}
