using System.Threading.Tasks;
using Godot;
using UISystem.Core.Transitions;
using UISystem.Helpers;

namespace UISystem.Transitions;

/// <summary>
/// Fade transition.
/// </summary>
public class FadeTransition : IViewTransition
{
    private readonly Control _target;

    private SceneTree _sceneTree;

    /// <summary>
    /// Initializes a new instance of the <see cref="FadeTransition"/> class.
    /// </summary>
    /// <param name="target">target control.</param>
    public FadeTransition(Control target)
    {
        _target = target;
        Fader.Init(_target);
    }

    private SceneTree SceneTree
    {
        get
        {
            _sceneTree ??= _target.GetTree();
            return _sceneTree;
        }
    }

    /// <inheritdoc/>
    public async Task Hide(bool instant = false)
    {
        if (instant)
        {
            _target.Modulate = new Color(_target.Modulate, 0);
            return;
        }

        await Fader.Hide(SceneTree, _target, instant);
    }

    /// <inheritdoc/>
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
