using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace FisicalObjects.Transist
{
    public class TransMineral
    {
        public Point Pozition { get; private set; }
        public int T1 { get; private set; }
		public int T2 { get; private set; }
		public int T3 { get; private set; }
        public float Angle { get; private set; }
		public int Radius { get; private set; }

		public TransMineral(Point poz, int t1, int t2, int t3, float ang, int rad)
        {
            Pozition = poz;
			T1 = t1;
			T2 = t2;
			T3 = t3;
            Angle = ang;
			Radius = rad;
        }
    }
}
