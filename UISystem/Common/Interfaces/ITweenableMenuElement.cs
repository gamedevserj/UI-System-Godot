using Godot;
using System.Threading.Tasks;

namespace UISystem.Common.Interfaces;
public interface ITweenableMenuElement
{

    Control PositionControl { get; }
    Control ResizableControl { get; }
    Task ResetHover();

}
