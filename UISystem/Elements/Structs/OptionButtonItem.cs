namespace UISystem.Elements;

/// <summary>
/// Helper struct to create options for dropdowns.
/// </summary>
public readonly struct OptionButtonItem
{
    /// <summary>
    /// Option label.
    /// </summary>
    public readonly string Label;

    /// <summary>
    /// Option id.
    /// </summary>
    public readonly int Id;

    /// <summary>
    /// Initializes a new instance of the <see cref="OptionButtonItem"/> struct.
    /// </summary>
    /// <param name="label">Option label.</param>
    /// <param name="id">Option id.</param>
    public OptionButtonItem(string label, int id)
    {
        Label = label;
        Id = id;
    }
}
