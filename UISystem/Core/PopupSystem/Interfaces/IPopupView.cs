using UISystem.Core.Views.Interfaces;

namespace UISystem.Core.PopupSystem.Interfaces;
internal interface IPopupView : IView
{

    void SetMessage(string message);

}
