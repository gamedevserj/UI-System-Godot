using System;

namespace UISystem.Core.PhysicalInput;
public partial interface IInputReceiver<TInputEvent>
{

    bool CanReceivePhysicalInput { get; }
    void OnCancelButtonDown(Action onComplete = null, bool instant = false);
    void OnPauseButtonDown();
    void OnResumeButtonDown();
    void OnAnyButtonDown(TInputEvent inputEvent);

}
