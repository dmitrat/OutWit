using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using OutWit.Common.Exceptions;
using OutWit.Themes.Interfaces;

namespace OutWit.Themes.Utils
{
    public static class Extensions
    {
        public static void SetTheme(this Application me, ITheme theme)
        {
            if(me == null)
                throw new ArgumentNullException(nameof(me), "Application is null");

            if (theme == null)
                throw new ArgumentNullException(nameof(theme), "Theme is null");

            me.Resources.SetTheme(theme);
        }

        public static void SetTheme(this ResourceDictionary me, ITheme theme)
        {
            if (me == null)
                throw new ArgumentNullException(nameof(me), "ResourceDictionary is null");

            if (theme == null)
                throw new ArgumentNullException(nameof(theme), "Theme is null");

            me.SetFontFamily("TitleFontFamily", theme.TitleFontFamily);
            me.SetDouble("TitleFontSize", theme.TitleFontSize);
            me.SetFontWeight("TitleFontWeight", theme.TitleFontWeight);

            me.SetFontFamily("PageTitleFontFamily", theme.PageTitleFontFamily);
            me.SetDouble("PageTitleFontSize", theme.PageTitleFontSize);
            me.SetFontWeight("PageTitleFontWeight", theme.PageTitleFontWeight);

            me.SetFontFamily("NavigationHeaderFontFamily", theme.NavigationHeaderFontFamily);
            me.SetDouble("NavigationHeaderFontSize", theme.NavigationHeaderFontSize);

            me.SetFontFamily("InputFontFamily", theme.InputFontFamily);
            me.SetDouble("InputFontSize", theme.InputFontSize);

            me.SetFontFamily("NavigationItemFontFamily", theme.NavigationItemFontFamily);
            me.SetDouble("NavigationItemFontSize", theme.NavigationItemFontSize);

            me.SetSolidColorBrush("PrimaryHueLightBrush", theme.PrimaryLight);
            me.SetSolidColorBrush("PrimaryHueLightForegroundBrush", theme.PrimaryLightForeground);
            me.SetSolidColorBrush("PrimaryHueMidBrush", theme.PrimaryMid);
            me.SetSolidColorBrush("PrimaryHueMidForegroundBrush", theme.PrimaryMidForeground);
            me.SetSolidColorBrush("PrimaryHueDarkBrush", theme.PrimaryDark);
            me.SetSolidColorBrush("PrimaryHueDarkForegroundBrush", theme.PrimaryDarkForeground);

            me.SetSolidColorBrush("SecondaryHueLightBrush", theme.SecondaryLight);
            me.SetSolidColorBrush("SecondaryHueLightForegroundBrush", theme.SecondaryLightForeground);
            me.SetSolidColorBrush("SecondaryHueMidBrush", theme.SecondaryMid);
            me.SetSolidColorBrush("SecondaryHueMidForegroundBrush", theme.SecondaryMidForeground);
            me.SetSolidColorBrush("SecondaryHueDarkBrush", theme.SecondaryDark);
            me.SetSolidColorBrush("SecondaryHueDarkForegroundBrush", theme.SecondaryDarkForeground);

            me.SetSolidColorBrush("SecondaryAccentBrush", theme.SecondaryMid);
            me.SetSolidColorBrush("SecondaryAccentForegroundBrush", theme.SecondaryMidForeground);

            me.SetSolidColorBrush("ValidationErrorBrush", theme.ValidationError);
            me.SetSolidColorBrush("MaterialDesignBackground", theme.Background);
            me.SetSolidColorBrush("MaterialDesignPaper", theme.Paper);
            me.SetSolidColorBrush("MaterialDesignCardBackground", theme.CardBackground);
            me.SetSolidColorBrush("MaterialDesignToolBarBackground", theme.ToolBarBackground);
            me.SetSolidColorBrush("MaterialDesignBody", theme.Body);
            me.SetSolidColorBrush("MaterialDesignBodyLight", theme.BodyLight);
            me.SetSolidColorBrush("MaterialDesignColumnHeader", theme.ColumnHeader);

            me.SetSolidColorBrush("MaterialDesignCheckBoxOff", theme.CheckBoxOff);
            me.SetSolidColorBrush("MaterialDesignCheckBoxDisabled", theme.CheckBoxDisabled);

            me.SetSolidColorBrush("MaterialDesignTextBoxBorder", theme.TextBoxBorder);

            me.SetSolidColorBrush("MaterialDesignDivider", theme.Divider);
            me.SetSolidColorBrush("MaterialDesignSelection", theme.Selection);

            me.SetSolidColorBrush("MaterialDesignDataGridRowHoverBackground", theme.RowHoverBackground);

            me.SetSolidColorBrush("MaterialDesignFlatButtonClick", theme.FlatButtonClick);
            me.SetSolidColorBrush("MaterialDesignFlatButtonRipple", theme.FlatButtonRipple);

            me.SetSolidColorBrush("MaterialDesignToolTipBackground", theme.ToolTipBackground);
            me.SetSolidColorBrush("MaterialDesignChipBackground", theme.ChipBackground);

            me.SetSolidColorBrush("MaterialDesignSnackbarBackground", theme.SnackbarBackground);
            me.SetSolidColorBrush("MaterialDesignSnackbarMouseOver", theme.SnackbarMouseOver);
            me.SetSolidColorBrush("MaterialDesignSnackbarRipple", theme.SnackbarRipple);

            me.SetSolidColorBrush("MaterialDesignTextFieldBoxBackground", theme.TextFieldBoxBackground);
            me.SetSolidColorBrush("MaterialDesignTextFieldBoxHoverBackground", theme.TextFieldBoxHoverBackground);
            me.SetSolidColorBrush("MaterialDesignTextFieldBoxDisabledBackground", theme.TextFieldBoxDisabledBackground);
            me.SetSolidColorBrush("MaterialDesignTextAreaBorder", theme.TextAreaBorder);
            me.SetSolidColorBrush("MaterialDesignTextAreaInactiveBorder", theme.TextAreaInactiveBorder);
        }

