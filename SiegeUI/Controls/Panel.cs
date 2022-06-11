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
        #region Fields

        /// <summary>
        /// Background color.
        /// </summary>
        public new Color BackColor = Color.LightGray;

        /// <summary>
        /// Border color.
        /// </summary>
        public Color BorderColor = Color.MediumGray;

        /// <summary>
        /// Border size.
        /// </summary>
        public int BorderSize = 1;

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
            Bounds.RenderFilled(sdlRenderer, BorderColor);

            new Rectangle(Bounds.X + BorderSize, Bounds.Y + BorderSize,
                Bounds.Width - (BorderSize * 2), Bounds.Height - (BorderSize * 2)).RenderFilled(sdlRenderer, BackColor);

            base.Update(sdlRenderer, false);
        }

        #endregion
    }
}
