namespace UISystem.Core.PhysicalInput;
public partial interface IInputReceiver<TInputEvent>
{

    bool CanReceivePhysicalInput { get; }

    void OnReturnButtonDown();
    void OnPauseButtonDown();
    void OnAnyButtonDown(TInputEvent inputEvent);

}
