using System;
using System.Collections.Generic;

namespace UISystem.Core;
public abstract class Manager<TController, TInputEvent, TType> 
    : IManager<TController, TInputEvent, TType> 
    where TController : IController<TInputEvent, TType>
    where TType : Enum
{

    protected TController _currentController;
    protected Dictionary<TType, TController> _controllers = new();

    public void Init(TController[] controllers)
    {
        for (int i = 0; i < controllers.Length; i++)
        {
            _controllers.Add(controllers[i].Type, controllers[i]);
        }
    }

}
