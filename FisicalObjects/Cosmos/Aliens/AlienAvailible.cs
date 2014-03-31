using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using FisicalObjects.Cosmos.Aliens.Base;
using FisicalObjects.Cosmos.Aliens.Descendants;

namespace FisicalObjects.Cosmos.Aliens
{
    static class AlienAvailible
    {
		public static string[] Types { get; private set; }
		public static int[] Counts { get; private set; }
		public static int[] TexSizes { get; private set; }
		public static int[] Rads { get; private set; }
		public static string[] Explosions { get; private set; }
		public static int[] HitPoints { get; private set; }
		public static int[] Mass { get; private set; }
        public static float[] MaxPowers {get; private set; }

		private static string[] Clashes;

		private const string Path = "Settings\\SpaceObjects.txt";
		private const string Starts = "Aliens:";
		private const string Ends = "End";
        //private const int Offset1 = -500;
        //private const int Offset2 = 500;
        //private const double SpeedMin = 0.07;
        //private const double SpeedMax = 0.6;

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
				if (data.Key == "MaxPowers")
				{
					temp = data.Value.Split('/');
					MaxPowers = new float[temp.Length];
					for (int i = 0; i < temp.Length; i++)
						MaxPowers[i] = (float)Convert.ToDouble(temp[i]);
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

		public static IAlien CreateSimpleAlien(string Type, Point pos, float vx, float vy)
		{
            int index = 0;
			for(int i = 0; i < Types.Length; i++)
                if(Types[i] == Type)
                {   
                    index = i;
                    break;
                }

			return new SimpleAlien(new Alien(Mass[index], HitPoints[index], Rads[index], pos, vx, vy, new Point(index, Rand.Next(0, Counts[index])), 0.0f, index, MaxPowers[index], Rand.Next(3, (int)(MaxPowers[index]*10))/10.0f));
		}

        //public static IAsteroid CreateSimpleAsteroid(int mass, Point pos)
        //{
        //    Point cent = new Point(Rand.Next(Earth.X + Offset1, Earth.X + Offset2), Rand.Next(Earth.Y + Offset1, Earth.Y + Offset2));
        //    double a, len, vx, vy;
        //    len = Math.Sqrt((cent.X - pos.X) * (cent.X - pos.X) + (cent.Y - pos.Y) * (cent.Y - pos.Y));
        //    a = Rand.NextDouble() * (SpeedMax - SpeedMin) + SpeedMin;
        //    vx = ((cent.X - pos.X) / len) * a;
        //    vy = ((cent.Y - pos.Y) / len) * a;
        //    return CreateSimpleAsteroid(mass, pos, (float)vx, (float)vy);
        //}

		public static List<IAlien> CreateWave(List<Point> pos, int size, List<int> densites)
		{
			List<IAlien> wave = new List<IAlien>();
			
			for (int i = 0; i < pos.Count; i++)
				for (int j = 0; j < densites[i]; j++)
					wave.Add(CreateSimpleAlien(Types[Rand.Next(0,Types.Length)], new Point(
                        Rand.Next(pos[i].X - size, pos[i].X + size), 
                        Rand.Next(pos[i].Y - size, pos[i].Y + size)), 0.0f, 0.0f));
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
