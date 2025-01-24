namespace UISystem.Core.PhysicalInput;
internal partial interface IInputProcessor<TInputEvent>
{

    bool IsPressingReturnToPreviousMenuButton(TInputEvent inputEvent);
    bool IsPressingPause(TInputEvent inputEvent);
    bool IsPressingAnyKey(TInputEvent inputEvent);

}
