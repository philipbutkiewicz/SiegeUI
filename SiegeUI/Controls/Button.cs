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
        public new Color BackColor = Color.Gray;

        /// <summary>
        /// Foreground color.
        /// </summary>
        public new Color ForeColor = Color.DarkGray;

        /// <summary>
        /// Border color.
        /// </summary>
        public Color BorderColor = Color.MediumGray;

        /// <summary>
        /// Border size.
        /// </summary>
        public int BorderSize = 1;

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

        ~Button()
        {
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
            _text.ForeColor = ForeColor;
            _text.AlignToBounds = Bounds;
            _text.Align = Drawing.Text.TextAlign.Middle;
            _text.AutoSize = true;

            if (renderBackground)
            {
                Color borderColor = BorderColor;
                Color backColor = BackColor;

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

                new Rectangle(Bounds.X + BorderSize, Bounds.Y + BorderSize,
                    Bounds.Width - (BorderSize * 2), Bounds.Height - (BorderSize * 2)).RenderFilled(sdlRenderer, backColor);
            }

            _text.Render(sdlRenderer);
        }

        #endregion

        #region Private methods

        void Button_MouseUp(object? sender, EventArgs.SiegeUI_MouseEventArgs e)
        {
            _mouseDown = false;
        }

        void Button_MouseDown(object? sender, EventArgs.SiegeUI_MouseEventArgs e)
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
