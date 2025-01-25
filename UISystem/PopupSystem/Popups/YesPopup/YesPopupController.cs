﻿using Godot;
using UISystem.Core.PopupSystem.Controllers;
using UISystem.Core.PopupSystem.Interfaces;
using UISystem.PopupSystem.Constants;
using UISystem.PopupSystem.Popups.ViewHandlers;
using UISystem.PopupSystem.Popups.Views;

namespace UISystem.PopupSystem.Popups.Controllers;
internal class YesPopupController<TViewHandler, TInputEvent> : PopupController<YesPopupViewHandler<YesPopupView>, InputEvent, YesPopupView>
{

    public override int Type => PopupType.Yes;
    public override int PressedReturnPopupResult => PopupResult.Yes;

    public YesPopupController(YesPopupViewHandler<YesPopupView> viewHandler, IPopupsManager<InputEvent> popupsManager) : base(viewHandler, popupsManager)
    { }

    protected override void SetupElements()
    {
        View.YesButton.ButtonDown += () => _popupsManager.HidePopup(PopupResult.Yes);
    }

}
