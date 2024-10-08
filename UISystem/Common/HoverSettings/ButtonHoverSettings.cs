using Godot;
using UISystem.Common.Enums;
using UISystem.Common.Interfaces;

namespace UISystem.Common.HoverSettings;
[GlobalClass]
public partial class ButtonHoverSettings : Resource
{

    [Export] private SizeTweenSettings sizeChangeSettings;
    [Export] private ColorTweenSettings colorChangeSettings;

    public ITweener CreateTweener(Control sizeTarget, Control colorTarget, bool sizeParallel = true,
        bool colorParallel = true)
    {
        return new SizeAndColorTweenerFacade(sizeTarget, colorTarget, sizeChangeSettings, colorChangeSettings,
            sizeParallel, colorParallel);
    }

    private class SizeAndColorTweenerFacade : ITweener
    {

        private readonly ITweener[] _tweeners;

        public SizeAndColorTweenerFacade(Control sizeTarget, Control colorTarget, SizeTweenSettings sizeSettings,
            ColorTweenSettings colorSettings, bool sizeParallel, bool colorParallel)
        {
            _tweeners = new ITweener[] {
                sizeSettings?.CreateTweener(sizeTarget, sizeParallel),
                colorSettings?.CreateTweener(colorTarget, colorParallel),
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
