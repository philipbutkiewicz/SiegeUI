using SiegeUI.Drawing;

namespace SiegeUI.Controls
{
    public class SiegeUI_Label : SiegeUI_Control
    {
        #region Properties

        /// <summary>
        /// Text to render.
        /// </summary>
        public string Text
        {
            get
            {
                return _siegeUI_Text.Text;
            }
            set
            {
                _siegeUI_Text.Text = value;
            }
        }

        /// <summary>
        /// Rendering bounds.
        /// </summary>
        public override SiegeUI_Rectangle Bounds
        {
            get
            {
                return _siegeUI_Text.Bounds;
            }
            set
            {
                _siegeUI_Text.Bounds = value;
            }
        }


        /// <summary>
        /// Foint size (in points).
        /// </summary>
        public int FontSize
        {
            get
            {
                return _siegeUI_Text.FontSize;
            }
            set
            {
                _siegeUI_Text.FontSize = value;
            }
        }

        /// <summary>
        /// Font to use.
        /// </summary>
        public string Font
        {
            get
            {
                return _siegeUI_Text.Font;
            }
            set
            {
                _siegeUI_Text.Font = value;
            }
        }

        /// <summary>
        /// Background color.
        /// </summary>
        public override SiegeUI_Color BackColor
        {
            get
            {
                return _siegeUI_Text.BackColor;
            }
            set
            {
                _siegeUI_Text.BackColor = value;
            }
        }

        /// <summary>
        /// Foreground color.
        /// </summary>
        public override SiegeUI_Color ForeColor
        {
            get
            {
                return _siegeUI_Text.ForeColor;
            }
            set
            {
                _siegeUI_Text.ForeColor = value;
            }
        }

        /// <summary>
        /// Shadow color.
        /// </summary>
        public SiegeUI_Color ShadowColor
        {
            get
            {
                return _siegeUI_Text.ShadowColor;
            }
            set
            {
                _siegeUI_Text.ShadowColor = value;
            }
        }

        /// <summary>
        /// Shadow offset position.
        /// </summary>
        public SiegeUI_Point ShadowOffset
        {
            get
            {
                return _siegeUI_Text.ShadowOffset;
            }
            set
            {
                _siegeUI_Text.ShadowOffset = value;
            }
        }

        /// <summary>
        /// Shadow blur.
        /// </summary>
        public int ShadowBlur
        {
            get
            {
                return _siegeUI_Text.ShadowBlur;
            }
            set
            {
                _siegeUI_Text.ShadowBlur = value;
            }
        }

        /// <summary>
        /// Should shadow be rendered?
        /// </summary>
        public bool Shadow
        {
            get
            {
                return _siegeUI_Text.Shadow;
            }
            set
            {
                _siegeUI_Text.Shadow = value;
            }
        }

        #endregion

        #region Fields

        /// <summary>
        /// Reference to the SiegeUI Text object.
        /// </summary>
        SiegeUI_Text _siegeUI_Text = new SiegeUI_Text();

        #endregion

        #region Public methods

        /// <summary>
        /// Updates the control.
        /// </summary>
        /// <param name="sdlRenderer"></param>
        public override void Update(IntPtr sdlRenderer)
        {
            _siegeUI_Text.Render(sdlRenderer);
        }

        #endregion
    }
}
