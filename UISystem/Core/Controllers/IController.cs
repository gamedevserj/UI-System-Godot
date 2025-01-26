namespace UISystem.Core;
public interface IController<TInputEvent>
{

    int Type { get; }
    void Init();

}
