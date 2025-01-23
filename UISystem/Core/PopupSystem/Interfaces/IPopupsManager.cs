using Godot;
using System;
using UISystem.Core.MenuSystem.Interfaces;

namespace UISystem.Core.PopupSystem.Interfaces;
public partial interface IPopupsManager
{

    void Init(IPopupController[] controllers);
    void ProcessInput(InputEvent @event);
    void ShowPopup(int popupType, IMenuController caller, string message, Action<int> onHideAction = null, bool instant = false);
    void HidePopup(int result);

}
