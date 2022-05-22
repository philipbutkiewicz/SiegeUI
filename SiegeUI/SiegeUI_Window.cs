using SDL2;
using SiegeUI.Drawing;
using static SiegeUI.SiegeUI_EventArgs;

namespace SiegeUI
{
    public class SiegeUI_Window : IDisposable
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
        public event EventHandler MouseMoving = default!;

        /// <summary>
        /// Occurs whenever the mouse button is pressed within the window.
        /// </summary>
        public event EventHandler MouseDown = default!;

        /// <summary>
        /// Occurs whenever the mouse button is released within the window.
        /// </summary>
        public event EventHandler MouseUp = default!;

        /// <summary>
        /// Occurs whenever the mouse wheel value changes within the window.
        /// </summary>
        public event EventHandler MouseWheel = default!;

        /// <summary>
        /// Occurs whenever the mouse enters the window.
        /// </summary>
        public event EventHandler MouseEntered = default!;

        /// <summary>
        /// Occurs whenever the mouse exits the window.
        /// </summary>
        public event EventHandler MouseExited = default!;

        /// <summary>
        /// Occurs whenever the keyboard button is pressed within the window.
        /// </summary>
        public event EventHandler KeyDown = default!;

        /// <summary>
        /// Occurs whenever the keyboard button is released within the window.
        /// </summary>
        public event EventHandler KeyUp = default!;

        #endregion

        #region Properties

        /// <summary>
        /// Is this a main window?
        /// Main window terminates the application when it's closed.
        /// </summary>
        public bool IsMainWindow { get; set; } = true;

        /// <summary>
        /// This dictionary contains all named controls attached to this window.
        /// Every item in this dictionary will be automatically updated with the window and disposed with the window.
        /// </summary>
        public Dictionary<string, SiegeUI_Control> Controls { get; set; } = new Dictionary<string, SiegeUI_Control>();

        /// <summary>
        /// Gets or sets window bounds (position & size).
        /// </summary>
        public SiegeUI_Rectangle Bounds
        { 
            get
            {
                return GetWindowBounds();
            }
            set
            {
                SetWindowBounds(value);
            }
        }

        /// <summary>
        /// Gets or sets the sizeable status of the window.
        /// </summary>
        public bool IsSizeable
        {
            get
            {
                return _isSizeable;
            }
            set
            {
                SetWindowSizeableStatus(value);
            }
        }

        /// <summary>
        /// Window background color.
        /// </summary>
        public SiegeUI_Color BackColor = SiegeUI_Color.DarkGray;

        /// <summary>
        /// Window foreground color.
        /// </summary>
        public SiegeUI_Color ForeColor = SiegeUI_Color.White;

        public

        #endregion

        #region Fields

        /// <summary>
        /// Is this window sizeable?
        /// </summary>
        bool _isSizeable = true;

        #endregion

        #region Pointers

        /// <summary>
        /// SDL_Window reference.
        /// </summary>
        IntPtr _sdlWindow;

        /// <summary>
        /// SDL_Renderer reference.
        /// </summary>
        IntPtr _sdlRenderer;

        #endregion

        #region Enums

        /// <summary>
        /// Window flags (bit field).
        /// </summary>
        [Flags]
        public enum WindowFlags
        {
            Sizeable = (int)SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE,
            Borderless = (int)SDL.SDL_WindowFlags.SDL_WINDOW_BORDERLESS,
            Utility = (int)SDL.SDL_WindowFlags.SDL_WINDOW_UTILITY,
            OpenGL = (int)SDL.SDL_WindowFlags.SDL_WINDOW_OPENGL,
            Vulkan = (int)SDL.SDL_WindowFlags.SDL_WINDOW_VULKAN,
            Fullscreen = (int)SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN
        };

        #endregion

        #region Public methods

