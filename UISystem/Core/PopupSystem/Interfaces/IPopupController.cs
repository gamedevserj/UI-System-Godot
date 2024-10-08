using Godot;
using System;
using UISystem.Core.MenuSystem.Interfaces;
using UISystem.Core.PopupSystem.Enums;

namespace UISystem.Core.PopupSystem.Interfaces;
public interface IPopupController
{

    int Popup { get; }
    void Init(Node parent);
    void HandleInputPressedWhenActive(InputEvent key);
    void Hide(PopupResult result, bool instant = false);
    void Show(IMenuController caller, string message, Action<PopupResult> onHideAction, bool instant);

}
