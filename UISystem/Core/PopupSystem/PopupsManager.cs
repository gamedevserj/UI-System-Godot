using System;
using UISystem.Core.PhysicalInput;

namespace UISystem.Core.PopupSystem;
public partial class PopupsManager<TInputEvent> : Manager<IPopupController<TInputEvent>, TInputEvent>,  IPopupsManager<TInputEvent>
{

    public static Action<IInputReceiver<TInputEvent>> OnControllerSwitch;

    public void ShowPopup(int popupType, string message, Action<int> onHideAction = null, bool instant = false)
    {
        _currentController = _controllers[popupType];
        _currentController.Init();
        _currentController.Show(message, (result)=> 
        {
            OnControllerSwitch?.Invoke(null);
            onHideAction?.Invoke(result); 
        }, instant);
        OnControllerSwitch?.Invoke(_currentController);
    }

    public void HidePopup(int result)
    {
        _currentController?.Hide(result);
        _currentController = null;
    }

}
