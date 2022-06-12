namespace SiegeUI.Drawing
{
    public class Quad
    {
        #region Properties

        /// <summary>
        /// Quad X1 position (left).
        /// </summary>
        public int Left
        {
            get
            {
                return _left;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Left cannot be less than 0", "value");
                }

                _left = value;
            }
        }

        /// <summary>
        /// Quad X2 position (right).
        /// </summary>
        public int Right
        {
            get
            {
                return _right;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Right cannot be less than 0", "value");
                }

                _right = value;
            }
        }

        /// <summary>
        /// Quad Y position (top).
        /// </summary>
        public int Top
        {
            get
            {
                return _top;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Top cannot be less than 0", "value");
                }

                _top = value;
            }
        }

        /// <summary>
        /// Quad Y2 position (bottom).
        /// </summary>
        public int Bottom
        {
            get
            {
                return _bottom;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Bottom cannot be less than 0", "value");
                }

                _bottom = value;
            }
        }

        #endregion

        #region Fields

        /// <summary>
        /// Quad X1 position (left).
        /// </summary>
        int _left = 0;

        /// <summary>
        /// Quad X2 position (right).
        /// </summary>
        int _right = 0;

        /// <summary>
        /// Quad Y position (top).
        /// </summary>
        int _top = 0;

        /// <summary>
        /// Quad Y2 position (bottom).
        /// </summary>
        int _bottom = 0;

        #endregion

        #region Public methods

        /// <summary>
        /// Creates a Quad object.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="right"></param>
        /// <param name="bottom"></param>
        public Quad(int left = 0, int top = 0, int right = 0, int bottom = 0)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        /// <summary>
        /// Returns a string that represents this object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{base.ToString()} {{ Left: {_left}, Top: {_top}, Right: {_right}, Bottom: {_bottom} }}";
        }

        #endregion
    }
}
