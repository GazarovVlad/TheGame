using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FisicalObjects.Cosmos.Asteroids;
using FisicalObjects.Cosmos.Asteroids.Base;
using FisicalObjects.Cosmos.Asteroids.Descendants;
using FisicalObjects.Transist;
using GraphicObjects.ExplosionsSpace;

namespace FisicalObjects.Cosmos.Asteroids
{
	static class AsterField
	{
		public static Point Null = new Point(-1000, -1000);
		public static List<IAsteroid> Asteroids;

		private const float ClashCoef = 1;
		private const double AttackCoef = 0.8;
		private const double ExplCoef1 = 1;
		private const int ExplCoef2 = 5;
		private const int ExplCoef3 = 5;
		private const int MinDensity = 10;
		private const int MaxDensity = 45;

		private static int MaxRadius;
		private static Cell[][] Field;
		private static int WorldSize;
		private static int AsterMassDestroed;
		private static Random Rand = new Random();
		private static int Degress = 400;
		private static int Start;
		private static int End;
		private static IAsteroid Ref;

		public static void Inicialize(int worldsize)
		{
			WorldSize = worldsize;
			SimpleAsteroid.Inicialize(worldsize);
			Cell.Inicialize(worldsize);
			MaxRadius = AsterAvalible.GetMaxRadius();
			Start = -2 * Degress + MaxRadius;
			End = WorldSize + 2 * Degress - MaxRadius;
			/*
			Start = 500;
			End = WorldSize - 500;
			*/
			AsterAvalible.Inicialize(new Point(WorldSize / 2, WorldSize / 2));
		}

		public static void CreateWorld(int worldrad)
		{
			Ref = null;
			AsterMassDestroed = 0;
			Asteroids = new List<IAsteroid>();
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
			Asteroids.Clear();
			ClearCells();
		}

		public static void SkipRef()
		{
			Ref = null;
		}

		public static TransInfo ChekRef()
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
			while (i < Asteroids.Count)
			{
				while ((i < Asteroids.Count) && (!CheckDistance(Asteroids[i].GetCoords())))
				{
					if (Ref != null)
					{
						if (Ref.Equals(Asteroids[i]))
							Ref = null;
					}
					Asteroids.RemoveAt(i);
				}
				if (i < Asteroids.Count)
				{
					ind = Cell.GetCellIndex(Asteroids[i].GetCoords());
					Field[ind.X][ind.Y].AddAsterLink(i);
				}
				i++;
			}
		}

		public static void Process()
		{
			//	столкновения
			List<Point> mean = new List<Point>();
			Point pos1, pos2;
			int r1, r2;
			for (int i = 0; i < Asteroids.Count; i++)
			{
				pos1 = Asteroids[i].GetCoords();
				r1 = Asteroids[i].GetRadius();
				mean = Cell.GetMeaningfulCells(Asteroids[i].GetCoords(), Asteroids[i].GetRadius() + MaxRadius);
				for (int j = 0; j < mean.Count; j++)
				{
					for (int k = 0; k < Field[mean[j].X][mean[j].Y].AserLinks.Count; k++)
					{
						if (Field[mean[j].X][mean[j].Y].AserLinks[k] != i)
						{
							pos2 = Asteroids[Field[mean[j].X][mean[j].Y].AserLinks[k]].GetCoords();
							r2 = Asteroids[Field[mean[j].X][mean[j].Y].AserLinks[k]].GetRadius();
							if (r1 + r2 > (int)Math.Sqrt((pos1.X - pos2.X) * (pos1.X - pos2.X) + (pos1.Y - pos2.Y) * (pos1.Y - pos2.Y)))
							{
								Clash(i, Field[mean[j].X][mean[j].Y].AserLinks[k], pos1, pos2, r1, r2);
							}
						}
					}
				}
			}

			//	движение
			for (int i = 0; i < Asteroids.Count; i++)
				Asteroids[i].Move();

			//	смерть
			for (int i = Asteroids.Count - 1; i >= 0; i--)
				if (!Asteroids[i].IsAlive())
				{
					if (Asteroids[i].IsExplode())
					{
						Point pos = Asteroids[i].GetCoords();
						if (!((pos.X < -MaxRadius) || (pos.X > WorldSize + MaxRadius) || (pos.Y < -MaxRadius) || (pos.Y > WorldSize + MaxRadius)))
							AsterMassDestroed += Asteroids[i].GetMass();
						Explosions.Add(AsterAvalible.GetExplosionName(Asteroids[i].GetIntType()), pos.X, pos.Y);
					}
					else
							AsterMassDestroed -= Asteroids[i].GetMass();
					if (Ref != null)
					{
						if (Ref.Equals(Asteroids[i]))
							Ref = null;
					}
					Asteroids.RemoveAt(i);
				}
		}

