using Godot;
using UISystem.Common.Interfaces;

namespace UISystem.Common.Elements;
public partial class RebindableKeyButtonView : ButtonView, IFocusableControl
{

    [Export] private TextureRect image;

    public TextureRect Image => image;

}
