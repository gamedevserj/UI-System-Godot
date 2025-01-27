using UISystem.Core.Transitions;

namespace UISystem.Core.Views;
internal interface IViewCreator<TView>
{

    bool IsViewValid { get; }
    TView CreateView();
    void DestroyView();
    void SwitchFocusAvailability(bool enable);

}
