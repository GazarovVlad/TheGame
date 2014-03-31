using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace FisicalObjects.Transist
{
	public class TransFire
	{
		public Point Target { get; private set; }
		public string FireType { get; private set; }
		public string RodEffect { get; private set; }
		public int RodRadius { get; private set; }
		public Point RodPosition { get; private set; }
		public float Angle { get; private set; }

		public TransFire(float ang, Point pos, Point targ, string firetype, int rr, string re)
		{
			int x, y;
			y = -(int)(rr * Math.Cos(ang));
			x = (int)(rr * Math.Sin(ang));
			RodPosition = new Point(x + pos.X, y + pos.Y);
			Angle = ang;
			Target = targ;
			FireType = firetype;
			RodRadius = rr;
			RodEffect = re;
		}
	}
}
