using System;
using System.Collections.Generic;
using System.Text;

namespace OutWit.Themes.Interfaces
{
    public interface IThemeContainerBase
    {
        event CurrentThemeChangedEventHandler CurrentThemeChanged;

        ITheme Current { get; }
    }

    public delegate void CurrentThemeChangedEventHandler(ITheme currentTheme);
}
