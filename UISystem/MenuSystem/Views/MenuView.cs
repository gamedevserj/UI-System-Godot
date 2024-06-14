using Godot;
using MenuSystem.Interfaces;
using UISystem.Common;

namespace MenuSystem.Views;
public partial class MenuView : BaseMenuView, IMenuView
{

    [Export] private Control fadeObjectsContainer;

    public Control FadeObjectsContainer => fadeObjectsContainer;

}
