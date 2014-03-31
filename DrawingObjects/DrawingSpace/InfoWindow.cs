using System;
using System.Collections.Generic;
using System.Text;
using ProgramObjects;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System.Drawing;
using UserControl.Interface;
using TheGameDrawing.TextureSpace;
using TheGameDrawing.TextRendering;
using UserControl.Menus.Elements;

namespace TheGameDrawing.DrawingSpace
{
	class InfoWindow
	{
		public static void Draw()
		{
			Drawing.OurSprite.Begin(SpriteFlags.AlphaBlend);
			Drawing.OurSprite.Draw2D(Textures.infoWindowStats, Point.Empty, 0, new Point(InformationWindow.RectStats.X, InformationWindow.RectStats.Y), Color.White);
			TextRenderer.DrawText(InformationWindow.LabStats, Color.FromArgb(20, 140, 20));
			Drawing.OurSprite.Draw2D(Textures.infoStructurePrice, Point.Empty, 0, new Point(InformationWindow.RectCurrentPrice.X, InformationWindow.RectCurrentPrice.Y), Color.White);
			TextRenderer.DrawText(InformationWindow.LabCurrentPrice, Color.FromArgb(20, 140, 20));
			if (InformationWindow.IsShow)
			{
				if (InformationWindow.IsButton())
					Drawing.OurSprite.Draw2D(Textures.infoWindowButton, Point.Empty, 0, new Point(InformationWindow.RectButton.X, InformationWindow.RectButton.Y), Color.White);
				Drawing.OurSprite.Draw2D(Textures.infoWindowIcon, Point.Empty, 0, new Point(InformationWindow.RectIcon.X, InformationWindow.RectIcon.Y), Color.White);
				Drawing.OurSprite.Draw2D(Textures.infoWindowFirstInfo, Point.Empty, 0, new Point(InformationWindow.RectFirstInfo.X, InformationWindow.RectFirstInfo.Y), Color.White);
				Drawing.OurSprite.Draw2D(Textures.infoWindowSecondInfo, Point.Empty, 0, new Point(InformationWindow.RectSecondInfo.X, InformationWindow.RectSecondInfo.Y), Color.White);
				Texture tex = null;
				List<int> ind = InformationWindow.GetTexIndSelectedObj();
				SurfaceDescription temp;
				if (InformationWindow.GetTypeSelectedObj() == "Building")
					tex = Textures.constrBuildings[ind[0]];
				if (InformationWindow.GetTypeSelectedObj() == "Mineral")
					tex = Textures.minerals[ind[0]][ind[1]][ind[2]];
				if (InformationWindow.GetTypeSelectedObj() == "Asteroid")
					tex = Textures.asteroids[ind[0]][ind[1]];
				if (InformationWindow.GetTypeSelectedObj() == "Tower")
				{
					tex = Textures.constrTowerBase[ind[0]];
					temp = tex.GetLevelDescription(0);
					Drawing.OurSprite.Draw2D(tex, Point.Empty, 0, new Point(InformationWindow.RectIcon.X + InformationWindow.RectIcon.Width / 2 - temp.Width / 2, InformationWindow.RectIcon.Y + InformationWindow.RectIcon.Height / 2 - temp.Height / 2), Color.White);
					tex = Textures.constrTowerTurrets[ind[1]];
				}
				temp = tex.GetLevelDescription(0);
				Drawing.OurSprite.Draw2D(tex, Point.Empty, 0, new Point(InformationWindow.RectIcon.X + InformationWindow.RectIcon.Width / 2 - temp.Width / 2, InformationWindow.RectIcon.Y + InformationWindow.RectIcon.Height / 2 - temp.Height / 2), Color.White);
				TextRenderer.DrawText(InformationWindow.LabFirstInfo, Color.FromArgb(130, 30, 20));
				TextRenderer.DrawText(InformationWindow.LabSecondInfo, Color.FromArgb(130, 30, 20));
			}
			Drawing.OurSprite.End();
		}
	}
}