        /// <summary>
        /// SiegeUI Window is a container for all controls.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="bounds"></param>
        /// <param name="windowFlags"></param>
        public SiegeUI_Window(string title, SiegeUI_Rectangle bounds, WindowFlags windowFlags)
        {
            CreateWindow(title, bounds, windowFlags);
            CreateRenderer();
            SetupRenderer();
        }

        /// <summary>
        /// Disposes of this object.
        /// </summary>
        public void Dispose()
        {
            foreach (KeyValuePair<string, SiegeUI_Control> _control in Controls)
            {
                _control.Value.Dispose();
            }

            Controls.Clear();

            SDL.SDL_DestroyRenderer(_sdlRenderer);
            SDL.SDL_DestroyWindow(_sdlWindow);
        }

        /// <summary>
        /// Window update method.
        /// </summary>
        public void Update()
        {
            SDL.SDL_Event sdl_event;
            SDL.SDL_WaitEvent(out sdl_event);
            HandleSDLEvent(sdl_event);

            SDL.SDL_RenderClear(_sdlRenderer);

            new SiegeUI_Rectangle(0, 0, Bounds.Width, Bounds.Height).RenderFilled(_sdlRenderer, BackColor);

            foreach (KeyValuePair<string, SiegeUI_Control> _control in Controls)
            {
                _control.Value?.Update();
            }

            SDL.SDL_UpdateWindowSurface(_sdlWindow);
            SDL.SDL_RenderPresent(_sdlRenderer);
        }

        /// <summary>
        /// Shows the window.
        /// </summary>
        public void Show()
        {
            SDL.SDL_ShowWindow(_sdlWindow);
        }

        /// <summary>
        /// Hides the window.
        /// </summary>
        public void Hide()
        {
            SDL.SDL_HideWindow(_sdlWindow);
        }

