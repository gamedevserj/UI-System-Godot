using System;
using UISystem.Common.Enums;

namespace UISystem.Common.Interfaces;
public interface ITweener
{

    void Tween(ControlDrawMode mode);
    void Kill();
    void Reset(Action onComplete);

}
