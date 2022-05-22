namespace SiegeUI.Drawing
{
    public  class SiegeUI_Point
    {
        #region Properties

        /// <summary>
        /// Rectangle X position (lop).
        /// </summary>
        public int X { get; set; } = 0;

        /// <summary>
        /// Rectangle Y position (top).
        /// </summary>
        public int Y { get; set; } = 0;

        #endregion

        #region Public methods

        /// <summary>
        /// Creates a new Point object.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public SiegeUI_Point(int x = 0, int y = 0)
        {
            X = x;
            Y = y;
        }

        #endregion
    }
}
