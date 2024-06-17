using Godot;
using UISystem.Common;
using UISystem.MenuSystem.Interfaces;

namespace UISystem.MenuSystem.Views;
public partial class MenuView : BaseInteractableView, IMenuView
{

    [Export] private Control fadeObjectsContainer;

    public Control FadeObjectsContainer => fadeObjectsContainer;

}
