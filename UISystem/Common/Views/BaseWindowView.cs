using Godot;
using System;
using UISystem.Core.Extensions;
using UISystem.Core.Transitions;
using UISystem.Core.Views;

namespace UISystem.Views;
public abstract partial class BaseWindowView : Control, IView
{

    protected IViewTransition _transition;

    public bool IsValid => this.IsValid();

    public abstract void Init(IViewTransition transition);
    public abstract void Show(Action onShown, bool instant = false);
    public abstract void Hide(Action onHidden, bool instant = false);
    public abstract void SwitchFocusAvailability(bool enable);
    public abstract void FocusElement();

    public void DestroyView() => this.SafeQueueFree();

}
