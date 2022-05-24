using SDL2;
using SiegeUI.Drawing;
using static SiegeUI.SiegeUI_EventArgs;

namespace SiegeUI
{
    public class SiegeUI_Control : IDisposable
    {
        #region Events

        /// <summary>
        /// Occurs whenever the window is shown.
        /// </summary>
        public event EventHandler WindowShown = default!;

        /// <summary>
        /// Occurs whenever the window is hidden.
        /// </summary>
        public event EventHandler WindowHidden = default!;

        /// <summary>
        /// Occurs whenever the window begins closing.
        /// </summary>
        public event EventHandler WindowClosing = default!;

        /// <summary>
        /// Occurs whenever the window gains focus.
        /// </summary>
        public event EventHandler WindowFocusGained = default!;

        /// <summary>
        /// Occurs whenever the window loses focus.
        /// </summary>
        public event EventHandler WindowFocusLost = default!;

        /// <summary>
        /// Occurs whenever the window is maximized.
        /// </summary>
        public event EventHandler WindowMaximized = default!;

        /// <summary>
        /// Occurs whenever the window is minimized.
        /// </summary>
        public event EventHandler WindowMinimized = default!;

        /// <summary>
        /// Occurs whenever the window is moved.
        /// </summary>
        public event EventHandler WindowMoved = default!;

        /// <summary>
        /// Occurs whenever the window is resized.
        /// </summary>
        public event EventHandler WindowResized = default!;

        /// <summary>
        /// Occurs whenever the window size is changed.
        /// </summary>
        public event EventHandler WindowSizeChanged = default!;

        /// <summary>
        /// Occurs whenever the mouse is moving within the window.
        /// </summary>
        public event EventHandler WindowMouseMoving = default!;

        /// <summary>
        /// Occurs whenever the mouse button is pressed within the window.
        /// </summary>
        public event EventHandler WindowMouseDown = default!;

        /// <summary>
        /// Occurs whenever the mouse button is released within the window.
        /// </summary>
        public event EventHandler WindowMouseUp = default!;

        /// <summary>
        /// Occurs whenever the mouse wheel value changes within the window.
        /// </summary>
        public event EventHandler WindowMouseWheel = default!;

        /// <summary>
        /// Occurs whenever the mouse enters the window.
        /// </summary>
        public event EventHandler WindowMouseEntered = default!;

        /// <summary>
        /// Occurs whenever the mouse exits the window.
        /// </summary>
        public event EventHandler WindowMouseExited = default!;

        /// <summary>
        /// Occurs whenever the keyboard button is pressed within the window.
        /// </summary>
        public event EventHandler WindowKeyDown = default!;

        /// <summary>
        /// Occurs whenever the keyboard button is released within the window.
        /// </summary>
        public event EventHandler WindowKeyUp = default!;

        #endregion

        #region Fields

        /// <summary>
        /// Textures associated with this control.
        /// </summary>
        protected Dictionary<string, IntPtr> _textures = new Dictionary<string, IntPtr>();

        #endregion

        #region Public methods

        /// <summary>
        /// Disposes of this control.
        /// </summary>
        public virtual void Dispose()
        {
            foreach (KeyValuePair<string, IntPtr> _texture in _textures)
            {
                SDL.SDL_DestroyTexture(_texture.Value);
            }
        }

        /// <summary>
        /// Updates the control.
        /// </summary>
        public virtual void Update()
        {

        }