        internal static void SetColor(this ResourceDictionary me, string name, Color color)
        {
            if (me == null)
                throw new ArgumentNullException(nameof(me), "ResourceDictionary is null");

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name), "Name is null");

            if (me[name] != null && !(me[name] is Color))
                throw new ExceptionOf<ITheme>($"Wrong resource type, resource with key {name} is not Color");

            me[name] = color;
        }

        internal static void SetSolidColorBrush(this ResourceDictionary me, string name, Color color)
        {
            if (me == null)
                throw new ArgumentNullException(nameof(me), "ResourceDictionary is null");

            if (string.IsNullOrEmpty(name)) 
                throw new ArgumentNullException(nameof(name), "Name is null");

            if(me[name] != null && !(me[name] is SolidColorBrush))
                throw new ExceptionOf<ITheme>($"Wrong resource type, resource with key {name} is not SolidColorBrush");

            if((me[name] is SolidColorBrush brush) && brush.Color == color)
                return;

            var newBrush = new SolidColorBrush(color);
            newBrush.Freeze();

            me[name] = newBrush;
        }

        internal static void SetFontFamily(this ResourceDictionary me, string name, FontFamily fontFamily)
        {
            if (me == null)
                throw new ArgumentNullException(nameof(me), "ResourceDictionary is null");

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name), "Name is null");

            if (me[name] != null && !(me[name] is FontFamily))
                throw new ExceptionOf<ITheme>($"Wrong resource type, resource with key {name} is not FontFamily");

            me[name] = fontFamily;
        }

        internal static void SetDouble(this ResourceDictionary me, string name, double value)
        {
            if (me == null)
                throw new ArgumentNullException(nameof(me), "ResourceDictionary is null");

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name), "Name is null");

            if (me[name] != null && !(me[name] is double))
                throw new ExceptionOf<ITheme>($"Wrong resource type, resource with key {name} is not double");

            me[name] = value;
        }

        internal static void SetFontWeight(this ResourceDictionary me, string name, FontWeight weight)
        {
            if (me == null)
                throw new ArgumentNullException(nameof(me), "ResourceDictionary is null");

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name), "Name is null");

            if (me[name] != null && !(me[name] is FontWeight))
                throw new ExceptionOf<ITheme>($"Wrong resource type, resource with key {name} is not FontWeight");

            me[name] = weight;
        }
    }
}
