using System;
using System.Collections.Generic;
using UISystem.PopupSystem.Popups.Views;

namespace UISystem.PopupSystem.Constants;

/// <summary>
/// Contains paths to popup views.
/// </summary>
internal static class PopupViewsPaths
{
    /// <summary>
    /// Paths to popup views.
    /// </summary>
    public static readonly Dictionary<Type, string> Paths = new()
    {
        { typeof(YesPopupView), Yes },
        { typeof(YesNoPopupView), YesNo },
        { typeof(YesNoCancelPopupView), YesNoCancel },
    };

    private const string Folder = "res://UISystem/PopupSystem/Prefabs/";

    private const string Yes = Folder + "YesPopupView.tscn";
    private const string YesNo = Folder + "YesNoPopupView.tscn";
    private const string YesNoCancel = Folder + "YesNoCancelPopupView.tscn";
}
