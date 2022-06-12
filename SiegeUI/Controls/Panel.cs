using SiegeUI.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiegeUI.Controls
{
    public class Panel : Control
    {
        #region Properties

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
                _borderSize = value;
            }
        }

        #endregion

        #region Fields

        Color _backColor = Color.LightGray;

        Color _borderColor = Color.MediumGray;

        int _borderSize = 1;

        #endregion

        #region Public methods

        public Panel(Control parent) : base(parent)
        {
        }

        /// <summary>
        /// Updates the control.
        /// </summary>
        public override void Update(IntPtr sdlRenderer, bool renderBackground = true)
        {
            Bounds.RenderFilled(sdlRenderer, _borderColor);

            new Rectangle(
                Bounds.X + _borderSize,
                Bounds.Y + _borderSize,
                Bounds.Width - (_borderSize * 2),
                Bounds.Height - (_borderSize * 2)
            ).RenderFilled(sdlRenderer, _backColor);

            base.Update(sdlRenderer, false);
        }

        #endregion
    }
}
