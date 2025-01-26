namespace UISystem.Core;
internal interface IManager<TController, TInputEvent> where TController : IController<TInputEvent>
{
    void Init(TController[] controllers);
}
