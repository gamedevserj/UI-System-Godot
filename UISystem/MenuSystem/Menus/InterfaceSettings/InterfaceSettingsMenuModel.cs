using UISystem.Common.Enums;
using UISystem.Constants;
using UISystem.Core.MenuSystem;

namespace UISystem.MenuSystem.Models;
public class InterfaceSettingsMenuModel : ISettingsMenuModel
{

    //private ControllerIconsType _tempIconsType;
    private ControllerIconsType _lastIconsType;

    private readonly GameSettings _settings;

    public bool HasUnappliedSettings => _settings.ControllerIconsType != _lastIconsType;
    public ControllerIconsType ControllerIconsType { get => _settings.ControllerIconsType; set => _settings.ControllerIconsType = value; }

    public InterfaceSettingsMenuModel(GameSettings settings)
    {
        _settings = settings;
        RememberLastSavedSettings();
        //LoadSettings();
    }

    public void SelectIconType(int index)
    {
        //_tempIconsType = (ControllerIconsType)index;
        ControllerIconsType = (ControllerIconsType)index;
    }

    public void SaveSettings()
    {
        //_settings.SetControllerIconsType(_tempIconsType);
        RememberLastSavedSettings();
        _settings.SaveInterfaceSettings();
    }

    public void DiscardChanges()
    {
        //LoadSettings();
        ControllerIconsType = _lastIconsType;
    }

    public void ResetToDefault()
    {
        ControllerIconsType = ConfigData.DefaultControllerIconsType;
        //_settings.SetControllerIconsType(_tempIconsType);
        SaveSettings();
    }

    //private void LoadSettings()
    //{
    //    _tempIconsType = _settings.ControllerIconsType;
    //}

    private void RememberLastSavedSettings()
    {
        _lastIconsType = ControllerIconsType;
    }

}

