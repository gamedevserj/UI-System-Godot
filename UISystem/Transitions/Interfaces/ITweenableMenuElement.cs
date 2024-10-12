using Godot;
using System.Threading.Tasks;

namespace UISystem.Transitions.Interfaces;
public interface ITweenableMenuElement
{

    Control PositionControl { get; }
    Control ResizableControl { get; }
    Task ResetHover();

}
