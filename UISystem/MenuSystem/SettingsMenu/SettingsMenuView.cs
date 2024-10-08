﻿using Godot;
using UISystem.Common.ElementViews;
using UISystem.Core.Views;

namespace UISystem.MenuSystem.SettingsMenu;
public abstract partial class SettingsMenuView : BaseInteractableWindow
{

    [Export] protected Control fadeObjectsContainer;
    [Export] private ButtonView resetButton;

    public ButtonView ResetButton => resetButton;

}
