using Microsoft.Xna.Framework.Input;

namespace Bazaar.resources {
    public static class Inputs {
        public const int Up = 0;
        public const int Right = 1;
        public const int Down = 2;
        public const int Left = 3;

        public const int Start = 4;
        public const int Select = 5;

        public const int Button1 = 6;
        public const int Button2 = 7;

        public static readonly int[] SkipDialog = { Button1, Button2, Start };
        public static readonly int[] MenuConfirm = { Button1, Start };

        public static Buttons[] GamepadMappings {
            get {
                var buttonMappings = new Buttons[8];

                buttonMappings[Up] = Buttons.DPadUp;
                buttonMappings[Right] = Buttons.DPadRight;
                buttonMappings[Down] = Buttons.DPadDown;
                buttonMappings[Left] = Buttons.DPadLeft;
                buttonMappings[Start] = Buttons.Start;
                buttonMappings[Select] = Buttons.Back;
                buttonMappings[Button1] = Buttons.A;
                buttonMappings[Button2] = Buttons.X;

                return buttonMappings;
            }
        }

        public static Keys[] PrimaryKeyboardMap {
            get {
                var keyMap = new Keys[8];

                keyMap[Up] = Keys.Up;
                keyMap[Right] = Keys.Right;
                keyMap[Down] = Keys.Down;
                keyMap[Left] = Keys.Left;
                keyMap[Start] = Keys.Enter;
                keyMap[Select] = Keys.Escape;
                keyMap[Button1] = Keys.M;
                keyMap[Button2] = Keys.N;

                return keyMap;
            }
        }

        public static Keys[] AltKeyboardMap {
            get {
                var keyMap = new Keys[8];

                keyMap[Up] = Keys.W;
                keyMap[Right] = Keys.D;
                keyMap[Down] = Keys.S;
                keyMap[Left] = Keys.A;
                keyMap[Start] = Keys.Enter;
                keyMap[Select] = Keys.Escape;
                keyMap[Button1] = Keys.X;
                keyMap[Button2] = Keys.Z;

                return keyMap;
            }
        }
    }
}
