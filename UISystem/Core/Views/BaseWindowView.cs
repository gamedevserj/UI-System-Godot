using Godot;
using System;
using UISystem.Core.Transitions.Interfaces;

namespace UISystem.Core.Views;
public abstract partial class BaseWindowView : Control
{

    protected IViewTransition _transition;

    public abstract void Init();
    public abstract void Show(Action onShown, bool instant = false);
    public abstract void Hide(Action onHidden, bool instant = false);
    public abstract void SwitchFocusAwailability(bool enable);

}
