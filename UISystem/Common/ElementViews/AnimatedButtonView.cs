using Godot;

namespace UISystem.Common.ElementViews;
public partial class AnimatedButtonView : Control
{

    [Export] private Control border;

    public Control ResizeableControl => this;
    public Control Border => border;

}
