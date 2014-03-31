using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace FisicalObjects
{
	static class Earth
	{
		public static bool IsExpls;

		private const int MaxHP = 5000;
		private const int MineralIndent = 250;
		private const float MinClashDistance = 0.65f;
		private const float MaxClashDistance = 0.9f;

		private static int HitPoints;
		private static Point Coords;
		private static int Radius;
		private static Random Rand;
		private static List<Point> Explosions;

		public static void Inicialize(int mapsize, int radius)
		{
			IsExpls = false;
			Radius = radius;
			Coords = new Point(mapsize / 2, mapsize / 2);
			Explosions = new List<Point>();
			Rand = new Random();
		}

		public static void CreateWorld()
		{
			HitPoints = MaxHP;
		}

		public static bool IsClash(Point position, int radius)
		{
			if (Radius + radius >= (int)Math.Sqrt((position.X - Coords.X) * (position.X - Coords.X) + (position.Y - Coords.Y) * (position.Y - Coords.Y)))
				return true;
			else
				return false;
		}

		public static bool IsClash(Point position, int radius, int clashdistance)
		{
			if (clashdistance + radius >= (int)Math.Sqrt((position.X - Coords.X) * (position.X - Coords.X) + (position.Y - Coords.Y) * (position.Y - Coords.Y)))
				return true;
			else
				return false;
		}

		public static int GetRandClashDistance()
		{
			return (int)((Rand.NextDouble() * (MaxClashDistance - MinClashDistance) + MinClashDistance) * Radius);
		}

		public static void Demage(int demage, Point position)
		{
			HitPoints -= demage;
			Explosions.Add(new Point(position.X - Coords.X, position.Y - Coords.Y));
			IsExpls = true;
		}

		public static int GetHitPoints()
		{
			return HitPoints;
		}

		public static int GetMineralMinRadius()
		{
			return Radius + MineralIndent;
		}

		public static Point GetPosition()
		{
			return Coords;
		}

		public static List<Point> GetExplosions()
		{
			return Explosions;
		}

		public static void ClearExpls()
		{
			IsExpls = false;
			Explosions.Clear();
		}

		public static void Prepare()
		{
			Explosions.Clear();
		}
	}
}
