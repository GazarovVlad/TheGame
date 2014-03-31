using System;
using System.Collections.Generic;
using System.Text;
using SupportedStructures;

namespace GraphicObjects.StarsSpace
{
    public static class Stars
    {
        public static int KoefMin { get; private set; }
        public static int KoefMax { get; private set; }

        public static int[] Size { get; private set; }
        public static int[] Count { get; private set; }
        public static double[] OutMapRad { get; private set; }
        public static int[] MinCount { get; private set; }
        public static int[] MaxCount { get; private set; }
        public static string[] Types { get; private set; }
        public static List<Star>[] AllStars { get; private set; }
        public static Field Space; // copy from ProgramObjects.ScreenGroup.WorkSpace.cs
        public static int MapLen; //

        private static int TypesCount;

        public static void Initialize(int[] minCount, int[] maxCount, int[] size, int[] count, int koefMin, int koefMax, string[] types, int typesCount, double[] outMapRad, Field space, int mapLen)
        {
            Size = size;
            Count = count;
            OutMapRad = outMapRad;
            Types = types;
            MinCount = minCount;
            MaxCount = maxCount;
            KoefMax = koefMax;
            KoefMin = koefMin;
            TypesCount = typesCount;
            Space = space;
            MapLen = mapLen;

            Random rand = new Random();
            int sCount;
            AllStars = new List<Star>[TypesCount];
            for (int j = 0; j < AllStars.Length; j++)
            {
                sCount = rand.Next(MinCount[j], MaxCount[j]);
                AllStars[j] = new List<Star>();
                for (int i = 0; i < sCount; i++)
                {
                    AllStars[j].Add(new Star(rand.Next(Count[j]), OutMapRad[j], 0 - OutMapRad[j], 0 - OutMapRad[j], MapLen + OutMapRad[j], MapLen + OutMapRad[j]));
                }
            }
        }

        public static void ModifyPositions()
        {
            for (int j = 0; j < TypesCount; j++ )
                for (int i = 0; i < AllStars[j].Count; i++)
                    AllStars[j][i].ModifyPosition();
        }
    }
}
