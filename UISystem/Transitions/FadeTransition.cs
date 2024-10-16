﻿using Godot;
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
        Fader.Hide(SceneTree, _target, onHidden, instant);
    }

    public void Show(Action onShown, bool instant)
    {
        Fader.Show(SceneTree, _target, onShown, instant);
    }
}
