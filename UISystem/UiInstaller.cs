using Godot;
using UISystem.MenuSystem;
using UISystem.MenuSystem.Enums;
using UISystem.PopupSystem;
using UISystem.ScreenFade;

namespace UISystem;
public partial class UiInstaller : Control
{

    public static UiInstaller Instance { get; private set; }

    [Export] private TextureRect menuBackground;
    [Export] private MenusManager menusManager;
    [Export] private PopupsManager popupsManager;
    [Export] private ScreenFadeManager screenFadeManager;

    public override void _EnterTree()
    {
        Instance ??= this;
    }

    public void Init(ConfigFile config, GameSettings settings)
    {
        popupsManager.Init();
        menusManager.Init(config, settings, popupsManager, screenFadeManager, new MenuBackgroundController(GetTree(), menuBackground));
        menusManager.ShowMenu(MenuType.Main, MenuStackBehaviourEnum.ClearStack);
    }

}
