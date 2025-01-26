namespace UISystem.Core.PhysicalInput;
public partial interface IInputProcessor<TInputEvent>
{

    void ProcessInput(TInputEvent input);

}
