﻿using Godot;
using UISystem.Core.Transitions.Interfaces;
using UISystem.Core.Views.Interfaces;
using UISystem.PopupSystem.Popups.Views;
using UISystem.Transitions;
using UISystem.Transitions.Interfaces;

namespace UISystem.PopupSystem.Popups.ViewHandlers;
internal class YesPopupViewHandler<TView> : PopupViewHandler<YesPopupView>, IViewHandler<YesPopupView>
{

    protected const float PanelDuration = 0.2f;
    protected const float ElementsDuration = 0.1f;

    public YesPopupViewHandler(string prefab, Node parent) : base(prefab, parent)
    { }

    public override IViewTransition CreateTransition()
    {
        return new PanelSizeTransition(_view, _view.FadeObjectsContainer, _view.Panel,
            new ITweenableMenuElement[] { _view.YesButton, _view.MessageMask },
            PanelDuration, ElementsDuration);
    }
}
