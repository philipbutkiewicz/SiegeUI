using SiegeUI.Drawing;
using SiegeUI.Drawing.Effects;

namespace SiegeUI.Controls
{
    public class Button : Control
    {
        #region Properties

        /// <summary>
        /// Text to render.
        /// </summary>
        public string Text
        {
            get
            {
                return _text.Content;
            }
            set
            {
                _text.Content = value;
            }
        }

        /// <summary>
        /// Background color.
        /// </summary>
        public new Color BackColor
        { 
            get
            { 
                return _backColor; 
            }
            set 
            {
                _backColor = value; 
            } 
        }

        /// <summary>
        /// Foreground color.
        /// </summary>
        public new Color ForeColor
        {
            get
            {
                return _foreColor;
            }
            set
            {
                _foreColor = value;
            }
        }

        /// <summary>
        /// Border color.
        /// </summary>
        public Color BorderColor
        {
            get
            {
                return _borderColor;
            }
            set
            {
                _borderColor = value;
            }
        }

        /// <summary>
        /// Border size.
        /// </summary>
        public int BorderSize
        {
            get
            {
                return _borderSize;
            }
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                _borderSize = value;
            }
        }

        /// <summary>
        /// Foint size (in points).
        /// </summary>
        public int FontSize
        {
            get
            {
                return _text.FontSize;
            }
            set
            {
                if (value < 0 || value > 1024)
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                _text.FontSize = value;
            }
        }

        /// <summary>
        /// Font to use.
        /// </summary>
        public string Font
        {
            get
            {
                return _text.Font;
            }
            set
            {
                _text.Font = value;
            }
        }

        /// <summary>
        /// Shadow color.
        /// </summary>
        public Color TextShadowColor
        {
            get
            {
                return _text.ShadowColor;
            }
            set
            {
                _text.ShadowColor = value;
            }
        }

        /// <summary>
        /// Shadow offset position.
        /// </summary>
        public Point TextShadowOffset
        {
            get
            {
                return _text.ShadowOffset;
            }
            set
            {
                _text.ShadowOffset = value;
            }
        }

        /// <summary>
        /// Shadow blur.
        /// </summary>
        public int TextShadowBlur
        {
            get
            {
                return _text.ShadowBlur;
            }
            set
            {
                if (value < 0 || value > 128)
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                _text.ShadowBlur = value;
            }
        }

        /// <summary>
        /// Should shadow be rendered?
        /// </summary>
        public bool TextShadow
        {
            get
            {
                return _text.Shadow;
            }
            set
            {
                _text.Shadow = value;
            }
        }

        #endregion

        #region Fields

        /// <summary>
        /// Reference to the SiegeUI Text object.
        /// </summary>
        Text _text = new Text();

        /// <summary>
        /// Background color.
        /// </summary>
        Color _backColor = Color.Gray;

        /// <summary>
        /// Foreground color.
        /// </summary>
        Color _foreColor = Color.DarkGray;

        /// <summary>
        /// Border color.
        /// </summary>
        Color _borderColor = Color.MediumGray;

        /// <summary>
        /// Border size.
        /// </summary>
        int _borderSize = 1;

        bool _mouseEntered = false;

        bool _mouseDown = false;

        #endregion

        #region Public methods

        public Button(Control parent) : base(parent)
        {
            MouseEntered += Button_MouseEntered;
            MouseExited += Button_MouseExited;
            MouseDown += Button_MouseDown;
            MouseUp += Button_MouseUp;
        }

        /// <summary>
        /// Disposes of this object.
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();

            MouseEntered -= Button_MouseEntered;
            MouseExited -= Button_MouseExited;
            MouseDown -= Button_MouseDown;
            MouseUp -= Button_MouseUp;
        }

        /// <summary>
        /// Updates the control.
        /// </summary>
        /// <param name="renderBackground"></param>
        /// <param name="sdlRenderer"></param>
        public override void Update(IntPtr sdlRenderer, bool renderBackground = true)
        {
            base.Update(sdlRenderer, false);

            _text.Bounds = new Rectangle(Bounds.X, Bounds.Y, 0, 0);
            _text.ForeColor = _foreColor;
            _text.AlignToBounds = Bounds;
            _text.Align = Drawing.Text.TextAlign.Middle;
            _text.AutoSize = true;

            if (renderBackground)
            {
                Color borderColor = _borderColor;
                Color backColor = _backColor;

                if (_mouseEntered)
                {
                    borderColor = BorderColor.Brightness(.1f);
                    backColor = BackColor.Brightness(.1f);
                }

                if (_mouseDown)
                {
                    borderColor = BorderColor.Brightness(-.1f);
                    backColor = BackColor.Brightness(-.1f);
                }

                Bounds.RenderFilled(sdlRenderer, borderColor);

                new Rectangle(
                    Bounds.X + _borderSize,
                    Bounds.Y + _borderSize,
                    Bounds.Width - (_borderSize * 2),
                    Bounds.Height - (_borderSize * 2)
                ).RenderFilled(sdlRenderer, backColor);
            }

            _text.Render(sdlRenderer);
        }

        #endregion

        #region Private methods

        void Button_MouseUp(object? sender, EventArgs.MouseEventArgs e)
        {
            _mouseDown = false;
        }

        void Button_MouseDown(object? sender, EventArgs.MouseEventArgs e)
        {
            _mouseDown = true;
        }

        void Button_MouseExited(object? sender, System.EventArgs e)
        {
            _mouseEntered = false;
        }

        void Button_MouseEntered(object? sender, System.EventArgs e)
        {
            _mouseEntered = true;
        }

        #endregion
    }
}
