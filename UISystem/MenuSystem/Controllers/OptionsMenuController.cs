using Godot;
using MenuSystem.Enums;
using MenuSystem.Views;
using MenuSystem.Models;

namespace MenuSystem.Controllers;
public partial class OptionsMenuController : MenuControllerFade<OptionsMenuView, OptionsMenuModel>
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
        _view.AudioSettingsButton.ButtonDown += PressedAudioSettingsButton;
        _view.VideoSettingsButton.ButtonDown += PressedVideoSettingsButton;
    }

    private void PressedAudioSettingsButton()
    {
        _lastSelectedElement = _view.AudioSettingsButton;
        _menusManager.ChangeMenu(MenuType.AudioSettings, MenuStackBehaviourEnum.AddToStack);
    }

    private void PressedVideoSettingsButton()
    {
        _lastSelectedElement = _view.VideoSettingsButton;
        _menusManager.ChangeMenu(MenuType.VideoSettings, MenuStackBehaviourEnum.AddToStack);
    }

}
