using UISystem.Core.Views.Interfaces;

namespace UISystem.Core.MenuSystem;
internal interface IMenuView<TInteractableElement> : IView
{

    void SetLastSelectedElement(TInteractableElement lastSelectedElement);

}
