namespace UISystem.InputController;
internal partial interface IUIInputProcessor<TInputEvent>
{

    bool IsPressingReturnToPreviousMenuButtonDown(TInputEvent inputEvent);

}
