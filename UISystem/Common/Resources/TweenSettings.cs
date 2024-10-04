﻿using Godot;
using System;
using UISystem.Common.Enums;
using UISystem.Common.Interfaces;

namespace UISystem.Common.Resources;
public abstract partial class TweenSettings<T> : Resource
{

    [Export] private float duration = 1;
    [Export] private float resetDuration = 0.25f;
    [Export] private Tween.EaseType ease = Tween.EaseType.Out;
    [Export] private Tween.TransitionType transition = Tween.TransitionType.Elastic;
    [Export] private Tween.TransitionType resetTransition = Tween.TransitionType.Back;

    public float Duration => duration;
    public float ResetDuration => resetDuration;

    public abstract T HoverValue { get; }
    public abstract T FocusValue { get; }
    public abstract T FocusHoverValue { get; }
    public abstract T DisabledValue { get; }


    protected abstract class Tweener<T> : ITweener
    {

        protected Tween _tween;
        protected T _originalValue;

        protected readonly SceneTree _tree;
        protected readonly Control _target;
        protected readonly bool _parallel;
        protected readonly TweenSettings<T> _settings;

        public Tweener(SceneTree tree, Control target, bool parallel, TweenSettings<T> settings, T originalValue)
        {
            _tree = tree;
            _target = target;
            _parallel = parallel;
            _settings = settings;
            _originalValue = originalValue;
        }
        
        public void Tween(ControlDrawMode mode)
        {
            Tween(SelectValue(mode));
        }
        
        public void Kill() => _tween?.Kill();

        public virtual void Reset(Action onComplete)
        {
            _tween?.Kill();
            _tween = _tree.CreateTween();
            _tween.SetEase(_settings.ease).SetTrans(_settings.resetTransition);
            _tween.Finished += () => onComplete?.Invoke();
        }

        protected virtual void Tween(T value)
        {
            _tween?.Kill();
            _tween = _tree.CreateTween();
            _tween.SetEase(_settings.ease).SetTrans(_settings.transition);
        }

        private T SelectValue(ControlDrawMode mode) => mode switch
        {
            ControlDrawMode.Normal => _originalValue,
            ControlDrawMode.Hover => _settings.HoverValue,
            ControlDrawMode.Focus => _settings.FocusValue,
            ControlDrawMode.HoverFocus => _settings.FocusHoverValue,
            ControlDrawMode.Disabled => _settings.DisabledValue,
            _ => _originalValue,
        };
    }

}