		public static int GetAsterMassDestroed()
		{
			int temp = AsterMassDestroed;
			AsterMassDestroed = 0;
			return temp;
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
				for (int k = 0; k < Field[mean[j].X][mean[j].Y].AserLinks.Count; k++)
				{
					pos2 = Asteroids[Field[mean[j].X][mean[j].Y].AserLinks[k]].GetCoords();
					r2 = Asteroids[Field[mean[j].X][mean[j].Y].AserLinks[k]].GetRadius() * AttackCoef;
					tlength = Math.Sqrt((pos.X - pos2.X) * (pos.X - pos2.X) + (pos.Y - pos2.Y) * (pos.Y - pos2.Y));
					if (rang + r2 > (int)tlength)
					{
						if ((length == -1) || (tlength < length))
						{
							length = tlength;
							ind = Field[mean[j].X][mean[j].Y].AserLinks[k];
						}
					}
				}
			}
			if (ind != -1)
			{
				return Asteroids[ind].Hit(pos, fmass, demage, hiteffect);
			}
			return Null;
		}

		public static int TryClash(Point pos, int r, int mass)	// Проверка на столкновение постройки и астероидов
		{
			List<Point> mean = new List<Point>();
			Point pos2;
			int r2;
			double demage = 0;
			mean = Cell.GetMeaningfulCells(pos, r + MaxRadius);
			for (int j = 0; j < mean.Count; j++)
			{
				for (int k = 0; k < Field[mean[j].X][mean[j].Y].AserLinks.Count; k++)
				{
					pos2 = Asteroids[Field[mean[j].X][mean[j].Y].AserLinks[k]].GetCoords();
					r2 = Asteroids[Field[mean[j].X][mean[j].Y].AserLinks[k]].GetRadius();
					if (r + r2 > (int)Math.Sqrt((pos.X - pos2.X) * (pos.X - pos2.X) + (pos.Y - pos2.Y) * (pos.Y - pos2.Y)))
					{
						double vx, vy, v;
						vx = Asteroids[Field[mean[j].X][mean[j].Y].AserLinks[k]].GetVX();
						vy = Asteroids[Field[mean[j].X][mean[j].Y].AserLinks[k]].GetVY();
						v = Math.Sqrt(vx * vx + vy * vy);
						demage += Asteroids[Field[mean[j].X][mean[j].Y].AserLinks[k]].GetMass() * v * ClashCoef + 2;
						SingleClash(Field[mean[j].X][mean[j].Y].AserLinks[k], mass, pos, pos2, r, r2);
					}
				}
			}
			return (int)(demage);
		}

		public static void Explode(Point pos, int explforse)
		{
			List<Point> mean = new List<Point>();
			Point pos2;
			double r2, length;
			mean = Cell.GetMeaningfulCells(pos, explforse + MaxRadius);
			for (int j = 0; j < mean.Count; j++)
			{
				for (int k = 0; k < Field[mean[j].X][mean[j].Y].AserLinks.Count; k++)
				{
					pos2 = Asteroids[Field[mean[j].X][mean[j].Y].AserLinks[k]].GetCoords();
					r2 = Asteroids[Field[mean[j].X][mean[j].Y].AserLinks[k]].GetRadius() * ExplCoef1;
					length = Math.Sqrt((pos.X - pos2.X) * (pos.X - pos2.X) + (pos.Y - pos2.Y) * (pos.Y - pos2.Y));
					if (explforse + r2 > length)
					{
						int demage, mass;
						demage = ExplCoef2 * explforse;
						mass = ExplCoef3 * explforse;
						Asteroids[Field[mean[j].X][mean[j].Y].AserLinks[k]].ExplodeHit(pos, demage, mass);
					}
				}
			}
		}

		public static void CreateWave(int count)
		{
			int rand, x = 0, y = 0, density;
			List<Point> positions = new List<Point>();
			List<int> densites = new List<int>();
			do
			{
				density = Rand.Next(MinDensity, MaxDensity);
				count -= density;
				densites.Add(density);
				rand = Rand.Next(0, 4);
				switch (rand)
				{
					case 0:
						y = Start;
						x = Rand.Next(Start, End);
						break;
					case 1:
						x = Start;
						y = Rand.Next(Start, End);
						break;
					case 2:
						y = End;
						x = Rand.Next(Start, End);
						break;
					case 3:
						x = End;
						y = Rand.Next(Start, End);
						break;
				}
				positions.Add(new Point(x, y));
			}
			while (count > 0);
			densites[densites.Count - 1] += count;
			Asteroids.AddRange(AsterAvalible.CreateWave(positions, Degress - MaxRadius, densites));
		}

		public static TransAsteroid[] GetTransAsteroids()
		{
			List<TransAsteroid> temp = new List<TransAsteroid>();
			Point pos;
			int rad;
			for (int i = 0; i < Asteroids.Count; i++)
			{
				pos = Asteroids[i].GetCoords();
				rad = Asteroids[i].GetRadius();
				if ((pos.X > -rad) && (pos.Y > -rad) && (pos.X < WorldSize + rad) && (pos.Y < WorldSize + rad))
					temp.Add(Asteroids[i].GetTransister());
			}
			TransAsteroid[] data = new TransAsteroid[temp.Count];
			for (int i = 0; i < temp.Count; i++)
				data[i] = temp[i];
			return data;
		}

		public static int GetAsteroid(Point pos)	// -1 -нету, иначе есть
		{
			int x, y;
			for (int i = 0; i < Asteroids.Count; i++)
			{
				x = Asteroids[i].GetCoords().X;
				y = Asteroids[i].GetCoords().Y;
				if (Asteroids[i].GetRadius() >= (int)Math.Sqrt((pos.X - x) * (pos.X - x) + (pos.Y - y) * (pos.Y - y)))
				{
					Ref = Asteroids[i];
					return i;
				}
			}
			return -1;
		}

		public static TransInfo GetTransInfo(int asteroid)
		{
			return Asteroids[asteroid].GetInfo();
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
					Field[i][j].ClearAsterLinks();
		}

		private static void Clash(int a1, int a2, Point pos1, Point pos2, int r1, int r2)
		{
			float vx1, vy1, vx2, vy2, m1, m2, vxx1, vxx2, vyy1, vyy2, v1, v2;
			float x1, y1, x2, y2, a;
			vx1 = Asteroids[a1].GetVX();
			vy1 = Asteroids[a1].GetVY();
			m1 = Asteroids[a1].GetMass();
			vx2 = Asteroids[a2].GetVX();
			vy2 = Asteroids[a2].GetVY();
			m2 = Asteroids[a2].GetMass();
			x1 = pos1.X - pos2.X;
			y1 = pos1.Y - pos2.Y;
			a = (float)(Math.Acos(x1 / Math.Sqrt(x1 * x1 + y1 * y1)));	// угол между прямыми
			x1 = (float)(vx1 * Math.Cos(a) - vy1 * Math.Sin(a));
			y1 = (float)(vx1 * Math.Sin(a) + vy1 * Math.Cos(a));
			x2 = (float)(vx2 * Math.Cos(a) - vy2 * Math.Sin(a));
			y2 = (float)(vx2 * Math.Sin(a) + vy2 * Math.Cos(a));	// вектора скоростей в новой системе
			vx1 = ((m1 - m2) * x1 + 2 * m2 * x2) / (m1 + m2);
			vx2 = ((m2 - m1) * x2 + 2 * m1 * x1) / (m1 + m2);	// вектора скоростей после столкновений
			v1 = (float)Math.Abs(x1 - vx1);
			v2 = (float)Math.Abs(x2 - vx2);
			vxx1 = (float)(vx1 * Math.Cos(2 * Math.PI - a) - y1 * Math.Sin(2 * Math.PI - a));
			vyy1 = (float)(vx1 * Math.Sin(2 * Math.PI - a) + y1 * Math.Cos(2 * Math.PI - a));
			vxx2 = (float)(vx2 * Math.Cos(2 * Math.PI - a) - y2 * Math.Sin(2 * Math.PI - a));
			vyy2 = (float)(vx2 * Math.Sin(2 * Math.PI - a) + y2 * Math.Cos(2 * Math.PI - a));
			Asteroids[a1].Clash(Asteroids[a2].GetMass(), vxx1, vyy1, v1);
			Asteroids[a2].Clash(Asteroids[a1].GetMass(), vxx2, vyy2, v2);
			double d, b;
			int x, y;
			d = Math.Sqrt((pos1.X - pos2.X) * (pos1.X - pos2.X) + (pos1.Y - pos2.Y) * (pos1.Y - pos2.Y));
			b = (r2 * r2 - r1 * r1 + d * d) / (2 * d);
			x = (int)(pos1.X + (pos2.X - pos1.X) / (d / (d - b)));
			y = (int)(pos1.Y + (pos2.Y - pos1.Y) / (d / (d - b)));
			Explosions.Add(AsterAvalible.GetClashName(Asteroids[a1].GetIntType(), Asteroids[a2].GetIntType()), x, y);
		}

		private static void SingleClash(int ind, int m1, Point pos1, Point pos2, int r1, int r2)
		{
			float vx2, vy2, m2, v;
			float x2, y2, a;
			vx2 = Asteroids[ind].GetVX();
			vy2 = Asteroids[ind].GetVY();
			m2 = Asteroids[ind].GetMass();
			a = (float)(Math.Acos(vx2 / Math.Sqrt(vx2 * vx2 + vy2 * vy2)));
			x2 = (float)(vx2 * Math.Cos(a) - vy2 * Math.Sin(a));
			y2 = (float)(vx2 * Math.Sin(a) + vy2 * Math.Cos(a));
			vx2 = ((m2 - m1) * vx2) / (m1 + m2);
			v = (float)Math.Abs(x2 - vx2);
			x2 = (float)(vx2 * Math.Cos(2 * Math.PI - a) - y2 * Math.Sin(2 * Math.PI - a));
			y2 = (float)(vx2 * Math.Sin(2 * Math.PI - a) + y2 * Math.Cos(2 * Math.PI - a));
			Asteroids[ind].Clash(m1, x2, y2, v);
			double d, b;
			int x, y;
			d = Math.Sqrt((pos1.X - pos2.X) * (pos1.X - pos2.X) + (pos1.Y - pos2.Y) * (pos1.Y - pos2.Y));
			b = (r2 * r2 - r1 * r1 + d * d) / (2 * d);
			x = (int)(pos1.X + (pos2.X - pos1.X) / (d / (d - b)));
			y = (int)(pos1.Y + (pos2.Y - pos1.Y) / (d / (d - b)));
			Explosions.Add(AsterAvalible.GetClashName(Asteroids[ind].GetIntType()), x, y);
		}
	}
}
