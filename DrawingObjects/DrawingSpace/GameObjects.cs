using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using GraphicObjects.ExplosionsSpace;
using ProgramObjects.ScreenGroup;
using TheGameDrawing.TextureSpace;
using SupportedStructures;
using FisicalObjects;
using FisicalObjects.Transist;
using UserControl.Interface;

namespace TheGameDrawing.DrawingSpace
{
    class GameObjects
    {
        private static Vector3 Pos;
        private static Rectangle Rect;
        private static Matrix Tr;
        private static SurfaceDescription Temp;

        public static void Draw()
        {
            DrawMinerals();
            DrawWays();
            DrawBuildings();
            DrawTowers();
            DrawAsteroids();
			DrawUISelectedObj();
			DrawFire();
            DrawExplosions();
        }

		public static void DrawUISelectedObj()
		{
			if (InformationWindow.IsShow)
			{
				Drawing.OurSprite.Begin(SpriteFlags.AlphaBlend);
				Temp = Textures.infoRads.Get(InformationWindow.GetRadSelectedObj()).GetLevelDescription(0);
				Point pos = InformationWindow.GetPosSelectedObj();
				Drawing.OurSprite.Draw2D(Textures.infoRads.Get(InformationWindow.GetRadSelectedObj()), Point.Empty, 0, new Point(pos.X - WorkSpace.Space.X - Temp.Width / 2 + WorkSpace.DX, pos.Y - WorkSpace.Space.Y - Temp.Height / 2 + WorkSpace.DY), Color.White);
				if ((InformationWindow.GetTypeSelectedObj() == "Building") || (InformationWindow.GetTypeSelectedObj() == "Tower"))
				{
					Drawing.OurSprite.Draw2D(Textures.radVisibl, Point.Empty, 0, new Point(pos.X - WorkSpace.Space.X - ConstrPanelControl.TextureRadVisiblSize / 2, pos.Y - WorkSpace.Space.Y - ConstrPanelControl.TextureRadVisiblSize / 2), Color.White);
					if (InformationWindow.GetFireRadSelectedObj() != "")
					{
						string frad = InformationWindow.GetFireRadSelectedObj();
						Temp = Textures.radAttack.Get(frad).GetLevelDescription(0);
						Drawing.OurSprite.Draw2D(Textures.radAttack.Get(frad), Point.Empty, 0, new Point(pos.X - WorkSpace.Space.X - Temp.Width / 2, pos.Y - WorkSpace.Space.Y - Temp.Height / 2), Color.White);
					}
				}
				Drawing.OurSprite.End();
			}
		}

        private static void DrawBuildings()
        {
            TransBuilding[] buidings = AIUnits.GetTransBuildings();
            Drawing.OurSprite.Begin(SpriteFlags.AlphaBlend);
            foreach (TransBuilding building in buidings)
            {
				Temp = Textures.constrBuildings[building.TBuildingIndex].GetLevelDescription(0);
				Pos = new Vector3(building.Pozition.X - WorkSpace.Space.X - Temp.Width / 2.0f + WorkSpace.DX, building.Pozition.Y - WorkSpace.Space.Y - Temp.Height / 2.0f + WorkSpace.DY, 1);
                Rect = new Rectangle(0, 0, Temp.Width, Temp.Height);
                Tr = Matrix.Translation(Pos.X + Temp.Width / 2.0f, Pos.Y + Temp.Height / 2.0f, 1);
				Drawing.OurSprite.Transform = Matrix.Invert(Tr) * Matrix.RotationZ(building.Angle) * Tr;
				Drawing.OurSprite.Draw(Textures.constrBuildings[building.TBuildingIndex], Rect, new Vector3(0, 0, 0), Pos, Color.White);
            }
            Drawing.OurSprite.End();
        }

