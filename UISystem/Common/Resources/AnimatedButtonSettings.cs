using Godot;
using UISystem.Common.Enums;

namespace UISystem.Common.Resources;
[GlobalClass]
public partial class AnimatedButtonSettings : Resource
{

    [Export] private float duration = 1;
    [Export] private Tween.EaseType ease = Tween.EaseType.Out;
    [Export] private Tween.TransitionType transition = Tween.TransitionType.Elastic;
    [Export] private HorizontalControlSizeChangeDirection horizontalDirection = HorizontalControlSizeChangeDirection.FromLeft;
    [Export] private VerticalControlSizeChangeDirection verticalDirection = VerticalControlSizeChangeDirection.FromTop;
    [Export] private Vector2 changeSizeHover = new(75, 0);
    [Export] private Vector2 changeSizeFocus = new(150, 0);

    public float Duration => duration;
    public Tween.EaseType Ease => ease;
    public Tween.TransitionType Transition => transition;
    public HorizontalControlSizeChangeDirection HorizontalDirection => horizontalDirection;
    public VerticalControlSizeChangeDirection VerticalDirection => verticalDirection;
    public Vector2 ChangeSizeHover => changeSizeHover;
    public Vector2 ChangeSizeFocus => changeSizeFocus;

}
