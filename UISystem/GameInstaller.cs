using Godot;
using UISystem.Constants;

namespace UISystem;
public partial class GameInstaller : Node
{

    public override void _Ready()
    {
        var config = new ConfigFile();
        Error err = config.Load(ConfigData.ConfigLocation);
        if (err != Error.Ok)
        {
            DefaultSettingsInitializer.SaveDefaultSettings(config);
        }

        UiInstaller.Instance.Init(config);
    }

}
