using Godot;
using System;
using UISystem.Core.Transitions.Interfaces;
using UISystem.Core.Views;

namespace UISystem.MenuSystem.Views;
public partial class InGameMenuView : BaseWindowView
{

    [Export] private Control fadeObjectsContainer;

    public Control FadeObjectsContainer => fadeObjectsContainer;

    public override void Init(IViewTransition transition)
    {
        _transition = transition;
    }

    public override void Show(Action onShown, bool instant = false)
    {
        _transition.Show(onShown, instant);
    }
    public override void Hide(Action onHidden, bool instant = false)
    {
        _transition.Hide(onHidden, instant);
    }

    public override void SwitchFocusAvailability(bool enable)
    {
        
    }

    public override void FocusElement()
    {
        throw new NotImplementedException();
    }
}
