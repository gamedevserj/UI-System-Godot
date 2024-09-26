using Godot;

namespace UISystem.Common.ElementViews;
public partial class AnimatedButtonView : Control
{

    [Export] private Control border;

    public Control ResizableControl => this;
    public Control Border => border;

}
