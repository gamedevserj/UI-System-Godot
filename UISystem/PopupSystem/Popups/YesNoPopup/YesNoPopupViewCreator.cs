using Godot;
using UISystem.Core.Views;
using UISystem.PopupSystem.Popups.Views;

namespace UISystem.PopupSystem.Popups.ViewHandlers;
internal class YesNoPopupViewCreator<TView> : PopupViewCreator<YesNoPopupView>, IViewCreator<YesNoPopupView>
{

    public YesNoPopupViewCreator(string prefab, Node parent) : base(prefab, parent)
    { }

    
}
