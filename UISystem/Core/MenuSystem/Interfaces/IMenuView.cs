using UISystem.Core.Views.Interfaces;

namespace UISystem.Core.MenuSystem.Interfaces;
internal interface IMenuView<TInteractableElement> : IView
{

    void SetLastSelectedElement(TInteractableElement lastSelectedElement);

}
