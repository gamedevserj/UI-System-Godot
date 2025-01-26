using UISystem.Core.Transitions;

namespace UISystem.Core.Views;
internal interface IViewHandler<TView>
{

    bool IsViewValid { get; }
    TView CreateView();
    IViewTransition CreateTransition();
    void DestroyView();
    void SwitchFocusAvailability(bool enable);

}
