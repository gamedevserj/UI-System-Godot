using System.Threading.Tasks;
using Godot;
using UISystem.Helpers;

namespace UISystem.MenuSystem;

/// <summary>
/// Menu background controller.
/// </summary>
public class MenuBackgroundController
{
    private readonly SceneTree _sceneTree;
    private readonly TextureRect _background;

    /// <summary>
    /// Initializes a new instance of the <see cref="MenuBackgroundController"/> class.
    /// </summary>
    /// <param name="sceneTree">Scene tree.</param>
    /// <param name="background">Background texture rect.</param>
    public MenuBackgroundController(SceneTree sceneTree, TextureRect background)
    {
        _sceneTree = sceneTree;
        _background = background;
    }

    /// <summary>
    /// Shows the background.
    /// </summary>
    /// <param name="instant">Whether transition should happen instantly.</param>
    public async Task ShowBackground(bool instant) => await Fader.Show(_sceneTree, _background, instant);

    /// <summary>
    /// Hides the background.
    /// </summary>
    /// <param name="instant">Whether transition should happen instantly.</param>
    public async Task HideBackground(bool instant) => await Fader.Hide(_sceneTree, _background, instant);
}
