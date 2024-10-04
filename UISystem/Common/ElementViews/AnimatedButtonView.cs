using Godot;

namespace UISystem.Common.ElementViews;
public partial class AnimatedButtonView : Control
{

    [Export] private Control hoverResizeControl;
    [Export] private Control border;
    [Export] private Control inside; // to control color
    [Export] private Label label;

    public Control ResizableControl => this;
    public Control HoverResizeControl => hoverResizeControl;
    public Control Border => border;
    public Control Inside => inside;
    public Label Label => label;

}
