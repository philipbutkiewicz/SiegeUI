using SDL2;

namespace SiegeUI.Drawing
{
    public class SiegeUI_Text
    {
        #region Properties

        /// <summary>
        /// Text to render.
        /// </summary>
        public string Text {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                _textureNeedsUpdating = true;
            }
        }

        /// <summary>
        /// Rendering bounds.
        /// </summary>
        public SiegeUI_Rectangle Bounds
        {
            get
            {
                return _bounds;
            }
            set
            {
                _bounds = value;
                if (value.Width != _bounds.Width || value.Height != _bounds.Height)
                {
                    _textureNeedsUpdating = true;
                }
            }
        }

        /// <summary>
        /// Foint size (in points).
        /// </summary>
        public int FontSize
        {
            get
            {
                return _fontSize;
            }
            set
            {
                _fontSize = value;
                _textureNeedsUpdating = true;
            }
        }

        /// <summary>
        /// Font to use.
        /// </summary>
        public string Font
        {
            get
            {
                return _font;
            }
            set
            {
                _font = value;
                _textureNeedsUpdating = true;
            }
        }

        /// <summary>
        /// Background color.
        /// </summary>
        public SiegeUI_Color BackColor
        {
            get
            {
                return _backColor;
            }
            set
            {
                _backColor = value;
                _textureNeedsUpdating = true;
            }
        }

        /// <summary>
        /// Foreground color.
        /// </summary>
        public SiegeUI_Color ForeColor
        {
            get
            {
                return _foreColor;
            }
            set
            {
                _foreColor = value;
                _textureNeedsUpdating = true;
            }
        }

        /// <summary>
        /// Shadow color.
        /// </summary>
        public SiegeUI_Color ShadowColor
        {
            get
            {
                return _shadowColor;
            }
            set
            {
                _shadowColor = value;
                _textureNeedsUpdating = true;
            }
        }

        /// <summary>
        /// Shadow offset position.
        /// </summary>
        public SiegeUI_Point ShadowOffset
        {
            get
            {
                return _shadowOffset;
            }
            set
            {
                _shadowOffset = value;
                _textureNeedsUpdating = true;
            }
        }

        /// <summary>
        /// Shadow blur.
        /// </summary>
        public int ShadowBlur
        {
            get
            {
                return _shadowBlur;
            }
            set
            {
                _shadowBlur = value;
                _textureNeedsUpdating = true;
            }
        }

        /// <summary>
        /// Should shadow be rendered?
        /// </summary>
        public bool Shadow
        {
            get
            {
                return _shadow;
            }
            set
            {
                _shadow = value;
                _textureNeedsUpdating = true;
            }
        }

        /// <summary>
        /// Should the bounds automatically scale according to text size?
        /// </summary>
        public bool AutoSize
        {
            get
            {
                return _autoSize;
            }
            set
            {
                _autoSize = value;
                _textureNeedsUpdating = true;
            }
        }

        #endregion

        #region Fields

        /// <summary>
        /// Text to render.
        /// </summary>
        string _text = "";

        /// <summary>
        /// Rendering bounds.
        /// </summary>
        SiegeUI_Rectangle _bounds = new SiegeUI_Rectangle();

        /// <summary>
        /// Foint size (in points).
        /// </summary>
        int _fontSize = 18;

        /// <summary>
        /// Font to use.
        /// </summary>
        string _font = SiegeUI_Text_Font.SiegeUI_Text_Font_Roboto.Medium;

        /// <summary>
        /// Background color.
        /// </summary>
        SiegeUI_Color _backColor = SiegeUI_Color.Transparent;

        /// <summary>
        /// Foreground color.
        /// </summary>
        SiegeUI_Color _foreColor = SiegeUI_Color.White;

        /// <summary>
        /// Shadow color.
        /// </summary>
        SiegeUI_Color _shadowColor = new SiegeUI_Color(0x00000088);

        /// <summary>
        /// Shadow offset position.
        /// </summary>
        SiegeUI_Point _shadowOffset = new SiegeUI_Point(2, 2);

        /// <summary>
        /// Shadow blur.
        /// </summary>
        int _shadowBlur = 1;

        /// <summary>
        /// Should shadow be rendered?
        /// </summary>
        bool _shadow = true;

        /// <summary>
        /// Should the bounds automatically scale according to text size?
        /// </summary>
        bool _autoSize = true;

        /// <summary>
        /// Path to font files.
        /// </summary>
        string _fontPath = Path.Join("Resources", "Fonts");

        /// <summary>
        /// SDL_ttf font reference.
        /// </summary>
        IntPtr _ttfFont = IntPtr.Zero;

        /// <summary>
        /// Texture.
        /// </summary>
        IntPtr _sdlTexture = IntPtr.Zero;

        /// <summary>
        /// Does the texture need updating?
        /// </summary>
        bool _textureNeedsUpdating = true;

        #endregion

        #region Public methods

        /// <summary>
        /// Creates a new Text object.
        /// </summary>
        public SiegeUI_Text()
        {
        }

