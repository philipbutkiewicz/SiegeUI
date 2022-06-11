using SDL2;

namespace SiegeUI
{
    public static class Controller
    {
        #region Properties

        /// <summary>
        /// Dictionary of all the windows in the application.
        /// </summary>
        public static List<Window> Windows { get; set; } = new List<Window>();

        #endregion

        #region Public methods

        /// <summary>
        /// Initializes SiegeUI.
        /// </summary>
        /// <exception cref="Exception"></exception>
        public static void Init()
        {
            int status = SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING);
            if (status < 0)
            {
                throw new Exception($"Siege UI failed to initialize SDL. SDL_Init failed with code {status}.");
            }

            status = SDL_ttf.TTF_Init();
            if (status < 0)
            {
                throw new Exception($"Siege UI failed to initialize SDL_ttf. TTF_Init failed with code {status}.");
            }
        }

        /// <summary>
        /// Updates SiegeUI and all windows added to the controller.
        /// </summary>
        public static void Update()
        {
            foreach (Window window in Windows)
            {
                window.Update(window.SDLRenderer);
            }
        }

        /// <summary>
        /// Destroys SiegeUI and all windows added to the controller.
        /// </summary>
        public static void Destroy()
        {
            foreach (Window window in Windows)
            {
                window.Dispose();
            }

            SDL_ttf.TTF_Quit();
            SDL.SDL_Quit();
            Environment.Exit(0);
        }

        #endregion
    }
}