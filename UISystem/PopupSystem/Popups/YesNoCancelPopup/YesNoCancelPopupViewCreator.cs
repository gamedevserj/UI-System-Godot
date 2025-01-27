using Godot;
using UISystem.Core.Views;
using UISystem.PopupSystem.Popups.Views;

namespace UISystem.PopupSystem.Popups.ViewHandlers;
internal class YesNoCancelPopupViewCreator<TView> : PopupViewCreator<YesNoCancelPopupView>, IViewCreator<YesNoCancelPopupView>
{

    public YesNoCancelPopupViewCreator(string prefab, Node parent) : base(prefab, parent)
    { }

    
}
