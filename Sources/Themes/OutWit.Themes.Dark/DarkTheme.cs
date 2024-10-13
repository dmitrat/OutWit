using System;
using System.Windows;
using System.Windows.Media;
using OutWit.Themes.Interfaces;

namespace OutWit.Themes.Dark
{
    internal class DarkTheme : ITheme
    {
        public string Key { get; } = "DarkTheme";

        public FontFamily TitleFontFamily { get; } = new FontFamily("Segoe UI Black");
        public double TitleFontSize { get; } = 28;
        public FontWeight TitleFontWeight { get; } = FontWeights.Normal;

        public FontFamily PageTitleFontFamily { get; } = new FontFamily("Segoe UI");
        public double PageTitleFontSize { get; } = 28;
        public FontWeight PageTitleFontWeight { get; } = FontWeights.Bold;

        public FontFamily InputFontFamily { get; } = new FontFamily("Segoe UI");
        public double InputFontSize { get; } = 24;

        public FontFamily NavigationHeaderFontFamily { get; } = new FontFamily("Segoe UI");
        public double NavigationHeaderFontSize { get; } = 24;

        public FontFamily NavigationItemFontFamily { get; } = new FontFamily("Segoe UI");
        public double NavigationItemFontSize { get; } = 16;

        public Color PrimaryLight { get; } = Color.FromRgb(45, 45, 48);
        public Color PrimaryLightForeground { get; } = Color.FromArgb(221, 241, 241, 241);

        public Color PrimaryMid { get; } = Color.FromRgb(37, 37, 38);
        public Color PrimaryMidForeground { get; } = Color.FromRgb(241, 241, 241);

        public Color PrimaryDark { get; } = Color.FromRgb(30, 30, 30);
        public Color PrimaryDarkForeground { get; } = Color.FromArgb(221, 241, 241, 241);

        public Color SecondaryLight { get; } = Color.FromRgb(45, 45, 48);
        public Color SecondaryLightForeground { get; } = Color.FromArgb(221, 241, 241, 241);

        public Color SecondaryMid { get; } = Color.FromRgb(0, 229, 255);
        public Color SecondaryMidForeground { get; } = Color.FromArgb(221, 0, 0, 0);

        public Color SecondaryDark { get; } = Color.FromRgb(0, 184, 212);
        public Color SecondaryDarkForeground { get; } = Color.FromArgb(221, 0, 0, 0);

        public Color ValidationError { get; } = Color.FromRgb(244, 67, 54);
        public Color Background { get; } = Color.FromRgb(0, 0, 0);
        public Color Paper { get; } = Color.FromRgb(45, 45, 48);
        public Color CardBackground { get; } = Color.FromRgb(66, 66, 66);
        public Color ToolBarBackground { get; } = Color.FromRgb(33, 33, 33);
        public Color Body { get; } = Color.FromArgb(221, 255, 255, 255);
        public Color BodyLight { get; } = Color.FromArgb(137, 255, 255, 255);
        public Color ColumnHeader { get; } = Color.FromArgb(188, 255, 255, 255);

        public Color CheckBoxOff { get; } = Color.FromArgb(137, 255, 255, 255);
        public Color CheckBoxDisabled { get; } = Color.FromRgb(100, 112, 118);

        public Color TextBoxBorder { get; } = Color.FromArgb(137, 255, 255, 255);

        public Color Divider { get; } = Color.FromArgb(31, 255, 255, 255);
        public Color Selection { get; } = Color.FromRgb(117, 117, 117);

        public Color RowHoverBackground { get; } = Color.FromArgb(31, 255, 255, 255);

        public Color FlatButtonClick { get; } = Color.FromArgb(25, 117, 117, 117);
        public Color FlatButtonRipple { get; } = Color.FromRgb(182, 182, 182);

        public Color ToolTipBackground { get; } = Color.FromRgb(238, 238, 238);
        public Color ChipBackground { get; } = Color.FromArgb(255, 46, 60, 67);

        public Color SnackbarBackground { get; } = Color.FromRgb(205, 205, 205);
        public Color SnackbarMouseOver { get; } = Color.FromRgb(185, 185, 189);
        public Color SnackbarRipple { get; } = Color.FromRgb(73, 73, 73);

        public Color TextFieldBoxBackground { get; } = Color.FromArgb(26, 255, 255, 255);
        public Color TextFieldBoxHoverBackground { get; } = Color.FromArgb(31, 255, 255, 255);
        public Color TextFieldBoxDisabledBackground { get; } = Color.FromArgb(13, 255, 255, 255);
        public Color TextAreaBorder { get; } = Color.FromArgb(188, 255, 255, 255);
        public Color TextAreaInactiveBorder { get; } = Color.FromArgb(26, 255, 255, 255);
    }
}
