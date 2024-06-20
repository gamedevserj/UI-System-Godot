using Godot;
using UISystem.Constants;

namespace UISystem;
public partial class GameInstaller : Node
{

    public override void _Ready()
    {
        var config = new ConfigFile();
        Error err = config.Load(ConfigData.ConfigLocation);
        GameSettings settings = new(config, err);

        UiInstaller.Instance.Init(config, settings);
    }

}
