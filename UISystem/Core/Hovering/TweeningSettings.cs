using static Godot.Tween;

namespace UISystem.Core.Hovering;
public struct TweeningSettings
{

    public float Duration = 1f;
    public float ResetDuration = 0.25f;
    public EaseType Ease = EaseType.Out;
    public EaseType ResetEase = EaseType.Out;
    public TransitionType Transition = TransitionType.Elastic;
    public TransitionType ResetTransition = TransitionType.Back;

    public TweeningSettings(float duration, float resetDuration,
        EaseType ease, EaseType resetEase, TransitionType transition, TransitionType resetTransition)
    {
        Duration = duration;
        ResetDuration = resetDuration;
        Ease = ease;
        ResetEase = resetEase;
        Transition = transition;
        ResetTransition = resetTransition;
    }
}
