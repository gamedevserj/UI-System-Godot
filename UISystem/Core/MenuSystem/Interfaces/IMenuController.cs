using Godot;
using System;
using UISystem.Core.MenuSystem.Enums;

namespace UISystem.Core.MenuSystem.Interfaces;
public partial interface IMenuController
{

    int Type { get; }
    bool CanReturnToPreviousMenu { get; set; }
    bool CanProcessInput { get; }

    void Init();
    void Hide(StackingType stackingType, Action onComplete = null, bool instant = false);
    void Show(Action onComplete = null, bool instant = false);

    void ProcessInput(InputEvent key);
    void DestroyView();

}
