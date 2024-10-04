using Godot;
using System;
using UISystem.Common.Enums;
using UISystem.Common.Interfaces;

namespace UISystem.Common.Resources;
[GlobalClass]
public partial class ButtonHoverSettings : Resource
{

    [Export] private SizeTweenSettings sizeChangeSettings;
    [Export] private ColorTweenSettings colorChangeSettings;

    public SizeTweenSettings SizeChangeSettings => sizeChangeSettings;
    public ColorTweenSettings ColorChangeSettings => colorChangeSettings;

    public ITweener CreateTweener(SceneTree tree, Control sizeTarget, Control colorTarget, bool sizeParallel = true, 
        bool colorParallel = true)
    {
        return new SizeAndColorTweenerFacade(tree, sizeTarget, colorTarget, SizeChangeSettings, ColorChangeSettings, 
            sizeParallel, colorParallel);
    }

    private class SizeAndColorTweenerFacade : ITweener
    {

        private readonly ITweener _sizeTweener;
        private readonly ITweener _colorTweener;

        public SizeAndColorTweenerFacade(SceneTree tree, Control sizeTarget, Control colorTarget, SizeTweenSettings sizeSettings, 
            ColorTweenSettings colorSettings, bool sizeParallel, bool colorParallel)
        {
            _sizeTweener = sizeSettings?.CreateTweener(tree, sizeTarget, sizeParallel);
            _colorTweener = colorSettings?.CreateTweener(tree, colorTarget, colorParallel);
        }

        public void Reset(Action onComplete)
        {
            _sizeTweener?.Reset(onComplete);
            _colorTweener?.Reset(null);
        }

        public void Tween(ControlDrawMode mode)
        {
            _sizeTweener?.Tween(mode);
            _colorTweener?.Tween(mode);
        }

        public void Kill()
        {
            _sizeTweener?.Kill();
            _colorTweener?.Kill();
        }
    }

}
