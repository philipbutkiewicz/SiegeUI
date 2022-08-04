using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiegeUI.Drawing
{
    public class Image
    {
        #region Props

        /// <summary>
        /// Returns the SDL_Texture associated with this object/
        /// </summary>
        public IntPtr SDLTexture
        {
            get
            {
                return _sdlTexture;
            }
        }

        /// <summary>
        /// Size of this image.
        /// </summary>
        public Size Size
        {
            get
            {
                return GetImageSize();
            }
        }

        /// <summary>
        /// Image render bounds.
        /// </summary>
        public Rectangle Bounds
        {
            get
            {
                return _bounds;
            }
            set
            {
                _bounds = value;
            }
        }

        #endregion

        #region Fields

        /// <summary>
        /// SDL renderer associated with this image.
        /// </summary>
        private IntPtr _sdlRenderer;

        /// <summary>
        /// Texture associated with this image.
        /// </summary>
        private IntPtr _sdlTexture;

        /// <summary>
        /// Image render bounds.
        /// </summary>
        private Rectangle _bounds = new Rectangle();

        #endregion

        #region Public methods

        public Image()
        {
        }

        public Image(IntPtr sdlRenderer, string fileName)
        {
            _sdlRenderer = sdlRenderer;
            _sdlTexture = LoadTextureFromFile(fileName);
        }

        /// <summary>
        /// Renders this Image.
        /// </summary>
        public void Render()
        {
            Size textureSize = GetImageSize();

            SDL.SDL_Rect srcRect = new Rectangle(0, 0, textureSize.Width, textureSize.Height).ToSDLRect();
            SDL.SDL_Rect dstRect = _bounds.ToSDLRect();

            SDL.SDL_RenderCopy(_sdlRenderer, _sdlTexture, ref srcRect, ref dstRect);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Loads a texture from file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        /// <exception cref="FileLoadException"></exception>
        private IntPtr LoadTextureFromFile(string fileName)
        {
            IntPtr surface = SDL.SDL_LoadBMP(fileName);
            if (surface == IntPtr.Zero)
            {
                throw new FileLoadException($"Texture {fileName} failed to load.");
            }

            IntPtr texture = SDL.SDL_CreateTextureFromSurface(_sdlRenderer, surface);
            if (texture == IntPtr.Zero)
            {
                throw new FileLoadException($"Could not create a texture from the image surface, perhaps the SDL renderer was not passed correctly?");
            }

            return texture;
        }

        /// <summary>
        /// Get size of the texture associated with this object.
        /// </summary>
        /// <returns></returns>
        private Size GetImageSize()
        {
            uint format = 0;
            int access = 0;
            int w = 0;
            int h = 0;
            SDL.SDL_QueryTexture(_sdlTexture, out format, out access, out w, out h);

            return new Size(w, h);
        }

        #endregion
    }
}
