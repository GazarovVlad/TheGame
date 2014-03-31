using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FisicalObjects.Cosmos.Asteroids.Base;
using FisicalObjects.Transist;

namespace FisicalObjects.Cosmos.Asteroids.Descendants
{
	class SimpleAsteroid:Asteroid
	{
		private const int Gravitaion = 1000;
		private const int Retarder1 = 14000;
		private const double Retarder2 = 0.00075;
		private const double Retarder3 = 0.0035;

		private static Point Center;

		public static void Inicialize(int mapsize)
		{
			Center = new Point(mapsize / 2, mapsize / 2);
		}

		public SimpleAsteroid(Asteroid body, int mass)
		{
			Angle = body.Angle;
			AngleDegress = body.AngleDegress;
			HitPoints = (int)(((body.HitPoints * 1.0) / body.Mass) * mass);
			Mass = body.Mass;
			Model = body.Model;
			Radius = body.Radius;
			VX = body.VX;
			VY = body.VY;
			X = body.X;
			Y = body.Y;
			Shield = body.Shield;
			Type = body.Type;
			ClashDistance = body.ClashDistance;
			Explodes = true;
		}

		public override void Move()
		{
			Angle += AngleDegress;
			if (Angle >= 2 * Math.PI)
				Angle -= (float)(2 * Math.PI);
			X += VX;
			Y += VY;
			double a, r, vx, vy;
			r = Math.Sqrt((Center.X - X) * (Center.X - X) + (Center.Y - Y) * (Center.Y - Y));
			a = (Gravitaion / (r * r * Retarder2 + Retarder1)) * Retarder3;
			vx = ((Center.X - X) / r) * a;
			vy = ((Center.Y - Y) / r) * a;
			VX += (float)vx;
			VY += (float)vy;
			if (Earth.IsClash(new Point((int)X, (int)Y), Radius, ClashDistance))
			{
				Earth.Demage(Mass, new Point((int)X, (int)Y));
				HitPoints = 0;
				Explodes = false;
			}
		}
	}
}
