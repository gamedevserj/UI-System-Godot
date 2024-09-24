using Godot;
using System;
using UISystem.Common.Helpers;
using UISystem.MenuSystem.Interfaces;

namespace UISystem.MenuSystem.ViewTransitions;
public class FadeTransition : IViewTransition
{

    private SceneTree _sceneTree;
    private SceneTree SceneTree
    {
        get
        {
            _sceneTree ??= _fadeObjectsContainer.GetTree();
            return _sceneTree;
        }
    }
    private readonly Control _fadeObjectsContainer;

    public FadeTransition(Control fadeObjectsContainer)
    {
        _fadeObjectsContainer = fadeObjectsContainer;
        Fader.Init(_fadeObjectsContainer);
    }

    public void Hide(Action onHidden, bool instant)
    {
        Fader.Hide(SceneTree, _fadeObjectsContainer, onHidden, instant);
    }

    public void Show(Action onShown, bool instant)
    {
        Fader.Show(SceneTree, _fadeObjectsContainer, onShown, instant);
    }
}
