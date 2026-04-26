using Godot;
using System;
using System.Threading.Tasks;
using UISystem.Helpers;

namespace UISystem.ScreenFade;
public partial class ScreenFadeManager : TextureRect
{

    private bool _isFading;

    public async Task FadeOut(Action onFadeOutComplete = null)
    {
        if (_isFading)
            return;

        _isFading = true;
        MouseFilter = MouseFilterEnum.Stop;

        await Fader.Show(GetTree(), this);
        onFadeOutComplete?.Invoke();

        await Fader.Hide(GetTree(), this);
        _isFading = false;
        MouseFilter = MouseFilterEnum.Ignore;
    }

}
