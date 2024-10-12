using Godot;
using UISystem.Helpers;

namespace UISystem.MenuSystem;
public class MenuBackgroundController
{

    private readonly SceneTree _sceneTree;
    private readonly TextureRect _background;

    public MenuBackgroundController(SceneTree sceneTree, TextureRect background)
    {
        _sceneTree = sceneTree;
        _background = background;
    }

    public void SetBackgroundColor(Color backgroundColor) => _background.Modulate = new Color(backgroundColor, 1);

    public void ShowBackground(bool instant) => Fader.Show(_sceneTree, _background, null, instant);

    public void HideBackground(bool instant) => Fader.Hide(_sceneTree, _background, null, instant);

}
