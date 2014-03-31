using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System.Drawing;
using UserControl.Interface;
using TheGameDrawing.DrawingSpace;
using FisicalObjects;

namespace TheGameDrawing.TextureSpace.TextureLoders
{
	class InfoWindowTex
	{
		private const int RadModifed = 4;
		private const string ButtonDestroyPath = "Sprites\\WorkSpace\\DestroyButton.png";

		public static void Load()
		{
			CreateRads();
			Textures.infoWindowButton = TextureLoader.FromFile(Drawing.OurDevice, ButtonDestroyPath, InformationWindow.RectButton.Width, InformationWindow.RectButton.Height, 0, Usage.None, Format.Unknown, Pool.Default, Filter.None, Filter.None, 0);
			Textures.infoWindowStats = new Texture(Drawing.OurDevice, Create(InformationWindow.RectStats.Width, InformationWindow.RectStats.Height, Color.FromArgb(50,250,50,50)), Usage.None, Pool.Managed);
			Textures.infoWindowFirstInfo = new Texture(Drawing.OurDevice, Create(InformationWindow.RectFirstInfo.Width, InformationWindow.RectFirstInfo.Height, Color.FromArgb(40, 50, 200, 50)), Usage.None, Pool.Managed);
			Textures.infoWindowSecondInfo = new Texture(Drawing.OurDevice, Create(InformationWindow.RectSecondInfo.Width, InformationWindow.RectSecondInfo.Height, Color.FromArgb(40, 50, 200, 50)), Usage.None, Pool.Managed);
			Textures.infoWindowIcon = new Texture(Drawing.OurDevice, Create(InformationWindow.RectIcon.Width, InformationWindow.RectIcon.Height, Color.FromArgb(50, 30, 40, 200)), Usage.None, Pool.Managed);
			Textures.infoStructurePrice = new Texture(Drawing.OurDevice, Create(InformationWindow.RectCurrentPrice.Width, InformationWindow.RectCurrentPrice.Height, Color.FromArgb(50, 250, 50, 50)), Usage.None, Pool.Managed);
		}

		private static void CreateRads()
		{
			Textures.infoRads = new SpecialTextures();
			List<int> rads = AIUnits.GetBuildingRads();
			rads.AddRange(AIUnits.GetMiniralRads());
			rads.AddRange(AIUnits.GetTowerRads());
			rads.AddRange(AIUnits.GetAsteroidRads());
			foreach (int rad in rads)
			{
				Textures.infoRads.Add(rad.ToString(), new Texture(Drawing.OurDevice, UserInterfaceTex.CreateRing(rad + RadModifed, Color.FromArgb(220, 255, 255, 255)), Usage.None, Pool.Managed));
			}
		}

		private static Bitmap Create(int w, int h, Color c)
		{
			Bitmap bm = new Bitmap(w, h);
			for (int i = 0; i < w; i++)
				for (int j = 0; j < h; j++)
				{
					bm.SetPixel(i, j, c);
				}
			return bm;
		}
	}
}
