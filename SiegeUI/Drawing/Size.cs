using SDL2;

namespace SiegeUI.Drawing
{
    public class Size
    {
        #region Properties

        /// <summary>
        /// Size width.
        /// </summary>
        public int Width
        {
            get
            {
                return _width;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Width cannot be less than 0", "value");
                }

                _width = value;
            }
        }

        /// <summary>
        /// Size height.
        /// </summary>
        public int Height
        {
            get
            {
                return _height;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Height cannot be less than 0", "value");
                }

                _height = value;
            }
        }

        #endregion

        #region Fields

        /// <summary>
        /// Rectagnle width.
        /// </summary>
        int _width = 0;

        /// <summary>
        /// Rectangle height.
        /// </summary>
        int _height = 0;

        #endregion

        #region Public methods

        /// <summary>
        /// Creates a new Size object.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Size(int width = 0, int height = 0)
        {
            Width = width;
            Height = height;
        }


        /// <summary>
        /// Returns a string that represents this object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{base.ToString()} {{ Width: {_width}, Height: {_height} }}";
        }

        #endregion
    }
}
