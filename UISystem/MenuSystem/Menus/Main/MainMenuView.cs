using System.Threading.Tasks;
using Godot;
using UISystem.Core.Transitions;
using UISystem.Elements;
using UISystem.Elements.ElementViews;
using UISystem.Extensions;
using UISystem.Transitions;

namespace UISystem.MenuSystem.Views;
public partial class MainMenuView : MenuView
{

    [Export] private ButtonView playButton;
    [Export] private ButtonView optionsButton;
    [Export] private ButtonView quitButton;
    [Export] private Control fadeObjectsContainer;

    [Export] private ButtonView testButton;
    [Export] private Control test;
    [Export] private bool parallel;

    public ButtonView PlayButton => playButton;
    public ButtonView OptionsButton => optionsButton;
    public ButtonView QuitButton => quitButton;
    public Control FadeObjectsContainer => fadeObjectsContainer;

    protected override IFocusableUiElement DefaultSelectedElement => PlayButton;

    protected override IViewTransition CreateTransition()
    {
        return new MainElementDropTransition(this, FadeObjectsContainer, PlayButton, new[] { OptionsButton, QuitButton });
    }

    protected override void PopulateFocusableElements()
    {
        FocusableElements = new IFocusableUiElement[] { PlayButton, OptionsButton, QuitButton };
    }

    public override void _EnterTree()
    {
        testButton.ButtonDown += Tween;
    }

    private void Tween()
    {
        var tween = GetTree().CreateTween();
        tween.TweenModulate(test, Colors.Green, 2);
        if (parallel)
            tween.Parallel();

        tween.TweenControlSize(test, Vector2.Zero, 2);
    }
}
