using OpenTK;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Engine.Core
{
    static class Input
    {
        private const int NUM_KEYS = 132;
        private const int NUM_MOUSEBUTTONS = 13;

        private static bool[] prevKeysPressed = new bool[NUM_KEYS];
        private static bool[] prevMouseButtonsPressed = new bool[NUM_MOUSEBUTTONS];

        private static KeyboardState kbState;
        private static MouseState msState;

        private static GameWindow window;

        public static Vector2 MousePosition
        {
            get
            {
                msState = Mouse.GetCursorState();
                return new Vector2(msState.X - window.Bounds.X, msState.Y - window.Bounds.Y);
            }
        }

        public static Vector2 PrevMousePosition { get; set; }

        public static Vector2 DeltaMouse { get; set; }

        public static int ScrollWheelPos
        {
            get
            {
                msState = Mouse.GetState();
                return msState.Wheel;
            }
        }

        public static void Initialize(GameWindow window)
        {
            Input.window = window;
        }

        public static void Update()
        {
            for (int i = 0; i < NUM_KEYS; i++)
                prevKeysPressed[i] = kbState.IsKeyDown((Key)i);
            for (int i = 0; i < NUM_MOUSEBUTTONS; i++)
                prevMouseButtonsPressed[i] = msState.IsButtonDown((MouseButton)i);

            DeltaMouse = MousePosition - PrevMousePosition;
        }

        public static void AfterUpdate()
        {
            PrevMousePosition = MousePosition;
        }

        public static void CenterMouse()
        {
            //Cursor.Position = new Point(window.Location.X + (int)Screen.Center.X, window.Location.Y + (int)Screen.Center.Y);
            Mouse.SetPosition(window.Location.X + (int)Screen.Center.X, window.Location.Y + (int)Screen.Center.Y);
        }

        /// /// <summary>
        /// Checks if a key is held down.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsKeyDown(Key key)
        {
            kbState = Keyboard.GetState();
            return kbState.IsKeyDown(key);
        }

        public static bool IsMouseDown(MouseButton button)
        {
            msState = Mouse.GetState();
            return msState.IsButtonDown(button);
        }

        /// <summary>
        /// Checks if a key is pressed once this frame.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsKeyPressed(Key key)
        {
            return IsKeyDown(key) && !prevKeysPressed[(int)key];
        }

        public static bool IsMouseButtonPressed(MouseButton button)
        {
            return IsMouseDown(button) && !prevMouseButtonsPressed[(int)button];
        }
    }
}
