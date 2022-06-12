using SiegeUI.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiegeUI.Controls
{
    public class ProgressBar : Control
    {
        #region Enums

        public enum ProgressBarAlignment
        {
            Horizontal,
            Vertical
        }

        #endregion

        #region Properties

        /// <summary>
        /// Progress bar alignment.
        /// </summary>
        public ProgressBarAlignment Alignment
        {
            get
            {
                return _alignment;
            }
            set
            {
                _alignment = value;
            }
        }

        /// <summary>
        /// Progress bar value.
        /// </summary>
        public int Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (value < _min || value > _max)
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                _value = value;
            }
        }

        /// <summary>
        /// Progress bar minimum value.
        /// </summary>
        public int Min
        {
            get
            {
                return _min;
            }
            set
            {
                if (value < 0 || value > _max)
                {
                    throw new ArgumentException("Min cannot be less than zero or bigger than Max", "value");
                }

                _min = value;
            }
        }

        /// <summary>
        /// Progress bar maximum value.
        /// </summary>
        public int Max
        {
            get
            {
                return _max;
            }
            set
            {
                if (value < 0 || value < _min)
                {
                    throw new ArgumentException("Max cannot be less than zero or smaller than Min", "value");
                }

                _max = value;
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

        #endregion

        #region Fields

        /// <summary>
        /// Progress bar alignment.
        /// </summary>
        ProgressBarAlignment _alignment = ProgressBarAlignment.Horizontal;

        /// <summary>
        /// Progress bar value.
        /// </summary>
        int _value = 0;

        /// <summary>
        /// Progress bar minimum value.
        /// </summary>
        int _min = 0;

        /// <summary>
        /// Progress bar maximum value.
        /// </summary>
        int _max = 100;

        /// <summary>
        /// Background color.
        /// </summary>
        Color _backColor = Color.Gray;

        /// <summary>
        /// Foreground color.
        /// </summary>
        Color _foreColor = Color.Green;

        /// <summary>
        /// Border color.
        /// </summary>
        Color _borderColor = Color.MediumGray;

        /// <summary>
        /// Border size.
        /// </summary>
        int _borderSize = 1;

        #endregion

        #region Public methods

        public ProgressBar(Control parent) : base(parent)
        {
        }

        /// <summary>
        /// Updates the control.
        /// </summary>
        /// <param name="renderBackground"></param>
        /// <param name="sdlRenderer"></param>
        public override void Update(IntPtr sdlRenderer, bool renderBackground = true)
        {
            base.Update(sdlRenderer, false);

            Bounds.RenderFilled(sdlRenderer, _borderColor);

            if (renderBackground)
            {
                new Rectangle(
                    Bounds.X + _borderSize,
                    Bounds.Y + _borderSize,
                    Bounds.Width - (_borderSize * 2),
                    Bounds.Height - (_borderSize * 2)
                ).RenderFilled(sdlRenderer, _backColor);
            }

            float progressPercentage = _value > 0 ? (float)_value / _max : 0f;

            Rectangle foreRectangle = new Rectangle(
                Bounds.X + _borderSize,
                Bounds.Y + _borderSize,
                (int)(Bounds.Width * progressPercentage) - (_borderSize * 2), 
                Bounds.Height - (_borderSize * 2)
            );

            if (_alignment == ProgressBarAlignment.Vertical)
            {
                foreRectangle.Width = Bounds.Width - (_borderSize * 2);
                foreRectangle.Height = (int)(Bounds.Height * progressPercentage) - (_borderSize * 2);
            }
            
            foreRectangle.RenderFilled(sdlRenderer, _foreColor);
        }

        #endregion
    }
}