        /// <summary>
        /// Creates a new Text object.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="bounds"></param>
        public SiegeUI_Text(string text, SiegeUI_Rectangle bounds)
        {
            Text = text;
            Bounds = bounds;
        }

        /// <summary>
        /// Renders this text object.
        /// </summary>
        /// <param name="sdlRenderer"></param>
        public void Render(IntPtr sdlRenderer)
        {
            if (_textureNeedsUpdating)
            {
                UpdateTexture(sdlRenderer);
            }

            SDL.SDL_Rect rect = Bounds.ToSDLRect();
            SDL.SDL_RenderCopy(sdlRenderer, _sdlTexture, IntPtr.Zero, ref rect);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Loads the currently selected font.
        /// </summary>
        void LoadCurrentFont()
        {
            if (_ttfFont != IntPtr.Zero)
            {
                SDL_ttf.TTF_CloseFont(_ttfFont);
            }

            _ttfFont = SDL_ttf.TTF_OpenFont(Path.Join(_fontPath, _font), _fontSize);
        }

        /// <summary>
        /// Updates the text texture.
        /// </summary>
        /// <param name="sdlRenderer"></param>
        void UpdateTexture(IntPtr sdlRenderer)
        {
            LoadCurrentFont();
            if (_autoSize)
            {
                AutoSizeBounds();
            }

            IntPtr textSurface = BackColor == SiegeUI_Color.Transparent ? SDL_ttf.TTF_RenderText_Blended(_ttfFont, _text, _foreColor.ToSDLColor()) :
                SDL_ttf.TTF_RenderText_Shaded(_ttfFont, Text, _foreColor.ToSDLColor(), _backColor.ToSDLColor());

            _sdlTexture = _shadow ? GenerateTextureWithShadow(sdlRenderer, textSurface) : GenerateTexture(sdlRenderer, textSurface);
            _textureNeedsUpdating = false;

            SDL.SDL_FreeSurface(textSurface);
        }

        /// <summary>
        /// Generates a text texture.
        /// </summary>
        /// <param name="sdlRenderer"></param>
        /// <param name="textSurface"></param>
        /// <returns></returns>
        IntPtr GenerateTexture(IntPtr sdlRenderer, IntPtr textSurface)
        {
            return SDL.SDL_CreateTextureFromSurface(sdlRenderer, textSurface);
        }

        /// <summary>
        /// Generates a text texture with shadow (expensive).
        /// TODO: Re-write this fucking garbage...
        /// </summary>
        /// <param name="sdlRenderer"></param>
        /// <param name="textSurface"></param>
        /// <returns></returns>
        unsafe IntPtr GenerateTextureWithShadow(IntPtr sdlRenderer, IntPtr textSurface)
        {
            IntPtr shadowSurface = SDL_ttf.TTF_RenderText_Blended(_ttfFont, _text, SiegeUI_Color.Black.ToSDLColor());
            Effects.SiegeUI_Blur.Box(shadowSurface, _shadowBlur);

            SDL.SDL_Surface * shadowSurfaceRef = (SDL.SDL_Surface*)shadowSurface;
            SDL.SDL_Rect shadowRect = new SiegeUI_Rectangle(1, 1, shadowSurfaceRef->w, shadowSurfaceRef->h).ToSDLRect();

            SDL.SDL_PixelFormat* pixelFormatRef = (SDL.SDL_PixelFormat*)shadowSurfaceRef->format;
            IntPtr targetSurface = SDL.SDL_CreateRGBSurface(shadowSurfaceRef->flags,
                shadowSurfaceRef->w + (_shadowOffset.X * _shadowBlur),
                shadowSurfaceRef->h + (_shadowOffset.Y *_shadowBlur),
                pixelFormatRef->BitsPerPixel, 
                pixelFormatRef->Rmask, 
                pixelFormatRef->Gmask, 
                pixelFormatRef->Bmask, 
                pixelFormatRef->Amask);

            SDL.SDL_SetSurfaceBlendMode(targetSurface, SDL.SDL_BlendMode.SDL_BLENDMODE_BLEND);
            SDL.SDL_BlitSurface(shadowSurface, IntPtr.Zero, targetSurface, ref shadowRect);
            SDL.SDL_BlitSurface(textSurface, IntPtr.Zero, targetSurface, IntPtr.Zero);

            IntPtr targetTexture = SDL.SDL_CreateTextureFromSurface(sdlRenderer, targetSurface);

            SDL.SDL_FreeSurface(shadowSurface);
            SDL.SDL_FreeSurface(targetSurface);

            return targetTexture;
        }

        void AutoSizeBounds()
        {
            int width, height;
            SDL_ttf.TTF_SizeText(_ttfFont, Text, out width, out height);

            if (_shadow)
            {
                width += _shadowOffset.X * _shadowBlur;
                height += _shadowOffset.Y * _shadowBlur;
            }

            Bounds = new SiegeUI_Rectangle(_bounds.X, Bounds.Y, width, height);
        }

        #endregion
    }
}
