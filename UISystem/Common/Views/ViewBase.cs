using System.Threading.Tasks;
using Godot;
using UISystem.Core.Elements;
using UISystem.Core.Transitions;
using UISystem.Core.Views;
using UISystem.Extensions;

namespace UISystem.Views;

/// <summary>
/// Base class for a window with interactable elements (menu, popup, etc.)
/// </summary>
public abstract partial class ViewBase : Control, IView
{
    private IViewTransition _transition;

    /// <summary>
    /// Gets or sets interactable elements.
    /// </summary>
    protected IInteractableElement[] FocusableElements { get; set; }

    /// <summary>
    /// Gets the element that is selected by default when menu is shown for the first time.
    /// </summary>
    protected abstract IInteractableElement DefaultSelectedElement { get; }

    /// <inheritdoc/>
    public virtual void Init()
    {
        _transition = CreateTransition();
        PopulateFocusableElements();
    }

    /// <inheritdoc/>
    public void SwitchInteractability(bool enable)
    {
        if (FocusableElements != null)
        {
            for (int i = 0; i < FocusableElements.Length; i++)
            {
                FocusableElements[i].SwitchFocusAvailability(enable);
            }
        }
    }

    /// <inheritdoc/>
    public async Task Show(bool instant = false)
    {
        SwitchInteractability(false);
        Visible = true;
        await _transition.Show(instant);
        SwitchInteractability(true);
    }

    /// <inheritdoc/>
    public async Task Hide(bool instant = false)
    {
        SwitchInteractability(false);
        await _transition.Hide(instant);
        Visible = false; // need to switch off visibility to allow GuiPanel3D to receive mouse events
    }

    /// <inheritdoc/>
    public void DestroyView() => this.SafeQueueFree();

    /// <inheritdoc/>
    public abstract void FocusElement();

    /// <summary>
    /// Sets interactable elements.
    /// </summary>
    protected abstract void PopulateFocusableElements();

    /// <summary>
    /// Creates transition.
    /// </summary>
    /// <returns>Instance of transition.</returns>
    protected abstract IViewTransition CreateTransition();
}
