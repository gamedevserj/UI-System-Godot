using Godot;
using System.Collections.Generic;
using UISystem.Core.MenuSystem.Interfaces;

namespace UISystem.Core.MenuSystem;
public partial class MenusManager : Control
{

    private IMenuController _currentController;
    private Stack<IMenuController> _previousMenus = new();
    private Dictionary<int, IMenuController> _controllers = new();

    public override void _Input(InputEvent @event)
    {
        _currentController?.HandleInputPressedWhenActive(@event);
    }

}
