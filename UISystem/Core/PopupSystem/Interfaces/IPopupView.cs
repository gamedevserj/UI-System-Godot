using UISystem.Core.Views;

namespace UISystem.Core.PopupSystem;
internal interface IPopupView : IView
{

    void SetMessage(string message);

}
