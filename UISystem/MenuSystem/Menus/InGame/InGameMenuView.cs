using Godot;
using UISystem.Core.Transitions;
using UISystem.Elements;
using UISystem.Transitions;

namespace UISystem.MenuSystem.Views;
public partial class InGameMenuView : MenuView
{

    [Export] private Control fadeObjectsContainer;

    public Control FadeObjectsContainer => fadeObjectsContainer;

    protected override IFocusableControl DefaultSelectedElement => null;

    protected override IViewTransition CreateTransition()
    {
        return new FadeTransition(FadeObjectsContainer);
    }

    protected override void PopulateFocusableElements()
    { }

}
