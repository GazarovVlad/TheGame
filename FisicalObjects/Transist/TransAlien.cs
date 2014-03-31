using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace FisicalObjects.Transist
{
    public class TransAlien
    {
        public Point Pozition { get; private set; }
        public Point Model { get; private set; }
		public float Angle { get; private set; }
		public int Radius { get; private set; }

		public TransAlien(Point poz, Point model, float ang, int rad)
        {
            Pozition = new Point(poz.X, poz.Y);
			Model = model;
			Angle = ang;
			Radius = rad;
        }
    }
}
