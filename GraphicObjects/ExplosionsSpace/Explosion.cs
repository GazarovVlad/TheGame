using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicObjects.ExplosionsSpace
{
    public class Explosion
    {
        public int Type { get; private set; }
        public int Stage { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public float Angle { get; private set; }

        private static Random Rand = new Random();

        private int StageType;

        public Explosion(int type, int x, int y)
        {
            StageType = Rand.Next(0, Explosions.TypeCounts[Type]);
            Angle = (float)(Rand.NextDouble() * Math.PI);
            Type = type;
            Stage = 0;
            X = x;
            Y = y;
		}

		public Explosion(int type, int x, int y, bool IsSpecial)
		{
			StageType = Rand.Next(0, Explosions.TypeCounts[Type]);
			Angle = (float)(Rand.NextDouble() * Math.PI);
			Type = type;
			Stage = 1;
			X = x;
			Y = y;
		}

        public int GetStageType()
        {
            return StageType;
        }

        public bool ProcessIsEnded()
        {
            StageType = Rand.Next(0, Explosions.TypeCounts[Type]);
            Stage++;
            if (Stage > Explosions.Stages[Type])
                return true;
            else
                return false;
        }
    }
}
