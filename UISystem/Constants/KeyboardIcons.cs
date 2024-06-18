using Godot;
using System.Collections.Generic;

namespace UISystem.Common.Constants;
public static class KeyboardIcons
{
    private static string ItemsFolder => "res://UISystem/Textures/Inputs/Keyboard/";
    private static readonly Dictionary<Key, string> _keys;

    static KeyboardIcons()
    {
        _keys = new Dictionary<Key, string>()
        {
            { Key.None, "keyboard_escape.png" },
            { Key.Escape, "keyboard_escape.png" },
            { Key.Tab, "keyboard_tab_icon.png" },
            { Key.Backspace, "keyboard_backspace.png" },
            { Key.Enter, "keyboard_enter.png" },
            { Key.KpEnter, "keyboard_numpad_enter.png" },
            { Key.Insert, "keyboard_insert.png" },
            { Key.Delete, "keyboard_delete.png" },
            { Key.Print, "keyboard_printscreen.png" },
            { Key.Home, "keyboard_home.png" },
            { Key.End, "keyboard_end.png" },
            { Key.Left, "keyboard_arrow_left.png" },
            { Key.Up, "keyboard_arrow_up.png" },
            { Key.Right, "keyboard_arrow_right.png" },
            { Key.Down, "keyboard_arrow_down.png" },
            { Key.Pageup, "keyboard_page_up.png" },
            { Key.Pagedown, "keyboard_page_down.png" },
            { Key.Shift, "keyboard_shift.png" },
            { Key.Ctrl, "keyboard_ctrl.png" },
            { Key.Alt, "keyboard_alt.png" },
            { Key.Capslock, "keyboard_capslock.png" },
            { Key.Numlock, "keyboard_numlock.png" },
            { Key.F1, "keyboard_f1.png" },
            { Key.F2, "keyboard_f2.png" },
            { Key.F3, "keyboard_f3.png" },
            { Key.F4, "keyboard_f4.png" },
            { Key.F5, "keyboard_f5.png" },
            { Key.F6, "keyboard_f6.png" },
            { Key.F7, "keyboard_f7.png" },
            { Key.F8, "keyboard_f8.png" },
            { Key.F9, "keyboard_f9.png" },
            { Key.F10, "keyboard_f10.png" },
            { Key.F11, "keyboard_f11.png"  },
            { Key.F12, "keyboard_f12.png"},
            { Key.KpMultiply, "keyboard_asterisk.png" },
            { Key.KpDivide, "keyboard_slash_forward.png" },
            { Key.KpSubtract, "keyboard_minus.png" },
            { Key.KpPeriod, "keyboard_period.png" },
            { Key.KpAdd, "keyboard_plus.png" },
            { Key.Kp0, "keyboard_0.png" },
            { Key.Kp1, "keyboard_1.png" },
            { Key.Kp2, "keyboard_2.png" },
            { Key.Kp3, "keyboard_3.png" },
            { Key.Kp4, "keyboard_4.png" },
            { Key.Kp5, "keyboard_5.png" },
            { Key.Kp6, "keyboard_6.png" },
            { Key.Kp7, "keyboard_7.png" },
            { Key.Kp8, "keyboard_8.png" },
            { Key.Kp9, "keyboard_9.png" },
            { Key.Space, "keyboard_space.png" },
            { Key.Exclam, "keyboard_exclamation.png" },
            { Key.Quotedbl, "keyboard_quote.png" },
            { Key.Asterisk, "keyboard_asterisk.png" },
            { Key.Plus, "keyboard_plus.png" },
            { Key.Comma, "keyboard_comma.png" },
            { Key.Minus, "keyboard_minus.png" },
            { Key.Period, "keyboard_period.png" },
            { Key.Slash, "keyboard_slash_forward.png" },
            { Key.Key0, "keyboard_0.png" },
            { Key.Key1, "keyboard_1.png" },
            { Key.Key2, "keyboard_2.png" },
            { Key.Key3, "keyboard_3.png" },
            { Key.Key4, "keyboard_4.png" },
            { Key.Key5, "keyboard_5.png" },
            { Key.Key6, "keyboard_6.png" },
            { Key.Key7, "keyboard_7.png" },
            { Key.Key8, "keyboard_8.png" },
            { Key.Key9, "keyboard_9.png" },
            { Key.Colon, "keyboard_colon.png" },
            { Key.Semicolon, "keyboard_semicolon.png" },
            { Key.Less, "keyboard_bracket_less.png" },
            { Key.Equal, "keyboard_equals.png" },
            { Key.Greater, "keyboard_bracket_greater.png" },
            { Key.Question, "keyboard_question.png" },
            { Key.A, "keyboard_a.png" },
            { Key.B, "keyboard_b.png" },
            { Key.C, "keyboard_c.png" },
            { Key.D, "keyboard_d.png" },
            { Key.E, "keyboard_e.png" },
            { Key.F, "keyboard_f.png" },
            { Key.G, "keyboard_g.png" },
            { Key.H, "keyboard_h.png" },
            { Key.I, "keyboard_i.png" },
            { Key.J, "keyboard_j.png" },
            { Key.K, "keyboard_k.png" },
            { Key.L, "keyboard_l.png" },
            { Key.M, "keyboard_m.png" },
            { Key.N, "keyboard_n.png" },
            { Key.O, "keyboard_o.png" },
            { Key.P, "keyboard_p.png" },
            { Key.Q, "keyboard_q.png" },
            { Key.R, "keyboard_r.png" },
            { Key.S, "keyboard_s.png" },
            { Key.T, "keyboard_t.png" },
            { Key.U, "keyboard_u.png" },
            { Key.V, "keyboard_v.png" },
            { Key.W, "keyboard_w.png" },
            { Key.X, "keyboard_x.png" },
            { Key.Y, "keyboard_y.png" },
            { Key.Z, "keyboard_z.png" },
            { Key.Bracketleft, "keyboard_bracket_open.png" },
            { Key.Backslash, "keyboard_slash_back.png" },
            { Key.Bracketright, "keyboard_bracket_close.png" },
            { Key.Asciitilde, "keyboard_tilde.png" },
        };
    }

