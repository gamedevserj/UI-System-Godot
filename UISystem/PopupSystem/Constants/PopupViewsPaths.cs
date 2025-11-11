using System;
using System.Collections.Generic;
using UISystem.PopupSystem.Popups.Views;

namespace UISystem.PopupSystem.Constants;
public static class PopupViewsPaths
{

    private const string Folder = "res://UISystem/PopupSystem/Prefabs/";

    private const string Yes = Folder + "YesPopupView.tscn";
    private const string YesNo = Folder + "YesNoPopupView.tscn";
    private const string YesNoCancel = Folder + "YesNoCancelPopupView.tscn";

    public static readonly Dictionary<Type, string> Paths = new()
    {
        { typeof(YesPopupView), Yes },
        { typeof(YesNoPopupView), YesNo },
        { typeof(YesNoCancelPopupView), YesNoCancel },
    };

}
