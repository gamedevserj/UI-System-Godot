using System.Collections.Generic;

namespace UISystem.Core;
public abstract class Manager<TController, TInputEvent> : IManager<TController, TInputEvent> where TController : IController<TInputEvent>
{

    protected TController _currentController;
    protected Dictionary<int, TController> _controllers = new();

    public void Init(TController[] controllers)
    {
        for (int i = 0; i < controllers.Length; i++)
        {
            _controllers.Add(controllers[i].Type, controllers[i]);
        }
    }

}
