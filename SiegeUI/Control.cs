using SDL2;
using SiegeUI.Drawing;
using static SiegeUI.EventArgs;

namespace SiegeUI
{
    public class Control : IDisposable
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
        public event EventHandler<SiegeUI_MouseEventArgs> WindowMouseMoving = default!;

        /// <summary>
        /// Occurs whenever the mouse button is pressed within the window.
        /// </summary>
        public event EventHandler<SiegeUI_MouseEventArgs> WindowMouseDown = default!;

        /// <summary>
        /// Occurs whenever the mouse button is released within the window.
        /// </summary>
        public event EventHandler<SiegeUI_MouseEventArgs> WindowMouseUp = default!;

        /// <summary>
        /// Occurs whenever the mouse wheel value changes within the window.
        /// </summary>
        public event EventHandler<SiegeUI_MouseEventArgs> WindowMouseWheel = default!;

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
        public event EventHandler<SiegeUI_KeyboardEventArgs> WindowKeyDown = default!;

        /// <summary>
        /// Occurs whenever the keyboard button is released within the window.
        /// </summary>
        public event EventHandler<SiegeUI_KeyboardEventArgs> WindowKeyUp = default!;

        /// <summary>
        /// Occurs whenever the mouse is moving within the control.
        /// </summary>
        public event EventHandler<SiegeUI_MouseEventArgs> MouseMoving = default!;

        /// <summary>
        /// Occurs whenever the mouse button is pressed within the control.
        /// </summary>
        public event EventHandler<SiegeUI_MouseEventArgs> MouseDown = default!;

        /// <summary>
        /// Occurs whenever the mouse button is released within the control.
        /// </summary>
        public event EventHandler<SiegeUI_MouseEventArgs> MouseUp = default!;

        /// <summary>
        /// Occurs whenever the mouse wheel value changes within the control.
        /// </summary>
        public event EventHandler<SiegeUI_MouseEventArgs> MouseWheel = default!;

        /// <summary>
        /// Occurs whenever the mouse enters the control.
        /// </summary>
        public event EventHandler MouseEntered = default!;

        /// <summary>
        /// Occurs whenever the mouse exits the control.
        /// </summary>
        public event EventHandler MouseExited = default!;

        #endregion

        #region Enums

        public enum DockingMode
        {
            None,
            Top,
            Bottom,
            Left,
            Right,
            Fill
        }

        #endregion

        #region Properties

        /// <summary>
        /// Control parent.
        /// </summary>
        public Control Parent { get; set; } = default!;

        /// <summary>
        /// Control children.
        /// </summary>
        public Dictionary<string, Control> Children = new Dictionary<string, Control>();

        /// <summary>
        /// Gets or sets control bounds (position & size).
        /// </summary>
        public virtual Rectangle Bounds { get; set; } = new Rectangle();

        /// <summary>
        /// Docking mode.
        /// </summary>
        public DockingMode Docking = DockingMode.None;

        /// <summary>
        /// Control padding.
        /// </summary>
        public Quad Padding = new Quad(4, 4, 4, 4);

        /// <summary>
        /// Background color.
        /// </summary>
        public virtual Color BackColor { get; set; } = Color.Gray;

        /// <summary>
        /// Foreground color.
        /// </summary>
        public virtual Color ForeColor { get; set; } = Color.White;

        #endregion

        #region Fields

        /// <summary>
        /// Is the mouse in bounds.
        /// </summary>
        bool _mouseInBounds = false;

        #endregion

        #region Public methods

        public Control(Control parent)
        {
            Parent = parent;

            WindowMouseMoving += Control_WindowMouseMoving;
            WindowMouseDown += Control_WindowMouseDown;
            WindowMouseUp += Control_WindowMouseUp;
            WindowMouseWheel += Control_WindowMouseWheel;
        }

        ~Control()
        {
            WindowMouseMoving -= Control_WindowMouseMoving;
            WindowMouseDown -= Control_WindowMouseDown;
            WindowMouseUp -= Control_WindowMouseUp;
            WindowMouseWheel -= Control_WindowMouseWheel;
        }

        /// <summary>
        /// Disposes of this control.
        /// </summary>
        public virtual void Dispose()
        {
            foreach (KeyValuePair<string, Control> child in Children)
            {
                child.Value?.Dispose();
            }
        }

