using Godot;
using UISystem.Common.Enums;
using UISystem.Common.Interfaces;

namespace UISystem.Common.HoverSettings.ElementHoverSettings;
[GlobalClass]
public partial class ButtonHoverSettings : Resource
{

    [Export] private float duration = 1;
    [Export] private float resetDuration = 0.25f;
    [Export] private Tween.EaseType ease = Tween.EaseType.Out;
    [Export] private Tween.EaseType resetEase = Tween.EaseType.Out;
    [Export] private Tween.TransitionType transition = Tween.TransitionType.Elastic;
    [Export] private Tween.TransitionType resetTransition = Tween.TransitionType.Back;
    [Export] private SizeTweenSettings sizeChangeSettings;
    [Export] private PositionTweenSettings positionChangeSettings;
    [Export] private ColorTweenSettings borderColorChangeSettings;
    [Export] private ColorTweenSettings colorChangeSettings;
    [Export] private ColorTweenSettings labelColorChangeSettings;

    public ITweener CreateTweener(Control resizableControl, Control colorTarget, Control borderColorTarget,
        Control labelColorTarget)
    {
        return new ButtonTweenerFacade(new TweeningSettings(duration, resetDuration, ease, resetEase, transition, resetTransition),
            resizableControl, sizeChangeSettings,
            resizableControl, positionChangeSettings,
            colorTarget, colorChangeSettings,
            borderColorTarget, borderColorChangeSettings,
            labelColorTarget, labelColorChangeSettings);
    }

    private class ButtonTweenerFacade : ITweener
    {

        private readonly ITweener[] _tweeners;

        public ButtonTweenerFacade(TweeningSettings transitionAndEaseSettings, 
            Control sizeTarget, SizeTweenSettings sizeSettings,
            Control positionTarget, PositionTweenSettings positionSettings,
            Control colorTarget, ColorTweenSettings colorSettings,
            Control borderColorTarget, ColorTweenSettings borderColorSettings,
            Control labelColorTarget, ColorTweenSettings labelColorSettings)
        {
            _tweeners = new ITweener[] {
                sizeSettings?.CreateTweener(sizeTarget, transitionAndEaseSettings),
                positionSettings?.CreateTweener(positionTarget, transitionAndEaseSettings),
                colorSettings?.CreateTweener(colorTarget, transitionAndEaseSettings),
                borderColorSettings?.CreateTweener(borderColorTarget, transitionAndEaseSettings),
                labelColorSettings?.CreateTweener(labelColorTarget, transitionAndEaseSettings),
            };
        }

        public void Reset(Tween tween)
        {
            for (int i = 0; i < _tweeners.Length; i++)
            {
                _tweeners[i]?.Reset(tween);
            }
        }

        public void Tween(Tween tween, ControlDrawMode mode)
        {
            for (int i = 0; i < _tweeners.Length; i++)
            {
                _tweeners[i]?.Tween(tween, mode);
            }
        }
    }

}
