namespace UISystem.Core.PhysicalInput;
public partial interface IInputProcessor<TInputEvent>
{

    void ProcessInput(TInputEvent input, IInputReceiver<TInputEvent> inputReceiver);
    //bool IsPressingCancel(TInputEvent inputEvent);
    //bool IsPressingPause(TInputEvent inputEvent);
    //bool IsPressingAnyKey(TInputEvent inputEvent);

}
