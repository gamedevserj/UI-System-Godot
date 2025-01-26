namespace UISystem.Core.PhysicalInput;
public partial interface IInputReceiver<TInputEvent>
{

    bool CanReceivePhysicalInput { get; }
    void OnCancelButtonDown();
    void OnPauseButtonDown();
    void OnAnyButtonDown(TInputEvent inputEvent);

}
