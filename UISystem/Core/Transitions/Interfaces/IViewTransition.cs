using System;

namespace UISystem.Core.Transitions.Interfaces;
public interface IViewTransition
{

    void Hide(Action onHidden, bool instant);
    void Show(Action onShown, bool instant);

}
