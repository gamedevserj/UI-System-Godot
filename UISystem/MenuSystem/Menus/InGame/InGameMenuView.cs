using Godot;
using System;
using UISystem.Core.Views;
using UISystem.Transitions;

namespace UISystem.MenuSystem.Views;
public partial class InGameMenuView : BaseWindowView
{

    [Export] private Control fadeObjectsContainer;

    public override void Init()
    {
        _transition = new FadeTransition(fadeObjectsContainer);
    }

    public override void Show(Action onShown, bool instant = false)
    {
        _transition.Show(onShown, instant);
    }
    public override void Hide(Action onHidden, bool instant = false)
    {
        _transition.Hide(onHidden, instant);
    }

    public override void SwitchFocusAwailability(bool enable)
    {
        
    }
}
