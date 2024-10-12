using Godot;

namespace UISystem.Elements.ElementViews;
public partial class RebindableKeyButtonView : ButtonView
{

    [Export] private TextureRect textureRect;

    public TextureRect TextureRect => textureRect;

}
