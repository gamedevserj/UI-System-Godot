using Godot;
using UISystem.Core.Transitions;
using UISystem.Core.Views.Interfaces;
using UISystem.PopupSystem.Popups.Views;
using UISystem.Transitions;
using UISystem.Transitions.Interfaces;

namespace UISystem.PopupSystem.Popups.ViewHandlers;
internal class YesNoPopupViewHandler<TView> : PopupViewHandler<YesNoPopupView>, IViewHandler<YesNoPopupView>
{

    public YesNoPopupViewHandler(string prefab, Node parent) : base(prefab, parent)
    { }

    public override IViewTransition CreateTransition()
    {
        return new PanelSizeTransition(_view, _view.FadeObjectsContainer, _view.Panel,
            new ITweenableMenuElement[] { _view.YesButton, _view.NoButton, _view.MessageMask });
    }
}
