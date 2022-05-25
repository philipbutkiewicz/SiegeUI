using SDL2;

namespace SiegeUI.Drawing
{
    public class SiegeUI_Color
    {
        #region Static properties

        public static SiegeUI_Color Black = new SiegeUI_Color(0x000000ff);

        public static SiegeUI_Color DarkGray = new SiegeUI_Color(0x999999ff);

        public static SiegeUI_Color Gray = new SiegeUI_Color(0xccccccff);

        public static SiegeUI_Color White = new SiegeUI_Color(0xffffffff);

        public static SiegeUI_Color Transparent = new SiegeUI_Color(0x00000000);

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
        public SiegeUI_Color(byte r, byte g, byte b, byte a = 255)
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
        public SiegeUI_Color(uint hex = 0xffaaccbb)
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
