using System;

namespace UISystem.Core;
public interface IController<TInputEvent, TType> where TType : Enum
{

    TType Type { get; }
    void Init();

}
