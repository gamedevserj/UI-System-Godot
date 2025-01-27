using Godot;
using UISystem.Core.Transitions;
using UISystem.Core.Views;
using UISystem.PopupSystem.Popups.Views;
using UISystem.Transitions;
using UISystem.Transitions.Interfaces;

namespace UISystem.PopupSystem.Popups.ViewHandlers;
internal class YesNoPopupViewHandler<TView> : PopupViewHandler<YesNoPopupView>, IViewModel<YesNoPopupView>
{

    public YesNoPopupViewHandler(string prefab, Node parent) : base(prefab, parent)
    { }

    
}
