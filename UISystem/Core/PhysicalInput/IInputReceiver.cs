using System;

namespace UISystem.Core.PhysicalInput;
public partial interface IInputReceiver<TInputEvent>
{

    bool CanReceivePhysicalInput { get; }
    void OnCancelButtonDown();
    void OnPauseButtonDown();
    void OnResumeButtonDown();
    void OnAnyButtonDown(TInputEvent inputEvent);

}
