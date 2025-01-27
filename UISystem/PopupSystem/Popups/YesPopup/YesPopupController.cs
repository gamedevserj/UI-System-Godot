using Godot;
using UISystem.Core.PopupSystem;
using UISystem.PopupSystem.Constants;
using UISystem.PopupSystem.Popups.Views;
using UISystem.Views;

namespace UISystem.PopupSystem.Popups.Controllers;
internal class YesPopupController<TViewHandler, TInputEvent> : PopupControllerBase<ViewCreator<YesPopupView>, YesPopupView>
{

    public override int Type => PopupType.Yes;
    public override int PressedReturnPopupResult => PopupResult.Yes;

    public YesPopupController(ViewCreator<YesPopupView> viewHandler, IPopupsManager<InputEvent> popupsManager) : base(viewHandler, popupsManager)
    { }

    protected override void SetupElements()
    {
        _view.YesButton.ButtonDown += () => _popupsManager.HidePopup(PopupResult.Yes);
    }

}
