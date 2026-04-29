using System.Threading.Tasks;
using Godot;
using UISystem.Helpers;

namespace UISystem.ScreenFade;

/// <summary>
/// Screen fade manager.
/// </summary>
public partial class ScreenFadeManager : TextureRect
{
    private bool _isFading;

    /// <summary>
    /// Fades screen out.
    /// </summary>
    public async Task FadeOut()
    {
        if (_isFading)
            return;

        _isFading = true;
        MouseFilter = MouseFilterEnum.Stop;

        await Fader.Show(GetTree(), this);
    }

    /// <summary>
    /// Fades screen in.
    /// </summary>
    public async Task FadeIn()
    {
        await Fader.Hide(GetTree(), this);
        _isFading = false;
        MouseFilter = MouseFilterEnum.Ignore;
    }
}
