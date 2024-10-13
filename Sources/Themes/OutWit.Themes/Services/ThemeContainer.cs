using System.Collections.Concurrent;
using System.Collections.Generic;
using OutWit.Common.Utils;
using OutWit.Themes.Interfaces;

namespace OutWit.Themes.Services
{
    internal class ThemeContainer : IThemeContainer
    {
        #region Events

        public event CurrentThemeChangedEventHandler CurrentThemeChanged = delegate { };

        #endregion

        #region Constructors

        public ThemeContainer()
        {
            Themes = new ConcurrentDictionary<string, ITheme>();
        }

        #endregion

        #region Functions

        public void Register(ITheme theme)
        {
            if(!Themes.TryAdd(theme.Key, theme))
                return;

            if (Current == null)
                Current = theme;
        }

        public void SetCurrentTheme(string key)
        {
            if(!Themes.ContainsKey(key))
                return;

            Current = Themes[key];
            CurrentThemeChanged(Current);
        }

        public void Clear()
        {
            Themes.Clear();
            Current = null;
        }

        public override string ToString()
        {
            if (Themes.Count == 0)
                return "No themes available";

            var str = "";

            if (Themes.Count == 1)
                str += $"1 Theme: ";
            else
                str += $"{Themes.Count} Themes: ";

            foreach (var themesKey in Themes.Keys)
                str += $"{themesKey}, ";

            return str.TrimEnd(2);
        }

        #endregion

        #region Properties

        public IEnumerable<string> AllThemes => Themes.Keys;

        public ITheme Current { get; private set; }

        private ConcurrentDictionary<string, ITheme> Themes { get; } 

        #endregion
    }
}