        /// <summary>
        /// Updates the control.
        /// </summary>
        public virtual void Update(IntPtr sdlRenderer, bool renderBackground = true)
        {
            if (Parent != null)
            {
                switch (Docking)
                {
                    case DockingMode.Top:
                        Bounds = new Rectangle(
                            Parent.Bounds.X + Padding.Left, 
                            Parent.Bounds.Y + Padding.Top,
                            (Parent.Bounds.Width - Padding.Right) - Padding.Left,
                            Parent.Bounds.Height > 0 ? ((Parent.Bounds.Height / 2) - Padding.Bottom) - Padding.Top : 0
                        );
                        break;
                    case DockingMode.Bottom:
                        Bounds = new Rectangle(
                            Parent.Bounds.X + Padding.Left,
                            Parent.Bounds.Y + (Parent.Bounds.Height > 0 ? (Parent.Bounds.Height / 2) + Padding.Top : 0),
                            (Parent.Bounds.Width - Padding.Right) - Padding.Left,
                            Parent.Bounds.Height > 0 ? ((Parent.Bounds.Height / 2) - Padding.Bottom) - Padding.Top : 0
                        );
                        break;
                    case DockingMode.Left:
                        Bounds = new Rectangle(
                            Parent.Bounds.X + Padding.Left,
                            Parent.Bounds.Y + Padding.Top,
                            Parent.Bounds.Width > 0 ? ((Parent.Bounds.Width / 2) - Padding.Right) - Padding.Left : 0,
                            (Parent.Bounds.Height - Padding.Bottom) - Padding.Top
                        );
                        break;
                    case DockingMode.Right:
                        Bounds = new Rectangle(
                            Parent.Bounds.X + (Parent.Bounds.Width > 0 ? (Parent.Bounds.Width / 2) + Padding.Left : 0),
                            Parent.Bounds.Y + Padding.Top,
                            Parent.Bounds.Width > 0 ? ((Parent.Bounds.Width / 2) - Padding.Right) - Padding.Left : 0,
                            (Parent.Bounds.Height - Padding.Bottom) - Padding.Top
                        );
                        break;
                    case DockingMode.Fill:
                        Bounds = new Rectangle(
                            Bounds.X + Padding.Left,
                            Bounds.Y + Padding.Top,
                            (Bounds.Width - Padding.Right) - Padding.Left,
                            (Bounds.Height - Padding.Bottom) - Padding.Top
                        );
                        break;
                }
            }

            if (BackColor != Color.Transparent && renderBackground)
            {
                Bounds.RenderFilled(sdlRenderer, BackColor);
            }

            foreach (KeyValuePair<string, Control> child in Children)
            {
                child.Value?.Update(sdlRenderer);
            }
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
                            WindowShown?.Invoke(this, System.EventArgs.Empty);
                            break;
                        case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_HIDDEN:
                            WindowHidden?.Invoke(this, System.EventArgs.Empty);
                            break;
                        case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_CLOSE:
                            WindowClosing?.Invoke(this, System.EventArgs.Empty);
                            break;
                        case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_FOCUS_GAINED:
                            WindowFocusGained?.Invoke(this, System.EventArgs.Empty);
                            break;
                        case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_FOCUS_LOST:
                            WindowFocusLost?.Invoke(this, System.EventArgs.Empty);
                            break;
                        case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_MAXIMIZED:
                            WindowMaximized?.Invoke(this, System.EventArgs.Empty);
                            break;
                        case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_MINIMIZED:
                            WindowMinimized?.Invoke(this, System.EventArgs.Empty);
                            break;
                        case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_MOVED:
                            WindowMoved?.Invoke(this, System.EventArgs.Empty);
                            break;
                        case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_RESIZED:
                            WindowResized?.Invoke(this, System.EventArgs.Empty);
                            break;
                        case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_SIZE_CHANGED:
                            WindowSizeChanged?.Invoke(this, System.EventArgs.Empty);
                            break;
                        case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_ENTER:
                            WindowMouseEntered?.Invoke(this, System.EventArgs.Empty);
                            break;
                        case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_LEAVE:
                            WindowMouseExited?.Invoke(this, System.EventArgs.Empty);
                            break;
                    }
                    break;
                case SDL.SDL_EventType.SDL_MOUSEMOTION:
                    SiegeUI_MouseEventArgs mouseEventArgs = new SiegeUI_MouseEventArgs()
                    {
                        EventType = SiegeUI_MouseEventArgs.MouseEventType.MouseMoving,
                        Position = new Point(sdl_event.motion.x, sdl_event.motion.y)
                    };

