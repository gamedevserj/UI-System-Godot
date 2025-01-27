using Godot;
using UISystem.Core.Transitions;
using UISystem.Core.Views;
using UISystem.MenuSystem.Views;
using UISystem.Transitions;
using UISystem.Transitions.Interfaces;

namespace UISystem.MenuSystem.ViewHandlers;
internal class AudioSettingsMenuViewHandler<TView> : MenuViewModel<AudioSettingsMenuView>, IViewModel<AudioSettingsMenuView>
{
    public AudioSettingsMenuViewHandler(string prefab, Node parent) : base(prefab, parent)
    { }

    
}
