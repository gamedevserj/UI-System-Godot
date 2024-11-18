using UISystem.Core.MenuSystem.Controllers;
using UISystem.Core.MenuSystem.Interfaces;
using UISystem.MenuSystem.Constants;
using UISystem.MenuSystem.Models;
using UISystem.MenuSystem.Views;

namespace UISystem.MenuSystem.Controllers;
internal class OptionsMenuController : MenuController<OptionsMenuView, OptionsMenuModel>
{

    public override int Type => MenuType.Options;

    public OptionsMenuController(string prefab, OptionsMenuModel model, IMenusManager menusManager)
        : base(prefab, model, menusManager)
    {
    }

    protected override void SetupElements()
    {
        _view.ReturnButton.ButtonDown += OnReturnButtonDown;
        _view.AudioSettingsButton.ButtonDown += OnAudioSettingsButtonDown;
        _view.VideoSettingsButton.ButtonDown += OnVideoSettingsButtonDown;
        _view.RebindKeysButton.ButtonDown += OnRebindKeysButtonDown;
        _view.InterfaceSettingsButton.ButtonDown += OnInterfaceSettingsButtonDown;
        DefaultSelectedElement = _view.InterfaceSettingsButton;
    }

    private void OnReturnButtonDown()
    {
        OnReturnToPreviousMenuButtonDown();
    }

    private void OnAudioSettingsButtonDown()
    {
        _lastSelectedElement = _view.AudioSettingsButton;
        _menusManager.ShowMenu(MenuType.AudioSettings);
    }

    private void OnVideoSettingsButtonDown()
    {
        _lastSelectedElement = _view.VideoSettingsButton;
        _menusManager.ShowMenu(MenuType.VideoSettings);
    }

    private void OnRebindKeysButtonDown()
    {
        _lastSelectedElement = _view.RebindKeysButton;
        _menusManager.ShowMenu(MenuType.RebindKeys);
    }

    private void OnInterfaceSettingsButtonDown()
    {
        _lastSelectedElement = _view.InterfaceSettingsButton;
        _menusManager.ShowMenu(MenuType.InterfaceSettings);
    }

}