        private static void DrawTowers()
        {
            TransTower[] towers = AIUnits.GetTransTowers();
            Drawing.OurSprite.Begin(SpriteFlags.AlphaBlend);
            foreach(TransTower tower in towers)
            {
				// Base
				Temp = Textures.constrTowerBase[tower.TBaseIndex].GetLevelDescription(0);
				Pos = new Vector3(tower.Pozition.X - WorkSpace.Space.X - Temp.Width / 2.0f + WorkSpace.DX, tower.Pozition.Y - WorkSpace.Space.Y - Temp.Height / 2.0f + WorkSpace.DY, 1);
                Rect = new Rectangle(0, 0, Temp.Width, Temp.Height);
                Tr = Matrix.Translation(Pos.X + Temp.Width / 2.0f, Pos.Y + Temp.Height / 2.0f, 1);
				Drawing.OurSprite.Transform = Matrix.Invert(Tr) * Matrix.RotationZ(tower.Angle) * Tr;
				Drawing.OurSprite.Draw(Textures.constrTowerBase[tower.TBaseIndex], Rect, new Vector3(0, 0, 0), Pos, Color.White);
				
				// Turret
				Temp = Textures.constrTowerTurrets[tower.TTurretIndex].GetLevelDescription(0);
				Pos = new Vector3(tower.Pozition.X - WorkSpace.Space.X - Temp.Width / 2.0f + WorkSpace.DX, tower.Pozition.Y - WorkSpace.Space.Y - Temp.Height / 2.0f + WorkSpace.DY, 1);
                Rect = new Rectangle(0, 0, Temp.Width, Temp.Height);
                Tr = Matrix.Translation(Pos.X + Temp.Width / 2.0f, Pos.Y + Temp.Height / 2.0f, 1);
				Drawing.OurSprite.Transform = Matrix.Invert(Tr) * Matrix.RotationZ(tower.TurretAngle) * Tr;
				Drawing.OurSprite.Draw(Textures.constrTowerTurrets[tower.TTurretIndex], Rect, new Vector3(0, 0, 0), Pos, Color.White);
            }
            Drawing.OurSprite.End();
        }

        private static void DrawWays()
        {
            TransWay[] ways = AIUnits.GetTransWays();
            CustomVertex.TransformedColored[] lines = new CustomVertex.TransformedColored[ways.Length * 2];
            for (int i = 0; i < ways.Length; i++)
            {
                lines[2 * i].Position = new Microsoft.DirectX.Vector4(ways[i].A.X - WorkSpace.Space.X + WorkSpace.DX, ways[i].A.Y - WorkSpace.Space.Y + WorkSpace.DY, 1, 1);
                lines[2 * i].Color = ways[i].Coloring.ToArgb();
                lines[2 * i + 1].Position = new Microsoft.DirectX.Vector4(ways[i].B.X - WorkSpace.Space.X + WorkSpace.DX, ways[i].B.Y - WorkSpace.Space.Y + WorkSpace.DY, 1, 1);
                lines[2 * i + 1].Color = ways[i].Coloring.ToArgb();
            }
            Drawing.OurSprite.Begin(SpriteFlags.AlphaBlend);
            Drawing.OurDevice.VertexFormat = CustomVertex.TransformedColored.Format;
            if (lines.Length > 0)
                Drawing.OurDevice.DrawUserPrimitives(PrimitiveType.LineList, lines.Length / 2, lines);
            Drawing.OurSprite.End();
        }

		private static void DrawFire()
		{
			TransFire[] data = AIUnits.GetTransFire();
			Drawing.OurSprite.Begin(SpriteFlags.AlphaBlend);
			foreach(TransFire fire in data)
			{
				Point pos = new Point(fire.RodPosition.X - WorkSpace.Space.X + WorkSpace.DX, fire.RodPosition.Y - WorkSpace.Space.Y + WorkSpace.DY);
				Point targ = new Point(fire.Target.X - WorkSpace.Space.X + WorkSpace.DX, fire.Target.Y - WorkSpace.Space.Y + WorkSpace.DY);
				Firing.DrawFire(pos, targ, fire);
			}
			Drawing.OurSprite.End();
		}

