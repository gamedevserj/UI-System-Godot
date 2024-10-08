using UISystem.Core.MenuSystem;
using UISystem.Core.MenuSystem.Controllers;
using UISystem.Core.MenuSystem.Enums;
using UISystem.MenuSystem.Models;
using UISystem.MenuSystem.Views;

namespace UISystem.MenuSystem.Controllers;
public partial class OptionsMenuController : MenuController<OptionsMenuView, OptionsMenuModel>
{

    public override int Menu => MenuType.Options;

    public OptionsMenuController(string prefab, OptionsMenuModel model, MenusManager menusManager) 
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
        _menusManager.ShowMenu(MenuType.AudioSettings, MenuStackBehaviourEnum.AddToStack);
    }

    private void OnVideoSettingsButtonDown()
    {
        _lastSelectedElement = _view.VideoSettingsButton;
        _menusManager.ShowMenu(MenuType.VideoSettings, MenuStackBehaviourEnum.AddToStack);
    }

    private void OnRebindKeysButtonDown()
    {
        _lastSelectedElement = _view.RebindKeysButton;
        _menusManager.ShowMenu(MenuType.RebindKeys, MenuStackBehaviourEnum.AddToStack);
    }

    private void OnInterfaceSettingsButtonDown()
    {
        _lastSelectedElement = _view.InterfaceSettingsButton;
        _menusManager.ShowMenu(MenuType.InterfaceSettings, MenuStackBehaviourEnum.AddToStack);
    }

}
