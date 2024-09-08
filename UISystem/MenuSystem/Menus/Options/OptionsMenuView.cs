using Godot;
using System;
using UISystem.Common.Elements;
using UISystem.Common.Helpers;
using UISystem.Common.Interfaces;

namespace UISystem.MenuSystem.Views;
public partial class OptionsMenuView : MenuView
{

    [Export] private ButtonView interfaceSettingsButton;
    [Export] private ButtonView audioSettingsButton;
    [Export] private ButtonView videoSettingsButton;
    [Export] private ButtonView rebindKeysButton;
    [Export] private ButtonView returnButton;
    [Export] private Control fadeObjectsContainer;

    public ButtonView ReturnButton => returnButton;
    public ButtonView InterfaceSettingsButton => interfaceSettingsButton;
    public ButtonView AudioSettingsButton => audioSettingsButton;
    public ButtonView VideoSettingsButton => videoSettingsButton;
    public ButtonView RebindKeysButton => rebindKeysButton;

    protected override void PopulateFocusableElements()
    {
        _focusableElements = new IFocusableControl[] { ReturnButton, AudioSettingsButton, VideoSettingsButton, 
            RebindKeysButton, InterfaceSettingsButton };
    }

    public override void Init()
    {
        base.Init();
        Fader.Init(fadeObjectsContainer);
    }

    public override void Hide(Action onHidden, bool instant)
    {
        Fader.Hide(GetTree(), fadeObjectsContainer, onHidden, instant);
    }

    public override void Show(Action onShown, bool instant)
    {
        Fader.Show(GetTree(), fadeObjectsContainer, onShown, instant);
    }

}
