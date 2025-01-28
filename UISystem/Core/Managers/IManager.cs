using System;

namespace UISystem.Core;
internal interface IManager<TController, TInputEvent, TType> where TController : IController<TInputEvent, TType>
    where TType : Enum
{
    void Init(TController[] controllers);
}
