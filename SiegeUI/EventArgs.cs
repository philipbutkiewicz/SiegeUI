using SDL2;
using SiegeUI.Drawing;

namespace SiegeUI
{
    public class EventArgs
    {
        public class MouseEventArgs : System.EventArgs
        {
            #region Enums

            /// <summary>
            /// Mouse event type.
            /// </summary>
            public enum MouseEventType
            {
                None,
                MouseMoving,
                MouseUp,
                MouseDown,
                MouseWheel
            }

            /// <summary>
            /// Mouse button.
            /// </summary>
            public enum MouseButton
            {
                Left,
                Middle,
                Right,
                X1,
                X2
            }

            #endregion

            #region Properties

            /// <summary>
            /// Mouse event type.
            /// </summary>
            public MouseEventType EventType = MouseEventArgs.MouseEventType.None;

            /// <summary>
            /// Mouse position.
            /// </summary>
            public Point Position = new Point();

            /// <summary>
            /// Mouse button.
            /// </summary>
            public MouseButton Button = MouseButton.Left;

            /// <summary>
            /// Mouse wheel direction.
            /// </summary>
            public uint WheelDirection = 0;

            #endregion

            public override string ToString()
            {
                return $"{base.ToString()} {{ EventType: = {EventType}, Position: {Position} }}";
            }
        }

        public class KeyboardEventArgs : System.EventArgs
        {
            #region Enums

            /// <summary>
            /// Keyboard event type.
            /// </summary>
            public enum KeyboardEventType
            {
                None,
                KeyDown,
                KeyUp
            }

            #endregion

            #region Properties

            /// <summary>
            /// Keyboard event type.
            /// </summary>
            public KeyboardEventType EventType = KeyboardEventArgs.KeyboardEventType.None;

            /// <summary>
            /// Key code.
            /// </summary>
            public SDL.SDL_Keycode KeyCode;

            #endregion

            public override string ToString()
            {
                return $"{base.ToString()} {{ EventType: = {EventType}, KeyCode: {KeyCode} }}";
            }
        }
    }
}