                    WindowMouseMoving?.Invoke(this, mouseEventArgs);
                    break;
                case SDL.SDL_EventType.SDL_MOUSEWHEEL:
                    SiegeUI_MouseEventArgs mouseWheelEventArgs = new SiegeUI_MouseEventArgs()
                    {
                        EventType = SiegeUI_MouseEventArgs.MouseEventType.MouseWheel,
                        WheelDirection = sdl_event.wheel.direction,
                        Position = new Point(sdl_event.wheel.x, sdl_event.wheel.y)
                    };

                    WindowMouseWheel?.Invoke(this, mouseWheelEventArgs);
                    break;
                case SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN:
                    SiegeUI_MouseEventArgs mouseDownEventArgs = new SiegeUI_MouseEventArgs()
                    {
                        EventType = SiegeUI_MouseEventArgs.MouseEventType.MouseDown,
                        Button = (SiegeUI_MouseEventArgs.MouseButton)sdl_event.button.button,
                        Position = new Point(sdl_event.button.x, sdl_event.button.y)
                    };

                    WindowMouseDown?.Invoke(this, mouseDownEventArgs);
                    break;
                case SDL.SDL_EventType.SDL_MOUSEBUTTONUP:
                    SiegeUI_MouseEventArgs mouseUpEventArgs = new SiegeUI_MouseEventArgs()
                    {
                        EventType = SiegeUI_MouseEventArgs.MouseEventType.MouseDown,
                        Button = (SiegeUI_MouseEventArgs.MouseButton)sdl_event.button.button,
                        Position = new Point(sdl_event.button.x, sdl_event.button.y)
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

            foreach (KeyValuePair<string, Control> child in Children)
            {
                child.Value?.HandleSDLEvent(sdl_event);
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Determines whether the mouse is within control bounds.
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool IsMouseInBounds(SiegeUI_MouseEventArgs e)
        {
            if (e.Position.X >= Bounds.X && e.Position.X <= Bounds.X + Bounds.Width)
            {
                if (e.Position.Y >= Bounds.Y && e.Position.Y <= Bounds.Y + Bounds.Height)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Window mouse wheel event handler. Will invoke MouseWheel event if mouse is in control bounds.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Control_WindowMouseWheel(object? sender, SiegeUI_MouseEventArgs e)
        {
            if (IsMouseInBounds(e))
            {
                MouseWheel?.Invoke(this, e);
            }
        }

        /// <summary>
        /// Window mouse up event handler. Will invoke MouseUp event if mouse is in control bounds.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Control_WindowMouseUp(object? sender, SiegeUI_MouseEventArgs e)
        {
            if (IsMouseInBounds(e))
            {
                MouseUp?.Invoke(this, e);
            }
        }

        /// <summary>
        /// Window mouse down event handler. Will invoke MouseUp event if mouse is in control bounds.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Control_WindowMouseDown(object? sender, SiegeUI_MouseEventArgs e)
        {
            if (IsMouseInBounds(e))
            {
                MouseDown?.Invoke(this, e);
            }
        }

        /// <summary>
        /// Window mouse moving event handler. Will invoke MouseUp event if mouse is in control bounds.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Control_WindowMouseMoving(object? sender, SiegeUI_MouseEventArgs e)
        {
            if (IsMouseInBounds(e))
            {
                if (!_mouseInBounds)
                {
                    MouseEntered?.Invoke(this, System.EventArgs.Empty);
                    _mouseInBounds = true;
                }

                MouseMoving?.Invoke(this, e);
            }
            else
            {
                if (_mouseInBounds)
                {
                    MouseExited?.Invoke(this, System.EventArgs.Empty);
                    _mouseInBounds = false;
                }
            }
        }

        #endregion
    }
}
