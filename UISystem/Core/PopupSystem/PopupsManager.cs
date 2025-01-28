using System;
using UISystem.Core.PhysicalInput;

namespace UISystem.Core.PopupSystem;
public partial class PopupsManager<TInputEvent, TType, TResult> : Manager<IPopupController<TInputEvent, TType, TResult>, TInputEvent, TType>, 
    IPopupsManager<TInputEvent, TType, TResult>
    where TType : Enum
    where TResult : Enum
{

    public static Action<IInputReceiver<TInputEvent>> OnControllerSwitch;

    public void ShowPopup(TType popupType, string message, Action<TResult> onHideAction = null, bool instant = false)
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

    public void HidePopup(TResult result)
    {
        _currentController?.Hide(result);
        _currentController = null;
    }

}
