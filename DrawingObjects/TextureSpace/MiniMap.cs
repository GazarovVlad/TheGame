using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX.Direct3D;
using System.Drawing;
using TheGame.ScreenGroup;

namespace TheGame.Global.TextureSpace
{
    class MiniMapTex
    {
        private const string PathMiniMap = "Sprites\\MiniMap.png";

        public static void Load()
        {
            Textures.miniMap = TextureLoader.FromFile(Drawing.OurDevice, PathMiniMap, MiniMap.Size, MiniMap.Size, 0, Usage.None, Format.Unknown, Pool.Default, Filter.None, Filter.None, 0);
            Textures.miniScreen = new Texture(Drawing.OurDevice, CreateMiniScreen((int)MiniMap.MiniScreenWidth, (int)MiniMap.MiniScreenHeight), Usage.None, Pool.Managed);
        }

        private static Bitmap CreateMiniScreen(int width, int height)
        {
            Bitmap bm = new Bitmap(width, height);
            for (int i = 0; i < bm.Width; i++)
            {
                bm.SetPixel(i, 0, Color.FromArgb(150, 0, 200, 20));
                bm.SetPixel(i, bm.Height - 1, Color.FromArgb(150, 0, 200, 20));
            }
            for (int i = 0; i < bm.Height; i++)
            {
                bm.SetPixel(0, i, Color.FromArgb(100, 0, 200, 20));
                bm.SetPixel(bm.Width - 1, i, Color.FromArgb(150, 0, 200, 20));
            }
            return bm;
        }
    }
}
