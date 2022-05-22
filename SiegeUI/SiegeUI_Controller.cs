using SDL2;

namespace SiegeUI
{
    public static class SiegeUI_Controller
    {
        #region Properties

        /// <summary>
        /// Dictionary of all the windows in the application.
        /// </summary>
        public static List<SiegeUI_Window> Windows { get; set; } = new List<SiegeUI_Window>();

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
        }

        /// <summary>
        /// Updates SiegeUI and all windows added to the controller.
        /// </summary>
        public static void Update()
        {
            foreach (SiegeUI_Window window in Windows)
            {
                window.Update();
            }
        }

        /// <summary>
        /// Destroys SiegeUI and all windows added to the controller.
        /// </summary>
        public static void Destroy()
        {
            foreach (SiegeUI_Window window in Windows)
            {
                window.Dispose();
            }

            SDL.SDL_Quit();
            Environment.Exit(0);
        }

        #endregion
    }
}