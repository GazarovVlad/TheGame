using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX.Direct3D;
using TheGame.ScreenGroup;
using System.Drawing;

namespace TheGame.Global.TextureSpace
{
    static class BackGround
    {
        public static void Load()
        {
            Textures.blackBackGround = new Texture(Drawing.OurDevice, CreateBlakScreen(Screen.ResolutionW, Screen.ResolutionH), Usage.None, Pool.Managed);
        }

        private static Bitmap CreateBlakScreen(int width, int height)
        {
            Bitmap bm = new Bitmap(width, height);
            for (int i = 0; i < bm.Width; i++)
            {
                for (int j = 0; j < bm.Height; j++)
                {
                    bm.SetPixel(i, j, Color.FromArgb(255, 0, 0, 0));
                }
            }
            return bm;
        }
    }
}
