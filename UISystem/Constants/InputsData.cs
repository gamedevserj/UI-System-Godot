namespace UISystem.Core.Constants;

/// <summary>
/// Class containing inputs data.
/// </summary>
#pragma warning disable SA1600 // Elements should be documented
internal static class InputsData
{
    public const string ReturnButton = "ReturnToPreviousMenu";
    public const string PauseButton = "PauseButton";

    public const int KeyboardEventIndex = 0;
    public const int JoystickEventIndex = 1;

    public const string MoveLeft = "MoveLeft";
    public const string MoveRight = "MoveRight";
    public const string Jump = "Jump";

    public static readonly string[] RebindableActions = { MoveLeft, MoveRight, Jump };
}
#pragma warning restore SA1600 // Elements should be documented
