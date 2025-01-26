using UISystem.Core.Views.Interfaces;

namespace UISystem.Core.PopupSystem;
internal interface IPopupView : IView
{

    void SetMessage(string message);

}
