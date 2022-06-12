using SiegeUI.Drawing;
using static SiegeUI.Drawing.Text;

namespace SiegeUI.Controls
{
    public class Label : Control
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
        /// Rendering bounds.
        /// </summary>
        public override Rectangle Bounds
        {
            get
            {
                return _text.Bounds;
            }
            set
            {
                _text.Bounds = value;
            }
        }

        /// <summary>
        /// Text padding.
        /// </summary>
        public new Quad Padding
        {
            get
            {
                return _text.Padding;
            }
            set
            {
                _text.Padding = value;
            }
        }

        /// <summary>
        /// Text alignment (AlignToBounds must be assigned).
        /// </summary>
        public TextAlign Align
        {
            get
            {
                return _text.Align;
            }
            set
            {
                _text.Align = value;
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
        /// Background color.
        /// </summary>
        public override Color BackColor
        {
            get
            {
                return _text.BackColor;
            }
            set
            {
                _text.BackColor = value;
            }
        }

        /// <summary>
        /// Foreground color.
        /// </summary>
        public override Color ForeColor
        {
            get
            {
                return _text.ForeColor;
            }
            set
            {
                _text.ForeColor = value;
            }
        }

        /// <summary>
        /// Shadow color.
        /// </summary>
        public Color ShadowColor
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
        public Point ShadowOffset
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
        public int ShadowBlur
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
        public bool Shadow
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

        /// <summary>
        /// Should the bounds automatically scale according to text size?
        /// </summary>
        public bool AutoSize
        {
            get
            {
                return _text.AutoSize;
            }
            set
            {
                _text.AutoSize = value;
            }
        }

        #endregion

        #region Fields

        /// <summary>
        /// Reference to the SiegeUI Text object.
        /// </summary>
        Text _text = new Text();

        #endregion

        #region Public methods

        public Label(Control parent) : base(parent)
        {
        }

        /// <summary>
        /// Updates the control.
        /// </summary>
        /// <param name="sdlRenderer"></param>
        public override void Update(IntPtr sdlRenderer, bool renderBackground = true)
        {
            base.Update(sdlRenderer, renderBackground);

            _text.AlignToBounds = Bounds;

            _text.Render(sdlRenderer);
        }

        #endregion
    }
}
