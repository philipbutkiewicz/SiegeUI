using SDL2;

namespace SiegeUI.Drawing
{
    public class SiegeUI_Rectangle
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

        /// <summary>
        /// Rectagnle width.
        /// </summary>
        public int Width { get; set; } = 0;

        /// <summary>
        /// Rectangle height.
        /// </summary>
        public int Height { get; set; } = 0;

        #endregion

        #region Public methods

        /// <summary>
        /// Creates a new Rectangle object.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public SiegeUI_Rectangle(int x = 0, int y = 0, int width = 0, int height = 0)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Renders this rectangle
        /// </summary>
        /// <param name="_sdlRenderer"></param>
        /// <param name="color"></param>
        public void Render(IntPtr _sdlRenderer, SiegeUI_Color color)
        {
            byte _r, _g, _b, _a;
            SDL.SDL_GetRenderDrawColor(_sdlRenderer, out _r, out _g, out _b, out _a);
            SDL.SDL_SetRenderDrawColor(_sdlRenderer, color.R, color.G, color.B, color.A);

            SDL.SDL_Rect rect = ToSDLRect();
            SDL.SDL_RenderDrawRect(_sdlRenderer, ref rect);

            SDL.SDL_SetRenderDrawColor(_sdlRenderer, _r, _g, _b, _a);
        }

        /// <summary>
        /// Renders this rectangle (filled).
        /// </summary>
        /// <param name="_sdlRenderer"></param>
        /// <param name="color"></param>
        public void RenderFilled(IntPtr _sdlRenderer, SiegeUI_Color color)
        {
            byte _r, _g, _b, _a;
            SDL.SDL_GetRenderDrawColor(_sdlRenderer, out _r, out _g, out _b, out _a);
            SDL.SDL_SetRenderDrawColor(_sdlRenderer, color.R, color.G, color.B, color.A);

            SDL.SDL_Rect rect = ToSDLRect();
            SDL.SDL_RenderFillRect(_sdlRenderer, ref rect);

            SDL.SDL_SetRenderDrawColor(_sdlRenderer, _r, _g, _b, _a);
        }

        /// <summary>
        /// Returns SDL_Rect
        /// </summary>
        /// <returns></returns>
        public SDL.SDL_Rect ToSDLRect()
        {
            SDL.SDL_Rect rect = new SDL.SDL_Rect();
            rect.x = X;
            rect.y = Y;
            rect.w = Width;
            rect.h = Height;

            return rect;
        }

        #endregion
    }
}
