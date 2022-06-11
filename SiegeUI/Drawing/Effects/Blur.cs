using SDL2;

namespace SiegeUI.Drawing.Effects
{
    public class Blur
    {
        #region Public methods

        /// <summary>
        /// Performs box blur on a surface.
        /// Based on sample code originally written in C++:
        /// https://bacprogramming.wordpress.com/2018/01/10/box-blur-with-sdl2/
        /// </summary>
        /// <param name="sdlSurface"></param>
        /// <param name="blurAmount"></param>
        unsafe public static void Box(IntPtr sdlSurface, int blurAmount = 2)
        {
            SDL.SDL_Surface *surface = (SDL.SDL_Surface *)sdlSurface;
            for (int y = 0; y < surface->h; y++)
            {
                for (int x = 0; x < (surface->pitch / 4); x++)
                {
                    uint color = ((uint *)surface->pixels)[(y * (surface->pitch / 4)) + x];

                    //SDL_GetRGBA() is a method for getting color
                    //components from a 32 bit color
                    byte r = 0, g = 0, b = 0, a = 0;
                    SDL.SDL_GetRGBA(color, surface->format, out r, out g, out b, out a);

                    uint rb = 0, gb = 0, bb = 0, ab = 0;

                    //Within the two for-loops below, colors of adjacent pixels are added up

                    for (int yo = -blurAmount; yo <= blurAmount; yo++)
                    {
                        for (int xo = -blurAmount; xo <= blurAmount; xo++)
                        {
                            if (y + yo >= 0 && x + xo >= 0
                                && y + yo < surface->h && x + xo < (surface->pitch / 4)
                                )
                            {
                                uint colOth = ((uint *)surface->pixels)[((y + yo)
                                                        * (surface->pitch / 4)) + (x + xo)];

                                byte ro = 0, go = 0, bo = 0, ao = 0;
                                SDL.SDL_GetRGBA(colOth, surface->format, out ro, out go, out bo, out ao);

                                rb += ro;
                                gb += go;
                                bb += bo;
                                ab += ao;
                            }
                        }
                    }

                    //The sum is then, divided by the total number of
                    //pixels present in a block of blur radius

                    //For blur_extent 1, it will be 9
                    //For blur_extent 2, it will be 25
                    //and so on...

                    //In this way, we are getting the average of
                    //all the pixels in a block of blur radius

                    //(((blur_extent * 2) + 1) * ((blur_extent * 2) + 1)) calculates
                    //the total number of pixels present in a block of blur radius

                    r = (byte)(rb / (((blurAmount * 2) + 1) * ((blurAmount * 2) + 1)));
                    g = (byte)(gb / (((blurAmount * 2) + 1) * ((blurAmount * 2) + 1)));
                    b = (byte)(bb / (((blurAmount * 2) + 1) * ((blurAmount * 2) + 1)));
                    a = (byte)(ab / (((blurAmount * 2) + 1) * ((blurAmount * 2) + 1)));

                    //Bit shifting color bits to form a 32 bit proper colour
                    color = (uint)((r) | (g << 8) | (b << 16) | (a << 24)); 
                    ((uint *)surface->pixels)[(y * (surface->pitch / 4)) + x] = color;
                }
            }
        }

        #endregion
    }
}
