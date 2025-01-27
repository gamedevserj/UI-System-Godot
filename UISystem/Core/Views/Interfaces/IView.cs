using System;

namespace UISystem.Core.Views;
internal interface IView
{

    // not using nullable bool to support older C# versions
    bool IsValid { get; }

    void Init();
    void Show(Action onShown, bool instant = false);
    void Hide(Action onHidden, bool instant = false);
    void DestroyView();
    void SwitchFocusAvailability(bool enable);
    void FocusElement();

}
