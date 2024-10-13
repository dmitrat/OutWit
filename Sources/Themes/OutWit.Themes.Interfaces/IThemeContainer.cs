using System;
using System.Collections.Generic;
using System.Text;

namespace OutWit.Themes.Interfaces
{
    public interface IThemeContainer : IThemeContainerBase
    {
        void Register(ITheme theme);

        void SetCurrentTheme(string key);

        void Clear();

        IEnumerable<string> AllThemes { get; }

    }
}
