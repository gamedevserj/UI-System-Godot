using Godot;

namespace UISystem.Hovering;
public interface IHoverTweener
{

    void Tween(Tween tween, ControlDrawMode mode);
    void Reset(Tween tween);

}
