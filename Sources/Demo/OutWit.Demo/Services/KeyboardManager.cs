using OutWit.Common.Controls.Interfaces;
using System;
using System.Windows.Input;
using OutWit.Demo.Views;
using KeyboardEventHandler = OutWit.Common.Controls.Interfaces.KeyboardEventHandler;

namespace OutWit.Demo.Services
{
    public class KeyboardManager : IKeyboardManager
    {
        public event KeyboardEventHandler KeyDown = delegate { };
        public event KeyboardEventHandler KeyUp = delegate { };


        public event KeyPressedEventHandler EnterKeyPressed = delegate { };
        public event KeyPressedEventHandler EscapeKeyPressed = delegate { };
        public event KeyPressedEventHandler F5KeyPressed = delegate { };
        public event KeyPressedEventHandler PageUpKeyPressed = delegate { };
        public event KeyPressedEventHandler PageDownKeyPressed = delegate { };
        public event KeyPressedEventHandler LeftArrowKeyPressed = delegate { };
        public event KeyPressedEventHandler RightArrowKeyPressed = delegate { };
        public event KeyPressedEventHandler UpArrowKeyPressed = delegate { };
        public event KeyPressedEventHandler DownArrowKeyPressed = delegate { };
        public event KeyPressedEventHandler SaveCombinationPressed = delegate { };
        public event KeyPressedEventHandler UndoCombinationPressed = delegate { };

        public KeyboardManager(MainWindow window)
        {
            Window = window;
            InitEvents();
        }

        private void InitEvents()
        {
            Window.PreviewKeyDown += OnPreviewKeyDown;
            Window.PreviewKeyUp += OnPreviewKeyUp;
        }

        private void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            KeyDown(e);

            if (e.Key == Key.Enter)
                EnterKeyPressed();

            else if (e.Key == Key.Escape)
                EscapeKeyPressed();

            else if (e.Key == Key.F5)
                F5KeyPressed();

            else if (e.Key == Key.S && Keyboard.Modifiers == ModifierKeys.Control)
                SaveCombinationPressed();

            else if (e.Key == Key.Z && Keyboard.Modifiers == ModifierKeys.Control)
                UndoCombinationPressed();
        }


        private void OnPreviewKeyUp(object sender, KeyEventArgs e)
        {
            KeyUp(e);
        }

        private MainWindow Window { get; }
    }
}