        private static void DrawExplosions()
        {
            Drawing.OurSprite.Begin(SpriteFlags.AlphaBlend);
            for (int i = 0; i < Explosions.AllExplosions.Count; i++)
            {
                Temp = Textures.allExplosions[Explosions.AllExplosions[i].Type][Explosions.AllExplosions[i].GetStageType()][Explosions.AllExplosions[i].Stage - 1].GetLevelDescription(0);
                Pos = new Vector3(Explosions.AllExplosions[i].X - WorkSpace.Space.X - Temp.Width / 2.0f + WorkSpace.DX, Explosions.AllExplosions[i].Y - WorkSpace.Space.Y - Temp.Height / 2.0f + WorkSpace.DY, 1);
                Rect = new Rectangle(0, 0, Temp.Width, Temp.Height);
                Tr = Matrix.Translation(Pos.X + Temp.Width / 2.0f, Pos.Y + Temp.Height / 2.0f, 1);
                Drawing.OurSprite.Transform = Matrix.Invert(Tr) * Matrix.RotationZ(Explosions.AllExplosions[i].Angle) * Tr;
                Drawing.OurSprite.Draw(Textures.allExplosions[Explosions.AllExplosions[i].Type][Explosions.AllExplosions[i].GetStageType()][Explosions.AllExplosions[i].Stage - 1], Rect, new Vector3(0, 0, 0), Pos, Color.White);
            }
            Drawing.OurSprite.End();
        }

        private static void DrawMinerals()
        {
            TransMineral[] minerals = AIUnits.GetTransMinerals();
            Drawing.OurSprite.Begin(SpriteFlags.AlphaBlend);
            foreach(TransMineral mineral in minerals)
            {
				Temp = Textures.minerals[mineral.T1][mineral.T2][mineral.T3].GetLevelDescription(0);
				Pos = new Vector3(mineral.Pozition.X - WorkSpace.Space.X - Temp.Width / 2 + WorkSpace.DX, mineral.Pozition.Y - WorkSpace.Space.Y - Temp.Height / 2 + WorkSpace.DY, 1);
                Rect = new Rectangle(0, 0, Temp.Width, Temp.Height);
                Tr = Matrix.Translation(Pos.X + Temp.Width / 2.0f, Pos.Y + Temp.Height / 2.0f, 1);
				Drawing.OurSprite.Transform = Matrix.Invert(Tr) * Matrix.RotationZ(mineral.Angle) * Tr;
				Drawing.OurSprite.Draw(Textures.minerals[mineral.T1][mineral.T2][mineral.T3], Rect, new Vector3(0, 0, 0), Pos, Color.White);
            }
            Drawing.OurSprite.End();
        }

        private static void DrawAsteroids()
        {
            TransAsteroid[] asters = AIUnits.GetTransAsteroids();
            Drawing.OurSprite.Begin(SpriteFlags.AlphaBlend);
            foreach(TransAsteroid aster in asters)
            {
				Temp = Textures.asteroids[aster.Model.X][aster.Model.Y].GetLevelDescription(0);
				Pos = new Vector3(aster.Pozition.X - WorkSpace.Space.X - Temp.Width / 2 + WorkSpace.DX, aster.Pozition.Y - WorkSpace.Space.Y - Temp.Height / 2 + WorkSpace.DY, 1);
                Rect = new Rectangle(0, 0, Temp.Width, Temp.Height);
                Tr = Matrix.Translation(Pos.X + Temp.Width / 2.0f, Pos.Y + Temp.Height / 2.0f, 1);
				Drawing.OurSprite.Transform = Matrix.Invert(Tr) * Matrix.RotationZ(aster.Angle) * Tr;
				Drawing.OurSprite.Draw(Textures.asteroids[aster.Model.X][aster.Model.Y], Rect, new Vector3(0, 0, 0), Pos, Color.White);
            }
            Drawing.OurSprite.End();
        }
    }
}
