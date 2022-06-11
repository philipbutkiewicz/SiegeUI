namespace SiegeUI.Drawing
{
    public class Quad
    {
        #region Properties

        /// <summary>
        /// Quad X1 position (left).
        /// </summary>
        public int Left { get; set; } = 0;

        /// <summary>
        /// Quad X2 position (right).
        /// </summary>
        public int Right { get; set; } = 0;

        /// <summary>
        /// Rectangle Y position (top).
        /// </summary>
        public int Top { get; set; } = 0;

        /// <summary>
        /// Rectangle Y2 position (bottom).
        /// </summary>
        public int Bottom { get; set; } = 0;

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

        public override string ToString()
        {
            return $"{base.ToString()} {{ Left: {Left}, Top: {Top}, Right: {Right}, Bottom: {Bottom} }}";
        }

        #endregion
    }
}
