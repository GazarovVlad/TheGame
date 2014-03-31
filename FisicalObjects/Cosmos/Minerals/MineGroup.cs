using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using GraphicObjects.ExplosionsSpace;
using FisicalObjects.Cosmos.Minerals.Base;
using FisicalObjects.Transist;

namespace FisicalObjects.Cosmos.Minerals
{
	static class MineGroup
	{
		public static Point Null = new Point(-1000, -1000);
		public static List<Mineral> AllMinerals { get; private set; }

		private static List<int> ModifedInfo;
		private static int Mined;
		private static Random Rand = new Random();
		private static Mineral Ref;

		public static void CreateWorld(List<Mineral> minerals)
		{
			Ref = null;
			AllMinerals = minerals;
			ModifedInfo = new List<int>();
			Mined = 0;
		}

		public static void DestroyWorld()
		{
			AllMinerals.Clear();
			ModifedInfo.Clear();
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

		public static void Add(Point poz, int mass)
		{
			AllMinerals.Add(MineAvalible.CreateMineral(poz, mass));
		}

		public static void Add(Point poz, string indifer)
		{
			AllMinerals.Add(MineAvalible.CreateMineral(poz, indifer));
		}

		public static Point Mine(Point poz, int rad, int demage, int target)
		{
			if (AllMinerals[target].Resource < 1)
				target = GetTarget(poz, rad);
			if ((target != -1) && (AllMinerals[target].Resource > 0))
			{
				Mined += AllMinerals[target].Mine(demage);
				int x, y;
				x = Rand.Next(AllMinerals[target].Position.X - AllMinerals[target].Radius / 2, AllMinerals[target].Position.X + AllMinerals[target].Radius / 2);
				y = Rand.Next(AllMinerals[target].Position.Y - AllMinerals[target].Radius / 2, AllMinerals[target].Position.Y + AllMinerals[target].Radius / 2);
				return new Point(x, y);
			}
			else
				return Null;
		}

		public static int ModifyTarget(int targ)
		{
			int ntarg = targ;
			for (int i = 0; i < ModifedInfo.Count; i++)
				if (targ == ModifedInfo[i])
					return -1;
			for (int i = 0; i < ModifedInfo.Count; i++)
				if (targ > ModifedInfo[i])
					ntarg--;
			if (AllMinerals[ntarg].Resource < 1)
				return -1;
			return ntarg;
		}

		public static int GetTarget(Point pos, int rad)
		{
			int targ = -1, min = -1, x, y, length;
			for (int i = 0; i < AllMinerals.Count; i++)
			{
				if (AllMinerals[i].Resource > 0)
				{
					x = AllMinerals[i].Position.X;
					y = AllMinerals[i].Position.Y;
					length = (int)Math.Sqrt((pos.X - x) * (pos.X - x) + (pos.Y - y) * (pos.Y - y));
					if (rad + AllMinerals[i].Radius > length)
					{
						if ((targ == -1) || (min > length))
						{
							targ = i;
							min = length;
						}
					}
				}
			}
			return targ;
		}

		public static int EndTurn()
		{
			int mined = Mined;
			ModifedInfo.Clear();
			Mined = 0;
			for (int i = AllMinerals.Count - 1; i >= 0; i--)
				if ((AllMinerals[i].Resource < 1) && (AllMinerals[i].Explosion != Mineral.NullExplosion))
				{
					if (Ref != null)
					{
						if (Ref.Equals(AllMinerals[i]))
							Ref = null;
					}
					ModifedInfo.Add(i);
					Explosions.Add(AllMinerals[i].Explosion, AllMinerals[i].Position.X, AllMinerals[i].Position.Y);
					AllMinerals.RemoveAt(i);
				}
			return mined;
		}

		public static bool ChekPlace(Point poz, int rad)
		{
			int x, y;
			for (int i = 0; i < AllMinerals.Count; i++)
			{
				x = AllMinerals[i].Position.X;
				y = AllMinerals[i].Position.Y;
				if (rad + AllMinerals[i].Radius >= (int)Math.Sqrt((poz.X - x) * (poz.X - x) + (poz.Y - y) * (poz.Y - y)))
					return false;
			}
			return true;
		}

		public static TransMineral[] GetTransMinerals()
		{
			TransMineral[] minerals = new TransMineral[AllMinerals.Count];
			for (int i = 0; i < minerals.Length; i++)
				minerals[i] = AllMinerals[i].GetTransist();
			return minerals;
		}

		public static int GetMiniral(Point pos)	// -1 -нету, иначе есть
		{
			int x, y;
			for (int i=0; i< AllMinerals.Count; i++)
			{
				x = AllMinerals[i].Position.X;
				y = AllMinerals[i].Position.Y;
				if (AllMinerals[i].Radius >= (int)Math.Sqrt((pos.X - x) * (pos.X - x) + (pos.Y - y) * (pos.Y - y)))
				{
					Ref = AllMinerals[i];
					return i;
				}
			}
			return -1;
		}

		public static TransInfo GetTransInfo(int mineral)
		{
			return AllMinerals[mineral].GetInfo();
		}
	}
}
