using Godot;
using MenuSystem;
using MenuSystem.Enums;
using PopupSystem;
using UISystem.Common.Interfaces;
using UISystem.ScreenFade;

namespace UISystem;
public partial class UiInstaller : Control
{

    private static UiInstaller instance;
    public static UiInstaller Instance { get => instance; private set => instance = value; }

    [Export] private TextureRect menuBackground;
    [Export] private MenusManager menusManager;
    [Export] private PopupsManager popupsManager;
    [Export] private ScreenFadeManager screenFadeManager;

    public override void _EnterTree()
    {
        instance ??= this;
    }

    public void Init(ConfigFile config)
    {
        popupsManager.Init();
        menusManager.Init(config, popupsManager, screenFadeManager, new MenuBackgroundController(GetTree(), menuBackground));
        menusManager.ChangeMenu(MenuType.Main, MenuStackBehaviourEnum.ClearStack);
    }

}
