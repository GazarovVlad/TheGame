using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace FisicalObjects.Transist
{
    public class TransBuilding
    {
        public Point Pozition { get; private set; }
        public int TBuildingIndex { get; private set; }
		public float Angle { get; private set; }
		public int Radius { get; private set; }

        public TransBuilding(int x, int y, int ind, float ang, int rad)
        {
            Pozition = new Point(x, y);
            TBuildingIndex = ind;
			Angle = ang;
			Radius = rad;
        }
    }
}
