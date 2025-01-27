using Godot;
using UISystem.Core.Transitions;
using UISystem.Core.Views;
using UISystem.PopupSystem.Popups.Views;
using UISystem.Transitions;
using UISystem.Transitions.Interfaces;

namespace UISystem.PopupSystem.Popups.ViewHandlers;
internal class YesNoCancelPopupViewHandler<TView> : PopupViewHandler<YesNoCancelPopupView>, IViewModel<YesNoCancelPopupView>
{

    public YesNoCancelPopupViewHandler(string prefab, Node parent) : base(prefab, parent)
    { }

    
}
