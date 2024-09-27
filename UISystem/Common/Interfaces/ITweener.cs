using UISystem.Common.Enums;

namespace UISystem.Common.Interfaces;
public interface ITweener
{

    void OnMouseEntered(ControlDrawMode mode);
    void OnMouseExited(ControlDrawMode mode);
    void OnFocusEntered(ControlDrawMode mode);
    void OnFocusExited(ControlDrawMode mode);

}
