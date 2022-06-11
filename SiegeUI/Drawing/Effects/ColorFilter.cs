using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiegeUI.Drawing.Effects
{
    public static class ColorFilter
    {
        #region Public methods

        /// <summary>
        /// Brightness filter.
        /// </summary>
        /// <param name="color"></param>
        /// <param name="brightness"></param>
        /// <returns></returns>
        public static Color Brightness(this Color color, float brightness = 0f)
        {
            byte colorValue = (byte)Math.Clamp(brightness * 255f, 0f, 255f);

            return new Color()
            {
                R = (byte)Math.Clamp(color.R + colorValue, 0, 255),
                G = (byte)Math.Clamp(color.G + colorValue, 0, 255),
                B = (byte)Math.Clamp(color.B + colorValue, 0, 255),
                A = (byte)Math.Clamp(color.A + colorValue, 0, 255),
            };
        }

        /// <summary>
        /// Transitions between 2 colors.
        /// TODO: This sucks. Pls rewrite it. I'm ashamed.
        /// </summary>
        /// <param name="color"></param>
        /// <param name="toColor"></param>
        /// <param name="velocity"></param>
        /// <returns></returns>
        public static Color Transition(this Color color, Color toColor, int velocity = 1)
        {
            Color newColor = new Color()
            {
                R = color.R,
                G = color.G,
                B = color.B,
                A = color.A
            };

            if (toColor.R < color.R)
            {
                newColor.R = (byte)Math.Clamp(color.R - velocity, toColor.R, color.R);
            }

            if (toColor.R > color.R)
            {
                newColor.R = (byte)Math.Clamp(color.R + velocity, color.R, toColor.R);
            }

            if (toColor.G < color.G)
            {
                newColor.G = (byte)Math.Clamp(color.G - velocity, toColor.G, color.G);
            }

            if (toColor.G > color.G)
            {
                newColor.G = (byte)Math.Clamp(color.G + velocity, color.G, toColor.G);
            }

            if (toColor.B < color.B)
            {
                newColor.B = (byte)Math.Clamp(color.B - velocity, toColor.B, color.B);
            }

            if (toColor.B > color.B)
            {
                newColor.B = (byte)Math.Clamp(color.B + velocity, color.B, toColor.B);
            }

            if (toColor.A < color.A)
            {
                newColor.A = (byte)Math.Clamp(color.A - velocity, toColor.A, color.A);
            }

            if (toColor.A > color.A)
            {
                newColor.A = (byte)Math.Clamp(color.A + velocity, color.A, toColor.A);
            }

            return newColor;
        }

        #endregion
    }
}
