using SDL2;
using SiegeUI.Drawing;

namespace SiegeUI
{
    public class Window : Control
    {
        #region Properties

        /// <summary>
        /// Is this a main window?
        /// Main window terminates the application when it's closed.
        /// </summary>
        public bool IsMainWindow { get; set; } = true;

        /// <summary>
        /// Gets bounds (position & size).
        /// </summary>
        public override Rectangle Bounds
        {
            get
            {
                Rectangle windowBounds = GetWindowBounds();
                return new Rectangle(0, 0, windowBounds.Width, windowBounds.Height);
            }
        }

        /// <summary>
        /// Gets or sets window bounds (position & size).
        /// </summary>
        public Rectangle WindowBounds
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
        /// Gets the SDL renderer for the window.
        /// </summary>
        public IntPtr SDLRenderer
        {
            get
            {
                return _sdlRenderer;
            }
        }

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
        /// <param name="parent"></param>
        /// <param name="title"></param>
        /// <param name="windowBounds"></param>
        /// <param name="windowFlags"></param>
        public Window(Control parent, string title, Rectangle windowBounds, WindowFlags windowFlags) : base(parent)
        {
            CreateWindow(title, windowBounds, windowFlags);
            CreateRenderer();
            SetupRenderer();
        }

        /// <summary>
        /// Disposes of this object.
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();

            SDL.SDL_DestroyRenderer(_sdlRenderer);
            SDL.SDL_DestroyWindow(_sdlWindow);
        }

        /// <summary>
        /// Window update method.
        /// </summary>
        /// <param name="sdlRenderer"></param>
        /// <param name="renderBackground"></param>
        public override void Update(IntPtr sdlRenderer, bool renderBackground = true)
        {
            SDL.SDL_Event sdl_event;
            SDL.SDL_WaitEvent(out sdl_event);
            HandleSDLEvent(sdl_event);

            base.Update(sdlRenderer, renderBackground);

            SDL.SDL_UpdateWindowSurface(_sdlWindow);
            SDL.SDL_RenderPresent(sdlRenderer);
        }


        /// <summary>
        /// Handles incoming SDL events.
        /// </summary>
        /// <param name="sdl_event"></param>
        public override void HandleSDLEvent(SDL.SDL_Event sdl_event)
        {
            uint windowID = SDL.SDL_GetWindowID(_sdlWindow);
            if (sdl_event.window.windowID != windowID)
            {
                return;
            }

            base.HandleSDLEvent(sdl_event);

            if (sdl_event.type == SDL.SDL_EventType.SDL_WINDOWEVENT && sdl_event.window.windowEvent == SDL.SDL_WindowEventID.SDL_WINDOWEVENT_CLOSE)
            {
                Close();

                if (IsMainWindow)
                {
                    Controller.Destroy();
                }
            }
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
        /// <param name="windowBounds"></param>
        /// <param name="windowFlags"></param>
        /// <exception cref="Exception"></exception>
        void CreateWindow(string title, Rectangle windowBounds, WindowFlags windowFlags)
        {
            _sdlWindow = SDL.SDL_CreateWindow(title, windowBounds.X, windowBounds.Y, windowBounds.Width, windowBounds.Height, (SDL.SDL_WindowFlags)windowFlags);
            if (_sdlWindow == IntPtr.Zero)
            {
                throw new Exception($"Failed to create a new window: {_sdlWindow}");
            }
        }

        /// <summary>
        /// Creates a new SDL_Renderer and assigns a reference to this object.
        /// </summary>
        /// <exception cref="Exception"></exception>
        void CreateRenderer()
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
        void SetupRenderer()
        {
            SDL.SDL_SetRenderDrawBlendMode(_sdlRenderer, SDL.SDL_BlendMode.SDL_BLENDMODE_BLEND);
        }

        /// <summary>
        /// Returns window bounds (SDL).
        /// </summary>
        /// <returns></returns>
        private Rectangle GetWindowBounds()
        {
            int x, y;
            SDL.SDL_GetWindowPosition(_sdlWindow, out x, out y);

            int w, h;
            SDL.SDL_GetWindowSize(_sdlWindow, out w, out h);

            return new Rectangle(x, y, w, h);
        }

        /// <summary>
        /// Sets window bounds (SDL).
        /// </summary>
        /// <param name="windowBounds"></param>
        void SetWindowBounds(Rectangle windowBounds)
        {
            SDL.SDL_SetWindowPosition(_sdlWindow, windowBounds.X, windowBounds.Y);
            SDL.SDL_SetWindowSize(_sdlWindow, windowBounds.Width, windowBounds.Height);
        }

        /// <summary>
        /// Sets window sizeable status (SDL).
        /// </summary>
        /// <param name="status"></param>
        void SetWindowSizeableStatus(bool status)
        {
            SDL.SDL_SetWindowResizable(_sdlWindow, status ? SDL.SDL_bool.SDL_TRUE : SDL.SDL_bool.SDL_FALSE);
            _isSizeable = status;
        }

        #endregion
    }
}
