using Godot;
using UISystem.Core.Enums;

namespace UISystem.Core.Interfaces;
public interface IHoverTweener
{

    void Tween(Tween tween, ControlDrawMode mode);
    void Reset(Tween tween);

}
