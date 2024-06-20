using Godot;
using UISystem.MenuSystem.Enums;
using UISystem.MenuSystem.Models;
using UISystem.MenuSystem.Views;

namespace UISystem.MenuSystem.Controllers;
public partial class OptionsMenuController : MenuController<OptionsMenuView, OptionsMenuModel>
{

    public override MenuType MenuType => MenuType.Options;

    public OptionsMenuController(string prefab, OptionsMenuModel model, MenusManager menusManager, SceneTree sceneTree) 
        : base(prefab, model, menusManager, sceneTree)
    {
    }

    protected override void CreateView(Node menuParent)
    {
        base.CreateView(menuParent);
        SetupElements();
        _defaultSelectedElement = _view.AudioSettingsButton;
    }

    private void SetupElements()
    {
        _view.ReturnButton.ButtonDown += OnReturnToPreviousMenuButtonDown;
        _view.AudioSettingsButton.ButtonDown += OnAudioSettingsButtonDown;
        _view.VideoSettingsButton.ButtonDown += OnVideoSettingsButtonDown;
        _view.RebindKeysButton.ButtonDown += OnRebindKeysButtonDown;
        _view.InterfaceSettingsButton.ButtonDown += OnInterfaceSettingsButtonDown;
    }

    private void OnAudioSettingsButtonDown()
    {
        _lastSelectedElement = _view.AudioSettingsButton;
        _menusManager.ChangeMenu(MenuType.AudioSettings, MenuStackBehaviourEnum.AddToStack);
    }

    private void OnVideoSettingsButtonDown()
    {
        _lastSelectedElement = _view.VideoSettingsButton;
        _menusManager.ChangeMenu(MenuType.VideoSettings, MenuStackBehaviourEnum.AddToStack);
    }

    private void OnRebindKeysButtonDown()
    {
        _lastSelectedElement = _view.RebindKeysButton;
        _menusManager.ChangeMenu(MenuType.RebindKeys, MenuStackBehaviourEnum.AddToStack);
    }

    private void OnInterfaceSettingsButtonDown()
    {
        _lastSelectedElement = _view.InterfaceSettingsButton;
        _menusManager.ChangeMenu(MenuType.InterfaceSettings, MenuStackBehaviourEnum.AddToStack);
    }

}
