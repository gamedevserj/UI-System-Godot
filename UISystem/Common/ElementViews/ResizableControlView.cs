using Godot;
using System.Threading.Tasks;
using UISystem.Common.Interfaces;

namespace UISystem.UISystem.Common.ElementViews;
/// <summary>
/// Is used to pass controls containing labels to PanelSizeTransition 
/// </summary>
public partial class ResizableControlView : Control, ITweenableMenuElement
{

    public Control PositionControl => this;
    public Control ResizableControl => this;

    public async Task ResetHover() => await Task.CompletedTask;

}
