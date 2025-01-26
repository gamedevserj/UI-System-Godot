using System;
using System.Collections.Generic;
using UISystem.Core.PhysicalInput;

namespace UISystem.Core.PopupSystem;
public partial class PopupsManager<TInputEvent> : IPopupsManager<TInputEvent>
{

    public static Action<IInputReceiver<TInputEvent>> OnControllerSwitch;

    private IPopupController<TInputEvent> _currentController;
    private Dictionary<int, IPopupController<TInputEvent>> _controllers = new();

    public void Init(IPopupController<TInputEvent>[] controllers)
    {
        for (int i = 0; i < controllers.Length; i++)
        {
            _controllers.Add(controllers[i].Type, controllers[i]);
        }
    }

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
