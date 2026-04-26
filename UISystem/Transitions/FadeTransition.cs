using Godot;
using System.Threading.Tasks;
using UISystem.Core.Transitions;
using UISystem.Helpers;

namespace UISystem.Transitions;
public class FadeTransition : IViewTransition
{

    private SceneTree _sceneTree;
    private SceneTree SceneTree
    {
        get
        {
            _sceneTree ??= _target.GetTree();
            return _sceneTree;
        }
    }
    private readonly Control _target;

    public FadeTransition(Control target)
    {
        _target = target;
        Fader.Init(_target);
    }

    public async Task Hide(bool instant = false)
    {
        if(instant)
        {
            _target.Modulate = new Color(_target.Modulate, 0);
            return;
        }
        await Fader.Hide(SceneTree, _target, instant);
    }

    public async Task Show(bool instant = false)
    {
        // should always hide before showing because awaiting for parameters shows menu for a split second
        _target.Modulate = new Color(_target.Modulate, 0);

        if (instant)
        {
            _target.Modulate = new Color(_target.Modulate, 1);
            return;
        }
        await Fader.Show(SceneTree, _target, instant);
    }
}
