using Godot;
using UISystem.Common.Enums;

namespace UISystem.Common.Interfaces;
public interface ITweener
{

    void Tween(Tween tween, ControlDrawMode mode);
    void Reset(Tween tween);

}
