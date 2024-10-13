using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace OutWit.Themes.Interfaces
{
    public interface ITheme
    {
        string Key { get; }

        FontFamily TitleFontFamily { get; }
        double TitleFontSize { get; }
        FontWeight TitleFontWeight { get; }

        FontFamily PageTitleFontFamily { get; }
        double PageTitleFontSize { get; }
        FontWeight PageTitleFontWeight { get; }

        FontFamily InputFontFamily { get; }
        double InputFontSize { get; }

        FontFamily NavigationHeaderFontFamily { get; }
        double NavigationHeaderFontSize { get; }

        FontFamily NavigationItemFontFamily { get; }
        double NavigationItemFontSize { get; }

        Color PrimaryLight { get; }
        Color PrimaryLightForeground { get; }

        Color PrimaryMid { get; }
        Color PrimaryMidForeground{ get; }

        Color PrimaryDark{ get; }
        Color PrimaryDarkForeground { get; }

        Color SecondaryLight{ get; }
        Color SecondaryLightForeground { get; }

        Color SecondaryMid{ get; }
        Color SecondaryMidForeground { get; }

        Color SecondaryDark { get; }
        Color SecondaryDarkForeground { get; }

        Color ValidationError { get; }
        Color Background { get; }
        Color Paper { get; }
        Color CardBackground { get; }
        Color ToolBarBackground { get; }
        Color Body { get; }
        Color BodyLight { get; }
        Color ColumnHeader { get; }

        Color CheckBoxOff { get; }
        Color CheckBoxDisabled { get; }

        Color Divider { get; }
        Color Selection { get; }

        Color RowHoverBackground { get; }

        Color FlatButtonClick { get; }
        Color FlatButtonRipple { get; }

        Color ToolTipBackground { get; }
        Color ChipBackground { get; }

        Color SnackbarBackground { get; }
        Color SnackbarMouseOver { get; }
        Color SnackbarRipple { get; }

        Color TextBoxBorder { get; }

        Color TextFieldBoxBackground { get; }
        Color TextFieldBoxHoverBackground { get; }
        Color TextFieldBoxDisabledBackground { get; }
        Color TextAreaBorder { get; }
        Color TextAreaInactiveBorder { get; }
    }
}
