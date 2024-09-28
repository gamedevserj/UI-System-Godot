using Godot;
using UISystem.Common.Interfaces;

namespace UISystem.Common.Elements;
public partial class RebindableKeyButtonView : ButtonView, IFocusableControl
{

    [Export] private TextureRect textureRect;

    public TextureRect TextureRect => textureRect;

}
