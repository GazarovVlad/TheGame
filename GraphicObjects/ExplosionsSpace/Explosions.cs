using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GraphicObjects.ExplosionsSpace
{
    public static class Explosions
    {
        public static int[] Stages { get; private set; }		// Количество стадий у каждого типа взрыва
		public static int[] TypeCounts { get; private set; }	// Количество типов стадий у каждого типа взрыва
		public static int[] TextureSizes { get; private set; }	// Размер текстур у каждого типа взрыва (у 1-го типа все текстуры 1-го размера)
		public static string[] TypeNames { get; private set; }	// Названия каждого типа взрыва
		public static List<Explosion> AllExplosions { get; private set; }

		private const int MinCount = 200;
		private const int MaxCount = 250;
		private const int MaxPause = 3;
		private const int MaxOnesExpl = 7;

		private static int StageCount;
		private static int Pause;
		private static Random Rand = new Random();
		private static Point Earth;
		private static int Rad;

        public static void Initialize(int[] stages, int[] typeCounts, string[] typeNames, int[] textureSizes)
        {
            Stages = stages;
			StageCount = 0;
			Pause = 0;
            TypeCounts = typeCounts;
            TypeNames = typeNames;
            TextureSizes = textureSizes;
            AllExplosions = new List<Explosion>();
        }

		public static void Add(string typeName, int x, int y)
		{
			for (int i = 0; i < TypeNames.Length; i++)
				if (TypeNames[i] == typeName)
					AllExplosions.Add(new Explosion(i, x, y));
		}
		public static void AddSpecial(string typeName, int x, int y)
		{
			for (int i = 0; i < TypeNames.Length; i++)
				if (TypeNames[i] == typeName)
					AllExplosions.Add(new Explosion(i, x, y, true));
		}

        public static void Process()
        {
			List<int> ended = new List<int>();
			int x, y, times;
			if (Pause > 0)
				Pause--;
			while ((StageCount > 0) && (Pause == 0))
			{
				StageCount--;
				Pause = Rand.Next(0, MaxPause);
				times = Rand.Next(3, MaxOnesExpl);
				while (times > 0)
				{
					do
					{
						x = Rand.Next(Earth.X - Rad, Earth.X + Rad);
						y = Rand.Next(Earth.Y - Rad, Earth.Y + Rad);
					}
					while ((int)Math.Sqrt((x - Earth.X) * (x - Earth.X) + (y - Earth.Y) * (y - Earth.Y)) > Rad);
					AllExplosions.Add(new Explosion(Rand.Next(0, TypeNames.Length), x, y));
					times--;
				}
			}
            for (int i = 0; i < AllExplosions.Count; i++)
            {
                if (AllExplosions[i].ProcessIsEnded())
                    ended.Add(i);
            }
            ended.Sort();
			for (int i = ended.Count - 1; i >= 0; i--)
				AllExplosions.RemoveAt(ended[i]);
        }

		public static void DestroyEarth(Point earth, int rad)
		{
			StageCount = Rand.Next(MinCount, MaxCount);
			Earth = earth;
			Rad = rad;
		}
    }
}
