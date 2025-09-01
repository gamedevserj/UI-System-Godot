using System;
using UISystem.Core.MenuSystem;
using UISystem.Core.Views;
using UISystem.MenuSystem.Views;

namespace UISystem.MenuSystem.Controllers;
internal class OptionsMenuController : MenuControllerBase<IViewCreator<OptionsMenuView>, OptionsMenuView>
{

    public OptionsMenuController(IViewCreator<OptionsMenuView> viewCreator, IMenuModel model, IMenusManager menusManager) : base(viewCreator, model, menusManager)
    { }   

    protected override void SetupElements()
    {
        _view.ReturnButton.ButtonDown += OnReturnButtonDown;
        _view.AudioSettingsButton.ButtonDown += OnAudioSettingsButtonDown;
        _view.VideoSettingsButton.ButtonDown += OnVideoSettingsButtonDown;
        _view.RebindKeysButton.ButtonDown += OnRebindKeysButtonDown;
        _view.InterfaceSettingsButton.ButtonDown += OnInterfaceSettingsButtonDown;
    }

    private void OnAudioSettingsButtonDown()
    {
        _view.SetLastSelectedElement(_view.AudioSettingsButton);
        _menusManager.ShowMenu(typeof(AudioSettingsMenuController));
    }

    private void OnVideoSettingsButtonDown()
    {
        _view.SetLastSelectedElement(_view.VideoSettingsButton);
        _menusManager.ShowMenu(typeof(VideoSettingsMenuController));
    }

    private void OnRebindKeysButtonDown()
    {
        _view.SetLastSelectedElement(_view.RebindKeysButton);
        _menusManager.ShowMenu(typeof(RebindKeysMenuController));
    }

    private void OnInterfaceSettingsButtonDown()
    {
        _view.SetLastSelectedElement(_view.InterfaceSettingsButton);
        _menusManager.ShowMenu(typeof(InterfaceSettingsMenuController));
    }
}
