using System.Collections.Generic;
using UISystem.PopupSystem.Enums;

namespace UISystem.PopupSystem.Constants;
public static class PopupViewsPaths
{

    private const string Folder = "res://UISystem/PopupSystem/Prefabs/";

    private const string Info = Folder + "InfoPopupView.tscn";
    private const string YesNo = Folder + "YesNoPopupView.tscn";
    private const string YesNoCancel = Folder + "YesNoCancelPopupView.tscn";

    public static readonly Dictionary<PopupType, string> Paths = new()
    {
        { PopupType.Yes, Info },
        { PopupType.YesNo, YesNo },
        { PopupType.YesNoCancel, YesNoCancel },
    };

}
