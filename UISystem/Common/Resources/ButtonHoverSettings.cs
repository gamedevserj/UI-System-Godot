using Godot;
namespace UISystem.Common.Resources;
[GlobalClass]
public partial class ButtonHoverSettings : Resource
{

    [Export] private SizeTweenSettings sizeChangeSettings;

    public SizeTweenSettings SizeChangeSettings => sizeChangeSettings;

}
