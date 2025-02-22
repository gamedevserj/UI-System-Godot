﻿using System.Collections.Generic;

namespace UISystem.PopupSystem.Constants;
public static class PopupViewsPaths
{

    private const string Folder = "res://UISystem/PopupSystem/Prefabs/";

    private const string Yes = Folder + "YesPopupView.tscn";
    private const string YesNo = Folder + "YesNoPopupView.tscn";
    private const string YesNoCancel = Folder + "YesNoCancelPopupView.tscn";

    public static readonly Dictionary<PopupType, string> Paths = new()
    {
        { PopupType.Yes, Yes },
        { PopupType.YesNo, YesNo },
        { PopupType.YesNoCancel, YesNoCancel },
    };

}
