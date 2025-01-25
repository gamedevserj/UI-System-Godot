using UISystem.Core.Transitions.Interfaces;

namespace UISystem.Core.Views.Interfaces;
internal interface IViewHandler<TView>
{

    TView GetOrCreateView();
    IViewTransition CreateTransition();
    void DestroyView();
    void SwitchFocusAvailability(bool enable);

}