        /// <summary>
        /// Closes the window.
        /// </summary>
        public void Close()
        {
            Hide();
            Dispose();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Creates a new SDL_Window and assigns a reference to this object.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="bounds"></param>
        /// <param name="windowFlags"></param>
        /// <exception cref="Exception"></exception>
        private void CreateWindow(string title, SiegeUI_Rectangle bounds, WindowFlags windowFlags)
        {
            _sdlWindow = SDL.SDL_CreateWindow(title, bounds.X, bounds.Y, bounds.Width, bounds.Height, (SDL.SDL_WindowFlags)windowFlags);
            if (_sdlWindow == IntPtr.Zero)
            {
                throw new Exception($"Failed to create a new window: {_sdlWindow}");
            }
        }

        /// <summary>
        /// Creates a new SDL_Renderer and assigns a reference to this object.
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void CreateRenderer()
        {
            _sdlRenderer = SDL.SDL_CreateRenderer(_sdlWindow, 0, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED);
            if (_sdlRenderer == IntPtr.Zero)
            {
                throw new Exception($"Failed to create a renderer: {_sdlRenderer}");
            }
        }

        /// <summary>
        /// Sets up renderer settings (blend mode etc.)
        /// </summary>
        private void SetupRenderer()
        {
            SDL.SDL_SetRenderDrawBlendMode(_sdlRenderer, SDL.SDL_BlendMode.SDL_BLENDMODE_BLEND);
        }

        /// <summary>
        /// Handles incoming SDL events.
        /// </summary>
        /// <param name="sdl_event"></param>
        private void HandleSDLEvent(SDL.SDL_Event sdl_event)
        {
            uint windowID = SDL.SDL_GetWindowID(_sdlWindow);
            if (sdl_event.window.windowID != windowID)
            {
                return;
            }

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
                            Close();

                            if (IsMainWindow)
                            {
                                SiegeUI_Controller.Destroy();
                            }

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
                            MouseEntered?.Invoke(this, EventArgs.Empty);
                            break;
                        case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_LEAVE:
                            MouseExited?.Invoke(this, EventArgs.Empty);
                            break;
                    }
                    break;
                case SDL.SDL_EventType.SDL_MOUSEMOTION:
                    SiegeUI_MouseEventArgs mouseEventArgs = new SiegeUI_MouseEventArgs()
                    {
                        EventType = SiegeUI_MouseEventArgs.MouseEventType.MouseMoving,
                        Position = new SiegeUI_Point(sdl_event.motion.x, sdl_event.motion.y)
                    };

                    MouseMoving?.Invoke(this, mouseEventArgs);
                    break;
                case SDL.SDL_EventType.SDL_MOUSEWHEEL:
                    SiegeUI_MouseEventArgs mouseWheelEventArgs = new SiegeUI_MouseEventArgs()
                    {
                        EventType = SiegeUI_MouseEventArgs.MouseEventType.MouseWheel,
                        WheelDirection = sdl_event.wheel.direction
                    };

                    MouseWheel?.Invoke(this, mouseWheelEventArgs);
                    break;
                case SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN:
                    SiegeUI_MouseEventArgs mouseDownEventArgs = new SiegeUI_MouseEventArgs()
                    {
                        EventType = SiegeUI_MouseEventArgs.MouseEventType.MouseDown,
                        Button = (SiegeUI_MouseEventArgs.MouseButton)sdl_event.button.button
                    };

                    MouseDown?.Invoke(this, mouseDownEventArgs);
                    break;
                case SDL.SDL_EventType.SDL_MOUSEBUTTONUP:
                    SiegeUI_MouseEventArgs mouseUpEventArgs = new SiegeUI_MouseEventArgs()
                    {
                        EventType = SiegeUI_MouseEventArgs.MouseEventType.MouseDown,
                        Button = (SiegeUI_MouseEventArgs.MouseButton)sdl_event.button.button
                    };

                    MouseUp?.Invoke(this, mouseUpEventArgs);
                    break;
                case SDL.SDL_EventType.SDL_KEYDOWN:
                    SiegeUI_KeyboardEventArgs keyDownEventArgs = new SiegeUI_KeyboardEventArgs()
                    {
                        EventType = SiegeUI_KeyboardEventArgs.KeyboardEventType.KeyDown,
                        KeyCode = sdl_event.key.keysym.sym
                    };

                    KeyDown?.Invoke(this, keyDownEventArgs);
                    break;
                case SDL.SDL_EventType.SDL_KEYUP:
                    SiegeUI_KeyboardEventArgs keyUpEventArgs = new SiegeUI_KeyboardEventArgs()
                    {
                        EventType = SiegeUI_KeyboardEventArgs.KeyboardEventType.KeyUp,
                        KeyCode = sdl_event.key.keysym.sym
                    };

                    KeyUp?.Invoke(this, keyUpEventArgs);
                    break;
            }
        }

        /// <summary>
        /// Returns window bounds (SDL).
        /// </summary>
        /// <returns></returns>
        private SiegeUI_Rectangle GetWindowBounds()
        {
            int x, y;
            SDL.SDL_GetWindowPosition(_sdlWindow, out x, out y);

            int w, h;
            SDL.SDL_GetWindowSize(_sdlWindow, out w, out h);

            return new SiegeUI_Rectangle(x, y, w, h);
        }

        /// <summary>
        /// Sets window bounds (SDL).
        /// </summary>
        /// <param name="bounds"></param>
        private void SetWindowBounds(SiegeUI_Rectangle bounds)
        {
            SDL.SDL_SetWindowPosition(_sdlWindow, bounds.X, bounds.Y);
            SDL.SDL_SetWindowSize(_sdlWindow, bounds.Width, bounds.Height);
        }

        /// <summary>
        /// Sets window sizeable status (SDL).
        /// </summary>
        /// <param name="status"></param>
        private void SetWindowSizeableStatus(bool status)
        {
            SDL.SDL_SetWindowResizable(_sdlWindow, status ? SDL.SDL_bool.SDL_TRUE : SDL.SDL_bool.SDL_FALSE);
            _isSizeable = status;
        }

        #endregion
    }
}
