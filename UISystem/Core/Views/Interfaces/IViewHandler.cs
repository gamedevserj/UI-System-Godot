using UISystem.Core.Transitions.Interfaces;

namespace UISystem.Core.Views.Interfaces;
internal interface IViewHandler<TView>
{

    bool IsViewValid { get; }
    TView CreateView();
    IViewTransition CreateTransition();
    void DestroyView();
    void SwitchFocusAvailability(bool enable);

}
