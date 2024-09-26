using Godot;

namespace UISystem.Common.Resources;
public abstract partial class TweenSettings : Resource
{

    [Export] private float duration = 1;
    [Export] private Tween.EaseType ease = Tween.EaseType.Out;
    [Export] private Tween.TransitionType transition = Tween.TransitionType.Elastic;

    public float Duration => duration;
    public Tween.EaseType Ease => ease;
    public Tween.TransitionType Transition => transition;

}