        /// <summary>
        /// Handles events fired off by SDL.
        /// </summary>
        /// <param name="sdl_event"></param>
        public virtual void HandleSDLEvent(SDL.SDL_Event sdl_event)
        {
            switch (sdl_event.type)
            {
                case SDL.SDL_EventType.SDL_WINDOWEVENT:
                    switch (sdl_event.window.windowEvent)
                    {
                        case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_SHOWN:
                            WindowShown?.Invoke(this, EventArgs.Empty);
                            break;
                        case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_HIDDEN:
                            WindowHidden?.Invoke(this, EventArgs.Empty);
                            break;
                        case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_CLOSE:
                            WindowClosing?.Invoke(this, EventArgs.Empty);
                            break;
                        case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_FOCUS_GAINED:
                            WindowFocusGained?.Invoke(this, EventArgs.Empty);
                            break;
                        case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_FOCUS_LOST:
                            WindowFocusLost?.Invoke(this, EventArgs.Empty);
                            break;
                        case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_MAXIMIZED:
                            WindowMaximized?.Invoke(this, EventArgs.Empty);
                            break;
                        case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_MINIMIZED:
                            WindowMinimized?.Invoke(this, EventArgs.Empty);
                            break;
                        case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_MOVED:
                            WindowMoved?.Invoke(this, EventArgs.Empty);
                            break;
                        case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_RESIZED:
                            WindowResized?.Invoke(this, EventArgs.Empty);
                            break;
                        case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_SIZE_CHANGED:
                            WindowSizeChanged?.Invoke(this, EventArgs.Empty);
                            break;
                        case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_ENTER:
                            WindowMouseEntered?.Invoke(this, EventArgs.Empty);
                            break;
                        case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_LEAVE:
                            WindowMouseExited?.Invoke(this, EventArgs.Empty);
                            break;
                    }
                    break;
                case SDL.SDL_EventType.SDL_MOUSEMOTION:
                    SiegeUI_MouseEventArgs mouseEventArgs = new SiegeUI_MouseEventArgs()
                    {
                        EventType = SiegeUI_MouseEventArgs.MouseEventType.MouseMoving,
                        Position = new SiegeUI_Point(sdl_event.motion.x, sdl_event.motion.y)
                    };

                    WindowMouseMoving?.Invoke(this, mouseEventArgs);
                    break;
                case SDL.SDL_EventType.SDL_MOUSEWHEEL:
                    SiegeUI_MouseEventArgs mouseWheelEventArgs = new SiegeUI_MouseEventArgs()
                    {
                        EventType = SiegeUI_MouseEventArgs.MouseEventType.MouseWheel,
                        WheelDirection = sdl_event.wheel.direction
                    };

                    WindowMouseWheel?.Invoke(this, mouseWheelEventArgs);
                    break;
                case SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN:
                    SiegeUI_MouseEventArgs mouseDownEventArgs = new SiegeUI_MouseEventArgs()
                    {
                        EventType = SiegeUI_MouseEventArgs.MouseEventType.MouseDown,
                        Button = (SiegeUI_MouseEventArgs.MouseButton)sdl_event.button.button
                    };

                    WindowMouseDown?.Invoke(this, mouseDownEventArgs);
                    break;
                case SDL.SDL_EventType.SDL_MOUSEBUTTONUP:
                    SiegeUI_MouseEventArgs mouseUpEventArgs = new SiegeUI_MouseEventArgs()
                    {
                        EventType = SiegeUI_MouseEventArgs.MouseEventType.MouseDown,
                        Button = (SiegeUI_MouseEventArgs.MouseButton)sdl_event.button.button
                    };

                    WindowMouseUp?.Invoke(this, mouseUpEventArgs);
                    break;
                case SDL.SDL_EventType.SDL_KEYDOWN:
                    SiegeUI_KeyboardEventArgs keyDownEventArgs = new SiegeUI_KeyboardEventArgs()
                    {
                        EventType = SiegeUI_KeyboardEventArgs.KeyboardEventType.KeyDown,
                        KeyCode = sdl_event.key.keysym.sym
                    };

                    WindowKeyDown?.Invoke(this, keyDownEventArgs);
                    break;
                case SDL.SDL_EventType.SDL_KEYUP:
                    SiegeUI_KeyboardEventArgs keyUpEventArgs = new SiegeUI_KeyboardEventArgs()
                    {
                        EventType = SiegeUI_KeyboardEventArgs.KeyboardEventType.KeyUp,
                        KeyCode = sdl_event.key.keysym.sym
                    };

                    WindowKeyUp?.Invoke(this, keyUpEventArgs);
                    break;
            }
        }

        #endregion
    }
}
