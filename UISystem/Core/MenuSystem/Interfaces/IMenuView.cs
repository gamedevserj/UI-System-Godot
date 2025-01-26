using UISystem.Core.Views;

namespace UISystem.Core.MenuSystem;
internal interface IMenuView<TInteractableElement> : IView
{

    void SetLastSelectedElement(TInteractableElement lastSelectedElement);

}
