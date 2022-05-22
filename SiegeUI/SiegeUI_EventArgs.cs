using SDL2;
using SiegeUI.Drawing;

namespace SiegeUI
{
    public  class SiegeUI_EventArgs
    {
        public class SiegeUI_MouseEventArgs : EventArgs
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
            public MouseEventType EventType = SiegeUI_MouseEventArgs.MouseEventType.None;

            /// <summary>
            /// Mouse position.
            /// </summary>
            public SiegeUI_Point Position = new SiegeUI_Point();

            /// <summary>
            /// Mouse button.
            /// </summary>
            public MouseButton Button = MouseButton.Left;

            /// <summary>
            /// Mouse wheel direction.
            /// </summary>
            public uint WheelDirection = 0;

            #endregion
        }

        public class SiegeUI_KeyboardEventArgs : EventArgs
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
            public KeyboardEventType EventType = SiegeUI_KeyboardEventArgs.KeyboardEventType.None;

            /// <summary>
            /// Key code.
            /// </summary>
            public SDL.SDL_Keycode KeyCode;

            #endregion
        }
    }
}
