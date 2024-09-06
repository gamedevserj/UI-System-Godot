using Godot;
using System;
using UISystem.Constants;

namespace UISystem.PopupSystem.Views;
public partial class PopupViewFade : PopupView
{

    protected const float fadeDuration = 0.25f;

    public override void Show(Action onShown)
    {
        Tween tween = GetTree().CreateTween();
        tween.SetPauseMode(Tween.TweenPauseMode.Process);
        tween.TweenProperty(this, PropertyConstants.Modulate, new Color(Modulate, 1), fadeDuration);
        tween.TweenCallback(Callable.From(() => { onShown?.Invoke(); }));
    }
    public override void Hide(Action onHidden)
    {
        Tween tween = GetTree().CreateTween();
        tween.SetPauseMode(Tween.TweenPauseMode.Process);
        tween.TweenProperty(this, PropertyConstants.Modulate, new Color(Modulate, 0), fadeDuration);
        tween.TweenCallback(Callable.From(() => { onHidden?.Invoke(); }));
    }

}
