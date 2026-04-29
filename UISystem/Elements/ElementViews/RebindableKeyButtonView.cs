using Godot;

namespace UISystem.Elements.ElementViews;

/// <summary>
/// Base class for rebindable key button view.
/// </summary>
public partial class RebindableKeyButtonView : ButtonView
{
    [Export] private TextureRect textureRect;

    /// <summary>
    /// Gets testure rect.
    /// </summary>
    public TextureRect TextureRect => textureRect;
}
