using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using GraphicObjects.StarsSpace;
using ProgramObjects.ScreenGroup;
using TheGameDrawing.TextureSpace;
using FisicalObjects;
using FisicalObjects.Transist;

namespace TheGameDrawing.DrawingSpace
{
    static class BackGround
    {
        public static void DrawBlack()
        {
            Drawing.OurSprite.Begin(SpriteFlags.None);
            Drawing.OurSprite.Draw2D(Textures.blackBackGround, Point.Empty, 0, Point.Empty, Color.White);
            Drawing.OurSprite.End();
        }

        public static void DrawStars()
        {
            Drawing.OurSprite.Begin(SpriteFlags.AlphaBlend);
            for (int j = Stars.AllStars.Length - 1; j >= 0; j--)
            {
                for (int i = 0; i < Stars.AllStars[j].Count; i++)
                {
                    Drawing.OurSprite.Draw2D(Textures.allStars[j][Stars.AllStars[j][i].I], Point.Empty, 0, new Point((int)Stars.AllStars[j][i].X + WorkSpace.DX, (int)Stars.AllStars[j][i].Y + WorkSpace.DY), Color.White);
                }
            }
            Drawing.OurSprite.End();
        }

        public static void DrawMiniMap()
        {
            Drawing.OurSprite.Begin(SpriteFlags.AlphaBlend);
            Drawing.OurSprite.Draw2D(Textures.miniMap, Point.Empty, 0, new Point(MiniMap.DX, MiniMap.DY), Color.White);
			if (AIUnits.WorldExist)
			{
				DrawMiniMinirals();
				DrawMiniBuildings();
				DrawMiniTowers();
				DrawMiniAsteroids();
			}
            Drawing.OurSprite.End();
        }

        public static void DrawMiniMapIndicator()
        {
            Drawing.OurSprite.Begin(SpriteFlags.AlphaBlend);
            Drawing.OurSprite.Draw2D(Textures.miniScreen, Point.Empty, 0, new Point((int)(MiniMap.MiniScreenX + MiniMap.DX), (int)(MiniMap.MiniScreenY + MiniMap.DY)), Color.White);
            Drawing.OurSprite.End();
        }

        public static void DrawWorkSpace()
        {
            Drawing.OurSprite.Begin(SpriteFlags.AlphaBlend);
            Drawing.OurSprite.Draw2D(Textures.workSpMiniMapBorder, Point.Empty, 0, new Point(WorkSpace.MiniMapBorder.X,WorkSpace.MiniMapBorder.Y), Color.White);
            Drawing.OurSprite.Draw2D(Textures.workSpDownBorder, Point.Empty, 0, new Point(WorkSpace.DownBorder.X, WorkSpace.DownBorder.Y), Color.White);
            Drawing.OurSprite.Draw2D(Textures.workSpRightBorder, Point.Empty, 0, new Point(WorkSpace.RightBorder.X, WorkSpace.RightBorder.Y), Color.White);
            Drawing.OurSprite.Draw2D(Textures.workSpLeftBorder, Point.Empty, 0, new Point(WorkSpace.LeftBorder.X, WorkSpace.LeftBorder.Y), Color.White);
            Drawing.OurSprite.Draw2D(Textures.workSpUpBorder, Point.Empty, 0, new Point(WorkSpace.UpBorder.X, WorkSpace.UpBorder.Y), Color.White);
            Drawing.OurSprite.End();
		}

		private static void DrawMiniBuildings()
		{
			TransBuilding[] buidings = AIUnits.GetTransBuildings();
			for (int i = 0; i < buidings.Length; i++)
			{
				SurfaceDescription temp = Textures.miniBuildings.Get(buidings[i].Radius.ToString()).GetLevelDescription(0);
				int x = (int)(MiniMap.Size * ((buidings[i].Pozition.X * 1.0) / WorkSpace.MapLen) + MiniMap.DX);
				int y = (int)(MiniMap.Size * ((buidings[i].Pozition.Y * 1.0) / WorkSpace.MapLen) + MiniMap.DY);
				Drawing.OurSprite.Draw2D(Textures.miniBuildings.Get(buidings[i].Radius.ToString()), Point.Empty, 0, new Point(x, y), Color.White);
			}
		}

		private static void DrawMiniTowers()
		{
			TransTower[] towers = AIUnits.GetTransTowers();
			for (int i = 0; i < towers.Length; i++)
			{
				SurfaceDescription temp = Textures.miniTowers.Get(towers[i].Radius.ToString()).GetLevelDescription(0);
				int x = (int)(MiniMap.Size * ((towers[i].Pozition.X * 1.0) / WorkSpace.MapLen) + MiniMap.DX);
				int y = (int)(MiniMap.Size * ((towers[i].Pozition.Y * 1.0) / WorkSpace.MapLen) + MiniMap.DY);
				Drawing.OurSprite.Draw2D(Textures.miniTowers.Get(towers[i].Radius.ToString()), Point.Empty, 0, new Point(x, y), Color.White);
			}
		}

		private static void DrawMiniMinirals()
		{
			TransMineral[] minerals = AIUnits.GetTransMinerals();
			for (int i = 0; i < minerals.Length; i++)
			{
				SurfaceDescription temp = Textures.miniMinerals.Get(minerals[i].Radius.ToString()).GetLevelDescription(0);
				int x = (int)(MiniMap.Size * ((minerals[i].Pozition.X * 1.0) / WorkSpace.MapLen) + MiniMap.DX);
				int y = (int)(MiniMap.Size * ((minerals[i].Pozition.Y * 1.0) / WorkSpace.MapLen) + MiniMap.DY);
				Drawing.OurSprite.Draw2D(Textures.miniMinerals.Get(minerals[i].Radius.ToString()), Point.Empty, 0, new Point(x, y), Color.White);
			}
		}

		private static void DrawMiniAsteroids()
		{
			TransAsteroid[] asteroids = AIUnits.GetTransAsteroids();
			for (int i = 0; i < asteroids.Length; i++)
			{
				SurfaceDescription temp = Textures.miniAsteroids.Get(asteroids[i].Radius.ToString()).GetLevelDescription(0);
				int x = (int)(MiniMap.Size * ((asteroids[i].Pozition.X * 1.0) / WorkSpace.MapLen) + MiniMap.DX);
				int y = (int)(MiniMap.Size * ((asteroids[i].Pozition.Y * 1.0) / WorkSpace.MapLen) + MiniMap.DY);
				Drawing.OurSprite.Draw2D(Textures.miniAsteroids.Get(asteroids[i].Radius.ToString()), Point.Empty, 0, new Point(x, y), Color.White);
			}
		}
    }
}