    public static string GetIcon(Key key)
    {
        return ItemsFolder + _keys[key];
    }

    /*
     * unassigned
     * case Key.None:
                break;
            case Key.Special:
                break;
            case Key.Backtab:
                break;
            case Key.Pause:
                break;
            case Key.Sysreq:
                break;
            case Key.Clear:
                break;
            case Key.Meta:
                break;
            case Key.Scrolllock:
                break;
            case Key.F13:
                break;
            case Key.F14:
                break;
            case Key.F15:
                break;
            case Key.F16:
                break;
            case Key.F17:
                break;
            case Key.F18:
                break;
            case Key.F19:
                break;
            case Key.F20:
                break;
            case Key.F21:
                break;
            case Key.F22:
                break;
            case Key.F23:
                break;
            case Key.F24:
                break;
            case Key.F25:
                break;
            case Key.F26:
                break;
            case Key.F27:
                break;
            case Key.F28:
                break;
            case Key.F29:
                break;
            case Key.F30:
                break;
            case Key.F31:
                break;
            case Key.F32:
                break;
            case Key.F33:
                break;
            case Key.F34:
                break;
            case Key.F35:
                break;
            case Key.Menu:
                break;
            case Key.Hyper:
                break;
            case Key.Help:
                break;
            case Key.Back:
                break;
            case Key.Forward:
                break;
            case Key.Stop:
                break;
            case Key.Refresh:
                break;
            case Key.Volumedown:
                break;
            case Key.Volumemute:
                break;
            case Key.Volumeup:
                break;
            case Key.Mediaplay:
                break;
            case Key.Mediastop:
                break;
            case Key.Mediaprevious:
                break;
            case Key.Medianext:
                break;
            case Key.Mediarecord:
                break;
            case Key.Homepage:
                break;
            case Key.Favorites:
                break;
            case Key.Search:
                break;
            case Key.Standby:
                break;
            case Key.Openurl:
                break;
            case Key.Launchmail:
                break;
            case Key.Launchmedia:
                break;
            case Key.Launch0:
                break;
            case Key.Launch1:
                break;
            case Key.Launch2:
                break;
            case Key.Launch3:
                break;
            case Key.Launch4:
                break;
            case Key.Launch5:
                break;
            case Key.Launch6:
                break;
            case Key.Launch7:
                break;
            case Key.Launch8:
                break;
            case Key.Launch9:
                break;
            case Key.Launcha:
                break;
            case Key.Launchb:
                break;
            case Key.Launchc:
                break;
            case Key.Launchd:
                break;
            case Key.Launche:
                break;
            case Key.Launchf:
                break;
            case Key.Globe:
                break;
            case Key.Keyboard:
                break;
            case Key.JisEisu:
                break;
            case Key.JisKana:
                break;
            case Key.Unknown:
                break;
            case Key.Numbersign:
                break;
            case Key.Dollar:
                break;
            case Key.Percent:
                break;
            case Key.Ampersand:
                break;
            case Key.Apostrophe:
                break;
            case Key.Parenleft:
                break;
            case Key.Parenright:
                break;
            case Key.At:
                break;
            case Key.Asciicircum:
                break;
            case Key.Underscore:
                break;
            case Key.Quoteleft:
                break;
            case Key.Braceleft:
                break;
            case Key.Bar:
                break;
            case Key.Braceright:
                break;
            case Key.Yen:
                break;
            case Key.Section:
                break;
     * */


}
