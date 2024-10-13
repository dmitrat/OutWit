using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;
using OutWit.Themes.Interfaces;

namespace OutWit.Themes.Light
{
    internal class LightTheme : ITheme
    {
        public string Key { get; } = "LightTheme";

        public FontFamily TitleFontFamily { get; } = new FontFamily("Segoe UI");
        public double TitleFontSize { get; } = 24;
        public FontWeight TitleFontWeight { get; } = FontWeights.Bold;

        public FontFamily PageTitleFontFamily { get; } = new FontFamily("Segoe UI");
        public double PageTitleFontSize { get; } = 28;
        public FontWeight PageTitleFontWeight { get; } = FontWeights.Bold;

        public FontFamily InputFontFamily { get; } = new FontFamily("Segoe UI");
        public double InputFontSize { get; } = 22;

        public FontFamily NavigationHeaderFontFamily { get; } = new FontFamily("Segoe UI");
        public double NavigationHeaderFontSize { get; } = 20;

        public FontFamily NavigationItemFontFamily { get; } = new FontFamily("Segoe UI");
        public double NavigationItemFontSize { get; } = 14;

        public Color PrimaryLight { get; } = Color.FromRgb(192, 211, 239);
        public Color PrimaryLightForeground { get; } = Color.FromArgb(221,0, 0, 0);

        public Color PrimaryMid { get; } = Color.FromRgb(29, 78, 137);
        public Color PrimaryMidForeground { get; } = Color.FromRgb(255, 255, 255);

        public Color PrimaryDark { get; } = Color.FromRgb(29, 78, 137);
        public Color PrimaryDarkForeground { get; } = Color.FromArgb(221,255, 255, 255);

        public Color SecondaryLight { get; } = Color.FromRgb(224, 233, 247);
        public Color SecondaryLightForeground { get; } = Color.FromArgb(221, 0, 0, 0);

        public Color SecondaryMid{ get; } = Color.FromRgb(0, 229, 255);
        public Color SecondaryMidForeground { get; } = Color.FromArgb(221, 0, 0, 0);

        public Color SecondaryDark { get; } = Color.FromRgb(0, 184, 212);
        public Color SecondaryDarkForeground { get; } = Color.FromArgb(221, 0, 0, 0);

        public Color ValidationError { get; } = Color.FromRgb(244, 67, 54);
        public Color Background { get; } = Color.FromRgb(255, 255, 255);
        public Color Paper { get; } = Color.FromRgb(241, 242, 235);
        public Color CardBackground { get; } = Color.FromRgb(255, 255, 255);
        public Color ToolBarBackground { get; } = Color.FromRgb(245, 245, 245);
        public Color Body { get; } = Color.FromRgb(0, 0, 0);
        public Color BodyLight { get; } = Color.FromArgb(137, 0, 0, 0);
        public Color ColumnHeader { get; } = Color.FromArgb(188, 0, 0, 0);

        public Color CheckBoxOff { get; } = Color.FromArgb(137, 0, 0, 0);
        public Color CheckBoxDisabled { get; } = Color.FromRgb(189, 189, 189);

        public Color TextBoxBorder { get; } = Color.FromArgb(137, 0, 0, 0);
        //public Color TextBoxBorder { get; } = Color.FromArgb(137, 255, 0, 0);

        //public Color Divider { get; } = Color.FromArgb(85, 29, 78, 137);
        public Color Divider { get; } = Color.FromArgb(31, 0, 0, 0);
        //public Color Divider { get; } = Color.FromArgb(255, 29, 78, 137);
        public Color Selection { get; } = Color.FromRgb(222, 222, 222);

        public Color RowHoverBackground { get; } = Color.FromArgb(20, 0, 0, 0);

        public Color FlatButtonClick { get; } = Color.FromRgb(222, 222, 222);
        public Color FlatButtonRipple { get; } = Color.FromRgb(182, 182, 182);

        public Color ToolTipBackground { get; } = Color.FromRgb(117, 117, 117);
        public Color ChipBackground { get; } = Color.FromArgb(18, 0, 0, 0);

        public Color SnackbarBackground { get; } = Color.FromRgb(29, 78, 137);
        public Color SnackbarMouseOver { get; } = Color.FromRgb(51, 95, 149);
        public Color SnackbarRipple { get; } = Color.FromRgb(182, 182, 182);

        public Color TextFieldBoxBackground { get; } = Color.FromArgb(15, 0, 0, 0);
        public Color TextFieldBoxHoverBackground { get; } = Color.FromArgb(20, 0, 0, 0);
        public Color TextFieldBoxDisabledBackground { get; } = Color.FromArgb(8, 0, 0, 0);

        public Color TextAreaBorder { get; } = Color.FromArgb(188, 0, 0, 0);
        public Color TextAreaInactiveBorder { get; } = Color.FromArgb(15, 0, 0, 0);
    }
}
