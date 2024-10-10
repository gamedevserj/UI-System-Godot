using static Godot.Tween;

namespace UISystem.Common.HoverSettings;
public struct TransitionAndEaseSettings
{

    public EaseType Ease = EaseType.Out;
    public EaseType ResetEase = EaseType.Out;
    public TransitionType Transition = TransitionType.Elastic;
    public TransitionType ResetTransition = TransitionType.Back;

    public TransitionAndEaseSettings(EaseType ease, EaseType resetEase, TransitionType transition, TransitionType resetTransition)
    {
        Ease = ease;
        ResetEase = resetEase;
        Transition = transition;
        ResetTransition = resetTransition;
    }
}
