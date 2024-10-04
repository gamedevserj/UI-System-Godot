using Godot;
using System;

namespace UISystem.Common.Interfaces;
public interface ISizeTweenable
{

    Control ResizableControl { get; }
    void PrepareForSizeTweening(Action onComplete);

}
