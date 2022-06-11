namespace SiegeUI.Drawing
{
    public  class Point
    {
        #region Properties

        /// <summary>
        /// Point X position (left).
        /// </summary>
        public int X { get; set; } = 0;

        /// <summary>
        /// Point Y position (top).
        /// </summary>
        public int Y { get; set; } = 0;

        #endregion

        #region Public methods

        /// <summary>
        /// Creates a new Point object.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Point(int x = 0, int y = 0)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"{base.ToString()} {{ X: = {X}, Y: {Y} }}";
        }

        #endregion
    }
}
