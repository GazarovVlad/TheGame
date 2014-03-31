using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using TheGameDrawing.DrawingSpace;
using UserControl.Interface;
using Microsoft.DirectX.Direct3D;
using FisicalObjects;

namespace TheGameDrawing.TextureSpace.TextureLoders
{
    static class UserInterfaceTex
    {
        private const string PathRadVisibl = "Sprites\\RadVisibl.png";
        private const string PathIconBorder = "Sprites\\Constructions\\Icons\\IconBorder2.png";

        public static void Load()
        {
            Textures.radVisibl = TextureLoader.FromFile(Drawing.OurDevice, PathRadVisibl, ConstrPanelControl.TextureRadVisiblSize, ConstrPanelControl.TextureRadVisiblSize, 0, Usage.None, Format.Unknown, Pool.Default, Filter.None, Filter.None, 0);
            Textures.iconBorder = TextureLoader.FromFile(Drawing.OurDevice, PathIconBorder, ConstrPanelControl.SlotSize, ConstrPanelControl.SlotSize, 0, Usage.None, Format.Unknown, Pool.Default, Filter.None, Filter.None, 0);
            CreateFireRangs();
		}

		public static Bitmap CreateRing(int rad, Color color)
		{
			Bitmap bm = new Bitmap(rad * 2, rad * 2);
			int y = 0;
			for (double x = 0; x <= rad; x += 0.5)
			{
				y = (int)Math.Sqrt(rad * rad - x * x);
				bm.SetPixel((int)x + rad - 1, y + rad - 1, color);
				bm.SetPixel((int)x + rad - 1, -y + rad, color);
				bm.SetPixel((int)-x + rad, y + rad - 1, color);
				bm.SetPixel((int)-x + rad, -y + rad, color);
				bm.SetPixel(y + rad - 1, (int)x + rad - 1, color);
				bm.SetPixel(-y + rad, (int)x + rad - 1, color);
				bm.SetPixel(y + rad - 1, (int)-x + rad, color);
				bm.SetPixel(-y + rad, (int)-x + rad, color);
			}
			return bm;
		}

        private static void CreateFireRangs()
        {
            List<int> rangs = AIUnits.GetFireRangs();
            Textures.radAttack = new SpecialTextures();
            for (int i = 0; i < rangs.Count; i++)
            {
				Texture tex = new Texture(Drawing.OurDevice, CreateRing(rangs[i], Color.FromArgb(150, 250, 50, 50)), Usage.None, Pool.Managed);
                Textures.radAttack.Add(rangs[i].ToString(), tex);
            }
        }
    }
}
