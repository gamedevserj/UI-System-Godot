using Godot;
using UISystem.Core.Common.ElementViews;

namespace UISystem.Common.ElementViews;
public partial class RebindableKeyButtonView : ButtonView
{

    [Export] private TextureRect textureRect;

    public TextureRect TextureRect => textureRect;

}
