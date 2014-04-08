using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FisicalObjects.Cosmos.Aliens;
using FisicalObjects.Cosmos.Aliens.Base;
using FisicalObjects.Cosmos.Aliens.Descendants;
using FisicalObjects.Transist;
using GraphicObjects.ExplosionsSpace;

namespace FisicalObjects.Cosmos.Aliens
{
	static class AliensField
	{
		public static Point Null = new Point(-1000, -1000);
        public static List<IAlien> Aliens;

		private const double AttackCoef = 0.8;

        private const int MinAlienDensity = 5;
        private const int MaxAlienDensity = 23;
        private static int WorldSize;
        private static Cell[][] Field;
        private static IAlien Ref;
        private static Random Rand = new Random();
        private static int Degress = 400;
        private static int MaxRadius;
		/*
		private static int AsterMassDestroed;

		private static int Start;
		private static int End;
		private static IAsteroid RefAster;   
        */

		public static void Inicialize(int worldsize)
		{
			WorldSize = worldsize;
            SimpleAlien.Inicialize(worldsize);
			Cell.Inicialize(worldsize);
			MaxRadius = AlienAvailible.GetMaxRadius();
            AlienAvailible.Inicialize(new Point(WorldSize / 2, WorldSize / 2));
		}

		public static void CreateWorld(int worldrad)
		{
            Ref = null;
            Aliens = new List<IAlien>();
			Field = new Cell[Cell.MaxInd.X + 1][];
			for (int i = 0; i < Field.Length; i++)
			{
				Field[i] = new Cell[Cell.MaxInd.Y + 1];
				for (int j = 0; j < Field[i].Length; j++)
					Field[i][j] = new Cell(new Point(i, j));
			}
		}

		public static void DestroyWorld()
		{
            Aliens.Clear();
			ClearCells();
		}

        public static void SkipRef()
        {
            Ref = null;
        }

        public static TransInfo CheckRef()
        {
            if (Ref == null)
                return null;
            return Ref.GetInfo();
        }

		public static void Prepare()
		{
			Point ind;
			int i = 0;
            ClearCells();		
            while (i < Aliens.Count)
            {
                while ((i < Aliens.Count) && (!CheckDistance(Aliens[i].GetCoords())))
                {
                    if (Ref != null)
                    {
                        if (Ref.Equals(Aliens[i]))
                            Ref = null;
                    }
                    Aliens.RemoveAt(i);
                }
                if (i < Aliens.Count)
                {
                    ind = Cell.GetCellIndex(Aliens[i].GetCoords());
                    Field[ind.X][ind.Y].AddAlienLink(i);
                }
                i++;
            }
		}

		public static void Process() 
		{		
			//	движение
            for (int i = 0; i < Aliens.Count; i++)
                Aliens[i].Move();
		}

		public static Point Attack(Point pos, int rang, int demage, int fmass, string hiteffect)
		{
			List<Point> mean = new List<Point>();
			Point pos2;
			double r2, length, tlength;
			int ind = -1;
			length = -1;
			mean = Cell.GetMeaningfulCells(pos, rang + MaxRadius);
			for (int j = 0; j < mean.Count; j++)
			{
				for (int k = 0; k < Field[mean[j].X][mean[j].Y].AlienLinks.Count; k++)
				{
					pos2 = Aliens[Field[mean[j].X][mean[j].Y].AlienLinks[k]].GetCoords();
					r2 = Aliens[Field[mean[j].X][mean[j].Y].AlienLinks[k]].GetRadius() * AttackCoef;
					tlength = Math.Sqrt((pos.X - pos2.X) * (pos.X - pos2.X) + (pos.Y - pos2.Y) * (pos.Y - pos2.Y));
					if (rang + r2 > (int)tlength)
					{
						if ((length == -1) || (tlength < length))
						{
							length = tlength;
							ind = Field[mean[j].X][mean[j].Y].AlienLinks[k];
						}
					}
				}
			}
			if (ind != -1)
			{
				return Aliens[ind].Hit(pos, fmass, demage, hiteffect);
			}
			return Null;
		}


        public static void CreateWave(int count)
        {
            int rand, x = 0, y = 0, density;
            List<Point> positions = new List<Point>();
            List<int> densites = new List<int>();
            int start = 500, end = WorldSize - 500;
            do
            {
                density = Rand.Next(MinAlienDensity, MaxAlienDensity);
                count -= density;
                densites.Add(density);
                rand = Rand.Next(0, 4);
                switch (rand)
                {
                    case 0:
                        y = start;
                        x = Rand.Next(start, end);
                        break;
                    case 1:
                        x = start;
                        y = Rand.Next(start, end);
                        break;
                    case 2:
                        y = end;
                        x = Rand.Next(start, end);
                        break;
                    case 3:
                        x = end;
                        y = Rand.Next(start, end);
                        break;
                }
                positions.Add(new Point(x, y));
            }
            while (count > 0);
            densites[densites.Count - 1] += count;
            Aliens.AddRange(AlienAvailible.CreateWave(positions, Degress - MaxRadius, densites));
        }

        public static TransAlien[] GetTransAliens()
        {
            List<TransAlien> temp = new List<TransAlien>();
            Point pos;
            int rad;
            for (int i = 0; i < Aliens.Count; i++)
            {
                pos = Aliens[i].GetCoords();
                rad = Aliens[i].GetRadius();
                if ((pos.X > -rad) && (pos.Y > -rad) && (pos.X < WorldSize + rad) && (pos.Y < WorldSize + rad))
                    temp.Add(Aliens[i].GetTransister());
            }
            TransAlien[] data = new TransAlien[temp.Count];
            for (int i = 0; i < temp.Count; i++)
                data[i] = temp[i];
            return data;
        }

        public static int GetAlien(Point pos)	// -1 -нету, иначе есть
        {
            int x, y;
            for (int i = 0; i < Aliens.Count; i++)
            {
                x = Aliens[i].GetCoords().X;
                y = Aliens[i].GetCoords().Y;
                if (Aliens[i].GetRadius() >= (int)Math.Sqrt((pos.X - x) * (pos.X - x) + (pos.Y - y) * (pos.Y - y)))
                {
                    Ref = Aliens[i];
                    return i;
                }
            }
            return -1;
        }

        public static TransInfo GetTransInfo(int alien)
        {
            return Aliens[alien].GetInfo();
        }

		private static bool CheckDistance(Point coords)
		{
			if ((coords.X < -(2 * Degress)) || (coords.Y < -(2 * Degress)) || (coords.X > WorldSize + 2 * Degress) || (coords.Y > WorldSize + 2 * Degress))
				return false;
			else
				return true;
		}

		private static void ClearCells()
		{
			for (int i = 0; i < Field.Length; i++)
                for (int j = 0; j < Field[i].Length; j++)
                    Field[i][j].ClearAlienLinks();             
		}
	}
}