using Godot;
using System;
using UISystem.Core.Constants;
using UISystem.Core.Elements.Interfaces;
using UISystem.Core.Extensions;
using UISystem.Core.PhysicalInput;
using UISystem.Core.MenuSystem.Interfaces;
using UISystem.Core.PopupSystem.Interfaces;
using UISystem.Core.PopupSystem.Views;
using UISystem.Core.Transitions.Interfaces;

namespace UISystem.Core.PopupSystem.Controllers;
internal abstract class PopupController<TView, TInputEvent> : IPopupController<TInputEvent> where TView : PopupView
{

    protected const float fadeDuration = 0.25f;

    protected TView _view;
    protected IFocusableControl _defaultSelectedElement;
    protected Action<int> _onHideAction;
    private IMenuController<TInputEvent> _caller;

    protected readonly string _prefab;
    protected readonly IPopupsManager<TInputEvent> _popupsManager;
    protected readonly SceneTree _sceneTree;
    protected readonly Node _parent;
    protected readonly IInputProcessor<TInputEvent> _inputProcessor;

    public abstract int Type { get; }
    public abstract int PressedReturnPopupResult { get; }
    public bool CanProcessInput { get; private set; } = true; // to prevent input processing during transitions

    public PopupController(string prefab, IPopupsManager<TInputEvent> popupsManager, Node parent, IInputProcessor<TInputEvent> inputProcessor, SceneTree sceneTree)
    {
        _prefab = prefab;
        _popupsManager = popupsManager;
        _sceneTree = sceneTree;
        _parent = parent;
        _inputProcessor = inputProcessor;
    }

    public virtual void Init()
    {
        if (!_view.IsValid())
        {
            CreateView(_parent);
        }
        _defaultSelectedElement = _view.DefaultSelectedElement;
    }

    public void ProcessInput(TInputEvent key)
    {
        if (!CanProcessInput) return;

        //if (_inputProcessor.IsPressingCancel(key))
        //    _popupsManager.HidePopup(PressedReturnPopupResult);
    }

    public void Show(IMenuController<TInputEvent> caller, string message, Action<int> onHideAction, bool instant = false)
    {
        CanProcessInput = false;
        _caller = caller;
        _caller.CanReturnToPreviousMenu = false;
        _view.Message.Text = message;
        _onHideAction = onHideAction;
        _view.Show((Action)(() =>
        {
            this.CanProcessInput = true;
            if (_defaultSelectedElement?.IsValidElement() == true)
            {
                _defaultSelectedElement.SwitchFocus(true);
            }
        }), instant);
    }

    public void Hide(int result, bool instant = false)
    {
        CanProcessInput = false;
        _view.Hide((Action)(() =>
        {
            this.CanProcessInput = true;
            _caller.CanReturnToPreviousMenu = true;
            _onHideAction?.Invoke(result);
            DestroyView();
        }), instant);
    }

    private void CreateView(Node parent)
    {
        PackedScene obj = ResourceLoader.Load<PackedScene>(_prefab);
        _view = obj.Instantiate() as TView;
        _view.Init(CreateTransition());
        SetupElements();
        parent.AddChild(_view);
    }

    private void DestroyView()
    {
        _view.SafeQueueFree();
    }

    protected abstract void SetupElements();
    protected abstract IViewTransition CreateTransition();

}
