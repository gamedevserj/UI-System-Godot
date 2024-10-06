using Godot;
using System.Threading.Tasks;

namespace UISystem.Common.Interfaces;
public interface ISizeTweenable
{

    Control ResizableControl { get; }
    Task ResetHover();

}
