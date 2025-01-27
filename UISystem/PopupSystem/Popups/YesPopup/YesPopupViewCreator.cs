using Godot;
using UISystem.Core.Views;
using UISystem.PopupSystem.Popups.Views;

namespace UISystem.PopupSystem.Popups.ViewHandlers;
internal class YesPopupViewCreator<TView> : PopupViewCreator<YesPopupView>, IViewCreator<YesPopupView>
{

    public YesPopupViewCreator(string prefab, Node parent) : base(prefab, parent)
    { }

    
}
