using System;
using System.Collections.Generic;
using UISystem.PopupSystem.Popups.Controllers;

namespace UISystem.PopupSystem.Constants;
public static class PopupViewsPaths
{

    private const string Folder = "res://UISystem/PopupSystem/Prefabs/";

    private const string Yes = Folder + "YesPopupView.tscn";
    private const string YesNo = Folder + "YesNoPopupView.tscn";
    private const string YesNoCancel = Folder + "YesNoCancelPopupView.tscn";

    public static readonly Dictionary<Type, string> Paths = new()
    {
        { typeof(YesPopupController), Yes },
        { typeof(YesNoPopupController), YesNo },
        { typeof(YesNoCancelPopupController), YesNoCancel },
    };

}
