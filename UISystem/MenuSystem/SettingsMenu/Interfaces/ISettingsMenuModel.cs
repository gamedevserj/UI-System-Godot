using UISystem.Core.MenuSystem.Interfaces;

namespace UISystem.MenuSystem.SettingsMenu.Interfaces;
public interface ISettingsMenuModel : IMenuModel
{

    bool HasUnappliedSettings { get; }

    void SaveSettings();
    void DiscardChanges();

    void ResetToDefault();

}
