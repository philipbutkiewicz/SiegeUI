using SDL2;

namespace SiegeUI.Drawing
{
    public class Color
    {
        #region Static properties

        public static Color Black = new Color(0x000000ff);

        public static Color DarkGray = new Color(0x888888ff);

        public static Color MediumGray = new Color(0xaaaaaaff);

        public static Color Gray = new Color(0xccccccff);

        public static Color LightGray = new Color(0xeeeeeeff);

        public static Color White = new Color(0xffffffff);

        public static Color Transparent = new Color(0x00000000);

        #endregion

        #region Properties

        /// <summary>
        /// Red.
        /// </summary>
        public byte R = 0;

        /// <summary>
        /// Green.
        /// </summary>
        public byte G = 0;

        /// <summary>
        /// Blue.
        /// </summary>
        public byte B = 0;

        /// <summary>
        /// Alpha.
        /// </summary>
        public byte A = 0;

        #endregion

        #region Public methods

        /// <summary>
        /// Color object (from RGBA).
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <param name="a"></param>
        public Color(byte r, byte g, byte b, byte a = 255)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        /// <summary>
        /// Color object (from hex value).
        /// </summary>
        /// <param name="hex">ie. 0x000000ff = rgba(0, 0, 0, 255)</param>
        public Color(uint hex = 0xffaaccbb)
        {
            R = (byte)(hex >> 8);
            G = (byte)(hex >> 16);
            B = (byte)(hex >> 24);
            A = (byte)(hex >> 32);
        }

        /// <summary>
        /// Convert to SDL_Color.
        /// </summary>
        /// <returns></returns>
        public SDL.SDL_Color ToSDLColor()
        {
            return new SDL.SDL_Color()
            {
                a = A,
                r = R,
                g = G,
                b = B
            };
        }

        #endregion
    }
}
