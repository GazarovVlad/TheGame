using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FisicalObjects.Cosmos.Asteroids.Base;
using FisicalObjects.Cosmos.Asteroids.Descendants;

namespace FisicalObjects.Cosmos.Asteroids
{
	static class AsterAvalible
	{
		public static string[] Types { get; private set; }
		public static int[] Counts { get; private set; }
		public static int[] TexSizes { get; private set; }
		public static int[] Rads { get; private set; }
		public static string[] Explosions { get; private set; }
		public static int[] HitPoints { get; private set; }
		public static int[] Mass { get; private set; }

		private static string[] Clashes;

		private const string Path = "Settings\\SpaceObjects.txt";
		private const string Starts = "Asteroids:";
		private const string Ends = "End";
		private const int Offset1 = -500;
		private const int Offset2 = 500;
		private const double SpeedMin = 0.07;
		private const double SpeedMax = 0.6;

		private static Random Rand = new Random();
		private static Point Earth;

		public static void Load()
		{
			Loder data = new Loder(Path, Starts, Ends);
			string[] temp;
			while (data.Next())
			{
				if (data.Key == "Types")
				{
					Types = data.Value.Split('/');
				}
				if (data.Key == "Counts")
				{
					temp = data.Value.Split('/');
					Counts = new int[temp.Length];
					for (int i = 0; i < temp.Length; i++)
						Counts[i] = Convert.ToInt32(temp[i]);
				}
				if (data.Key == "TexureSizes")
				{
					temp = data.Value.Split('/');
					TexSizes = new int[temp.Length];
					for (int i = 0; i < temp.Length; i++)
						TexSizes[i] = Convert.ToInt32(temp[i]);
				}
				if (data.Key == "RadSizes")
				{
					temp = data.Value.Split('/');
					Rads = new int[temp.Length];
					for (int i = 0; i < temp.Length; i++)
						Rads[i] = Convert.ToInt32(temp[i]);
				}
				if (data.Key == "Explosions")
				{
					temp = data.Value.Split('/');
					Explosions = new string[temp.Length];
					for (int i = 0; i < temp.Length; i++)
						Explosions[i] = temp[i];
				}
				if (data.Key == "HitPoints")
				{
					temp = data.Value.Split('/');
					HitPoints = new int[temp.Length];
					for (int i = 0; i < temp.Length; i++)
						HitPoints[i] = Convert.ToInt32(temp[i]);
				}
				if (data.Key == "Mass")
				{
					temp = data.Value.Split('/');
					Mass = new int[temp.Length];
					for (int i = 0; i < temp.Length; i++)
						Mass[i] = Convert.ToInt32(temp[i]);
				}
				if (data.Key == "Clashes")
				{
					temp = data.Value.Split('/');
					Clashes = new string[temp.Length];
					for (int i = 0; i < temp.Length; i++)
						Clashes[i] = temp[i];
				}
			}
			data.EndReading();
		}

		public static void Inicialize(Point earth)
		{
			Earth = earth;
		}

		public static IAsteroid CreateSimpleAsteroid(int mass, Point pos, float vx, float vy)
		{
			int low = -1, more = -1, ind = 0;
			for (int i = 0; i < Mass.Length; i++)
			{
				if ((mass > Mass[i]) && ((low == -1)||(Mass[low] < Mass[i])))
					low = i;
				if ((mass < Mass[i]) && ((more == -1)||(Mass[more] > Mass[i])))
					more = i;
			}
			if ((low == -1) && (more == -1))
				ind = 0;
			else
			{
				if ((low != -1) && (more != -1))
				{
					if (mass - Mass[low] < Mass[more] - mass)
						ind = low;
					else
						ind = more;
				}
				else
				{
					if (low == -1)
						ind = more;
					if (more == -1)
						ind = low;
				}
			}
			float temp = Mass[ind] / HitPoints[ind];
			return new SimpleAsteroid(new Asteroid(mass, (int)(mass * temp), Rads[ind], pos, vx, vy, new Point(ind, Rand.Next(0, Counts[ind])), ind), mass);
		}

		public static IAsteroid CreateSimpleAsteroid(int mass, Point pos)
		{
			Point cent = new Point(Rand.Next(Earth.X + Offset1, Earth.X + Offset2), Rand.Next(Earth.Y + Offset1, Earth.Y + Offset2));
			double a, len, vx, vy;
			len = Math.Sqrt((cent.X - pos.X) * (cent.X - pos.X) + (cent.Y - pos.Y) * (cent.Y - pos.Y));
			a = Rand.NextDouble() * (SpeedMax - SpeedMin) + SpeedMin;
			vx = ((cent.X - pos.X) / len) * a;
			vy = ((cent.Y - pos.Y) / len) * a;
			return CreateSimpleAsteroid(mass, pos, (float)vx, (float)vy);
		}

		public static List<IAsteroid> CreateWave(List<Point> pos, int size, List<int> densites)
		{
			List<IAsteroid> wave = new List<IAsteroid>();
			int minmass = Mass[0], maxmass = Mass[0];
			foreach (int mass in Mass)
			{
				if (mass < minmass)
					minmass = mass;
				if (mass > maxmass)
					maxmass = mass;
			}
			minmass = (int)(minmass * 0.75);
			maxmass = (int)(maxmass * 1.25);
			for (int i = 0; i < pos.Count; i++)
				for (int j = 0; j < densites[i]; j++)
					wave.Add(CreateSimpleAsteroid(Rand.Next(minmass, maxmass), new Point(Rand.Next(pos[i].X - size, pos[i].X + size), Rand.Next(pos[i].Y - size, pos[i].Y + size))));
			return wave;
		}

		public static int GetMaxRadius()
		{
			int max = Rads[0];
			foreach (int rad in Rads)
				if (rad > max)
					max = rad;
			return max;
		}

		public static string GetClashName(int type1, int type2)
		{
			if (Rads[type1] < Rads[type2])
				return Clashes[type1];
			else
				return Clashes[type2];
		}

		public static string GetClashName(int type)
		{
			return Clashes[type];
		}

		public static string GetExplosionName(int type)
		{
			return Explosions[type];
		}

		public static int[] GetAllRads()
		{
			return Rads;
		}
	}
}
