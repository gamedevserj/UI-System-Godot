using Godot;
using UISystem.Constants;

namespace UISystem.MenuSystem;
public class MenuBackgroundController 
{

    private const float TransitionDuration = 0.25f;
    private readonly SceneTree _sceneTree;
    private readonly TextureRect _background;

    public MenuBackgroundController(SceneTree sceneTree, TextureRect background)
    {
        _sceneTree = sceneTree;
        _background = background;
    }

    public void SetBackgroundColor(Color backgroundColor)
    {
        _background.Modulate = new Color(backgroundColor, 1);
    }

    public void ShowBackground(bool instant)
    {
        Tween tween = _sceneTree.CreateTween();
        tween.SetPauseMode(Tween.TweenPauseMode.Process);
        tween.TweenProperty(_background, PropertyConstants.Modulate, new Color(_background.Modulate, 1), GetDuration(instant));
    }

    public void HideBackground(bool instant)
    {
        Tween tween = _sceneTree.CreateTween();
        tween.SetPauseMode(Tween.TweenPauseMode.Process);
        tween.TweenProperty(_background, PropertyConstants.Modulate, new Color(_background.Modulate, 0), GetDuration(instant));
    }

    protected float GetDuration(bool instant)
    {
        return instant ? 0 : TransitionDuration;
    }

}
