using Godot;
using System.Threading.Tasks;
using UISystem.Core.Transitions;
using UISystem.Core.Views;
using UISystem.Elements;
using UISystem.Extensions;

namespace UISystem.Views;
/// <summary>
/// Base class for a window with interactable elements (menu, popup, etc.)
/// </summary>
public abstract partial class ViewBase : Control, IView
{
    private IViewTransition _transition;
    protected IFocusableControl[] _focusableElements;

    public virtual void Init()
    {
        _transition = CreateTransition();
        PopulateFocusableElements();
    }

    public void SwitchInteractability(bool enable)
    {
        if (_focusableElements != null)
        {
            for (int i = 0; i < _focusableElements.Length; i++)
            {
                _focusableElements[i].SwitchFocusAvailability(enable);
            }
        }
    }

    public async Task Show(bool instant = false)
    {
        SwitchInteractability(false);
        Visible = true;
        await _transition.Show(instant);
        SwitchInteractability(true);
    }

    public async Task Hide(bool instant = false)
    {
        SwitchInteractability(false);
        await _transition.Hide(instant);
        Visible = false; // need to switch off visibility to allow GuiPanel3D to receive mouse events
    }

    public void DestroyView() => this.SafeQueueFree();
    public abstract void FocusElement();
    protected abstract void PopulateFocusableElements();
    protected abstract IViewTransition CreateTransition();
}
