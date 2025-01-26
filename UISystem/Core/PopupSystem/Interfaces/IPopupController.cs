﻿using System;
using UISystem.Core.MenuSystem;
using UISystem.Core.PhysicalInput;

namespace UISystem.Core.PopupSystem;
public partial interface IPopupController<TInputEvent> : IInputReceiver<TInputEvent>
{

    int Type { get; }
    void Init();
    void Hide(int result, bool instant = false);
    void Show(IMenuController<TInputEvent> caller, string message, Action<int> onHideAction, bool instant);

}
