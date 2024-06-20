using Godot;
using System;
using UISystem.Constants;

namespace UISystem.MenuSystem.Views;
public partial class MenuViewFade : MenuView
{

    protected const float TransitionDuration = 0.25f;

    [Export] private Control fadeObjectsContainer;

    public override void Init()
    {
        base.Init();
        fadeObjectsContainer.Modulate = new Color(Modulate.R, Modulate.G, Modulate.B, 0);
    }


    public override void Show(Action onComplete, bool instant = false)
    {
        Tween tween = CreateTween();
        tween.SetPauseMode(Tween.TweenPauseMode.Process);
        Color originalColor = fadeObjectsContainer.Modulate;
        tween.TweenProperty(fadeObjectsContainer, PropertyConstants.Modulate, new Color(originalColor, 1), GetDuration(instant));
        tween.TweenCallback(Callable.From(() => onComplete?.Invoke()));
    }

    public override void Hide(Action onComplete, bool instant = false)
    {
        Tween tween = CreateTween();
        tween.SetPauseMode(Tween.TweenPauseMode.Process);
        tween.TweenProperty(fadeObjectsContainer, PropertyConstants.Modulate, new Color(fadeObjectsContainer.Modulate, 0), GetDuration(instant));
        tween.TweenCallback(Callable.From(() => onComplete?.Invoke()));
    }

    private static float GetDuration(bool instant)
    {
        return instant ? 0 : TransitionDuration;
    }

}
