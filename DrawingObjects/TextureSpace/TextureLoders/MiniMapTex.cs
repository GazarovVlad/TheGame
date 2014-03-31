using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Microsoft.DirectX.Direct3D;
using ProgramObjects.ScreenGroup;
using TheGameDrawing.DrawingSpace;
using FisicalObjects;

namespace TheGameDrawing.TextureSpace.TextureLoders
{
    class MiniMapTex
    {
		private const string PathMiniMap = "Sprites\\MiniMap.png";

        public static void Load()
		{
			List<int> rads;
			rads = AIUnits.GetBuildingRads();
			CreateMiniThing(ref Textures.miniBuildings, rads, Color.FromArgb(200, 120, 210, 255));
			rads = AIUnits.GetTowerRads();
			CreateMiniThing(ref Textures.miniTowers, rads, Color.FromArgb(200, 156, 120, 255));
			rads = AIUnits.GetMiniralRads();
			CreateMiniThing(ref Textures.miniMinerals, rads, Color.FromArgb(100, 192, 255, 120));
			rads = AIUnits.GetAsteroidRads();
			CreateMiniThing(ref Textures.miniAsteroids, rads, Color.FromArgb(150, 255, 80, 80));
            Textures.miniMap = TextureLoader.FromFile(Drawing.OurDevice, PathMiniMap, MiniMap.Size, MiniMap.Size, 0, Usage.None, Format.Unknown, Pool.Default, Filter.None, Filter.None, 0);
            Textures.miniScreen = new Texture(Drawing.OurDevice, CreateMiniScreen((int)MiniMap.MiniScreenWidth, (int)MiniMap.MiniScreenHeight), Usage.None, Pool.Managed);
        }

		private static void CreateMiniThing(ref SpecialTextures tex, List<int> rads, Color color)
		{
			tex = new SpecialTextures();
			foreach (int rad in rads)
				tex.Add(rad.ToString(), new Texture(Drawing.OurDevice, CreateSphere(rad * ((MiniMap.Size*1.0)/WorkSpace.MapLen), color), Usage.None, Pool.Managed));
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
                bm.SetPixel(0, i, Color.FromArgb(150, 0, 200, 20));
                bm.SetPixel(bm.Width - 1, i, Color.FromArgb(150, 0, 200, 20));
            }
            return bm;
		}

		private static Bitmap CreateSphere(double rad, Color color)
		{
			rad++;
			int size = (int)(rad * 2);
			if (size < rad * 2)
				size++;
			Bitmap bm = new Bitmap(size, size);
			for (int i = 0; i < size; i++)
				for (int j = 0; j < size; j++)
				{
					if (rad >= Math.Sqrt((i - rad) * (i - rad) + (j - rad) * (j - rad)))
					bm.SetPixel(i, j, color);
				}
			return bm;
		}
    }
}
