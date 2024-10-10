using Godot;
using UISystem.Common.Enums;
using UISystem.Common.Interfaces;

namespace UISystem.Common.HoverSettings.ElementHoverSettings;
[GlobalClass]
public partial class ButtonHoverSettings : Resource
{

    [Export] private Tween.EaseType ease = Tween.EaseType.Out;
    [Export] private Tween.EaseType resetEase = Tween.EaseType.Out;
    [Export] private Tween.TransitionType transition = Tween.TransitionType.Elastic;
    [Export] private Tween.TransitionType resetTransition = Tween.TransitionType.Back;
    [Export] private SizeTweenSettings sizeChangeSettings;
    [Export] private ColorTweenSettings borderColorChangeSettings;
    [Export] private PositionTweenSettings positionChangeSettings;

    public ITweener CreateTweener(Control sizeTarget, Control borderColorTarget, Control positionTarget, bool sizeParallel = true,
        bool colorParallel = true)
    {
        return new ButtonTweenerFacade(new TransitionAndEaseSettings(ease, resetEase, transition, resetTransition),
            sizeTarget, borderColorTarget, 
            sizeChangeSettings, borderColorChangeSettings, positionTarget, positionChangeSettings,
            sizeParallel, colorParallel);
    }

    private class ButtonTweenerFacade : ITweener
    {

        private readonly ITweener[] _tweeners;

        public ButtonTweenerFacade(TransitionAndEaseSettings transitionAndEaseSettings, Control sizeTarget, Control colorTarget, 
            SizeTweenSettings sizeSettings, ColorTweenSettings colorSettings, Control positionTarget, PositionTweenSettings positionSettings, bool sizeParallel, bool colorParallel)
        {
            _tweeners = new ITweener[] {
                sizeSettings?.CreateTweener(sizeTarget, transitionAndEaseSettings, sizeParallel),
                colorSettings?.CreateTweener(colorTarget, transitionAndEaseSettings, colorParallel),
                positionSettings?.CreateTweener(positionTarget, transitionAndEaseSettings, true),
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
