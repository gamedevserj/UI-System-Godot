using Godot;
using UISystem.Elements;

namespace UISystem.MenuSystem.Views;
public partial class InGameMenuView : MenuView
{

    [Export] private Control fadeObjectsContainer;

    public Control FadeObjectsContainer => fadeObjectsContainer;

    protected override IFocusableControl DefaultSelectedElement => null;

    protected override void PopulateFocusableElements()
    { }

}
