using SDL2;

namespace SiegeUI
{
    public class SiegeUI_Control : IDisposable
    {
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
        public void Dispose()
        {
            foreach (KeyValuePair<string, IntPtr> _texture in _textures)
            {
                SDL.SDL_DestroyTexture(_texture.Value);
            }
        }

        /// <summary>
        /// Updates the control.
        /// </summary>
        public void Update()
        {

        }

        #endregion
    }
}
