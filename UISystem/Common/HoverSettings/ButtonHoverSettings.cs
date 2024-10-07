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

        private readonly ITweener _sizeTweener;
        private readonly ITweener _colorTweener;

        public SizeAndColorTweenerFacade(Control sizeTarget, Control colorTarget, SizeTweenSettings sizeSettings, 
            ColorTweenSettings colorSettings, bool sizeParallel, bool colorParallel)
        {
            _sizeTweener = sizeSettings?.CreateTweener(sizeTarget, sizeParallel);
            _colorTweener = colorSettings?.CreateTweener(colorTarget, colorParallel);
        }

        public void Reset(Tween tween)
        {
            _sizeTweener?.Reset(tween);
            _colorTweener?.Reset(tween);
        }

        public void Tween(Tween tween, ControlDrawMode mode)
        {
            _sizeTweener?.Tween(tween, mode);
            _colorTweener?.Tween(tween, mode);
        }

    }

}
