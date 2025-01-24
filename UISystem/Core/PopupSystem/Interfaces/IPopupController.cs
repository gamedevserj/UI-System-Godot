﻿using System;
using UISystem.Core.MenuSystem.Interfaces;

namespace UISystem.Core.PopupSystem.Interfaces;
public partial interface IPopupController<TInputEvent>
{

    int Type { get; }
    bool CanProcessInput { get; }
    void Init();
    void ProcessInput(TInputEvent key);
    void Hide(int result, bool instant = false);
    void Show(IMenuController<TInputEvent> caller, string message, Action<int> onHideAction, bool instant);

}
