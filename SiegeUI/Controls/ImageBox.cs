using SiegeUI.Drawing;

namespace SiegeUI.Controls
{
    public class ImageBox : Control
    {

        #region Properties

        /// <summary>
        /// Image associated with this ImageBox.
        /// </summary>
        public Image Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
            }
        }

        /// <summary>
        /// Rendering bounds.
        /// </summary>
        public override Rectangle Bounds
        {
            get
            {
                return _image.Bounds;
            }
            set
            {
                _image.Bounds = value;
            }
        }

        #endregion

        #region Fields

        /// <summary>
        /// Image associated with this ImageBox.
        /// </summary>
        private Image _image = new Image();

        #endregion

        #region Public methods

        public ImageBox(Control parent) : base(parent)
        {
            Docking = DockingMode.None;
        }

        /// <summary>
        /// Updates the control.
        /// </summary>
        /// <param name="renderBackground"></param>
        /// <param name="sdlRenderer"></param>
        public override void Update(IntPtr sdlRenderer, bool renderBackground = true)
        {
            base.Update(sdlRenderer, false);

            _image.Render();
        }

        #endregion
    }
}