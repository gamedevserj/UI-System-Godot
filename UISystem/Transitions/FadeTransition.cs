using Godot;
using System;
using UISystem.Core.Transitions.Interfaces;
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

    public void Hide(Action onHidden, bool instant)
    {
        if(instant)
        {
            _target.Modulate = new Color(_target.Modulate, 0);
            onHidden?.Invoke();
            return;
        }
        Fader.Hide(SceneTree, _target, onHidden, instant);
    }

    public void Show(Action onShown, bool instant)
    {
        // should always hide before showing because awaiting for parameters shows menu for a split second
        _target.Modulate = new Color(_target.Modulate, 0);

        if (instant)
        {
            _target.Modulate = new Color(_target.Modulate, 1);
            onShown?.Invoke();
            return;
        }
        Fader.Show(SceneTree, _target, onShown, instant);
    }
}
