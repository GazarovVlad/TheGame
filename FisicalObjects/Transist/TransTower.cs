using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace FisicalObjects.Transist
{
    public class TransTower
    {
        public Point Pozition { get; private set; }
        public int TBaseIndex { get; private set; }
		public float Angle { get; private set; }
        public int TTurretIndex { get; private set; }
        public float TurretAngle { get; private set; }
		public int Radius { get; private set; }

        public TransTower(int x, int y, int tbind, float ang, int ttind, float tang, int rad)
        {
            Pozition = new Point(x, y);
            TBaseIndex = tbind;
			Angle = ang;
            TTurretIndex = ttind;
            TurretAngle = tang;
			Radius = rad;
        }
    }
}
