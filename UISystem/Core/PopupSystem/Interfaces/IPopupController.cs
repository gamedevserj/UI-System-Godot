using Godot;
using System;
using UISystem.Core.MenuSystem.Interfaces;

namespace UISystem.Core.PopupSystem.Interfaces;
public partial interface IPopupController
{

    int Type { get; }
    bool CanProcessInput { get; }
    void Init();
    void ProcessInput(InputEvent key);
    void Hide(int result, bool instant = false);
    void Show(IMenuController caller, string message, Action<int> onHideAction, bool instant);

}
