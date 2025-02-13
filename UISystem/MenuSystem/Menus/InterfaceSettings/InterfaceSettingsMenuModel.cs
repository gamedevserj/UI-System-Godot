using UISystem.Common.Enums;
using UISystem.Constants;
using UISystem.Core.MenuSystem;

namespace UISystem.MenuSystem.Models;
public class InterfaceSettingsMenuModel : ISettingsMenuModel
{

    private ControllerIconsType _lastIconsType;

    private readonly GameSettings _settings;

    public bool HasUnappliedSettings => _settings.ControllerIconsType != _lastIconsType;
    public ControllerIconsType ControllerIconsType { get => _settings.ControllerIconsType; set => _settings.ControllerIconsType = value; }

    public InterfaceSettingsMenuModel(GameSettings settings)
    {
        _settings = settings;
        RememberLastSavedSettings();
    }

    public void SelectIconType(int index)
    {
        ControllerIconsType = (ControllerIconsType)index;
    }

    public void SaveSettings()
    {
        RememberLastSavedSettings();
        _settings.SaveInterfaceSettings();
    }

    public void DiscardChanges()
    {
        ControllerIconsType = _lastIconsType;
    }

    public void ResetToDefault()
    {
        ControllerIconsType = ConfigData.DefaultControllerIconsType;
        SaveSettings();
    }

    private void RememberLastSavedSettings()
    {
        _lastIconsType = ControllerIconsType;
    }

}

