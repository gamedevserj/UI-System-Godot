using UISystem.Core.Transitions;

namespace UISystem.Core.Views;
internal interface IViewModel<TView>
{

    bool IsViewValid { get; }
    TView CreateView();
    void DestroyView();
    void SwitchFocusAvailability(bool enable);

}
