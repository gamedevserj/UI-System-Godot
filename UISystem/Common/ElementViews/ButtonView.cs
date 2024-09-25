using Godot;
using UISystem.Common.ElementViews;
using UISystem.Common.Interfaces;

namespace UISystem.Common.Elements;
public partial class ButtonView : BaseButton, IFocusableControl
{

    [Export] private AnimatedButtonView animatedButtonView;

    public AnimatedButtonView AnimatedButtonView => animatedButtonView;

    public override void _EnterTree()
    {
        animatedButtonView.Init(this);
    }

}
