using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FisicalObjects.Transist;
using GraphicObjects.ExplosionsSpace;

namespace FisicalObjects.Cosmos.Asteroids.Base
{
	class Asteroid:IAsteroid
	{
		public const string ObjectType = "Asteroid";

		public int Mass { get; protected set; }			// Масса астероида
		public int HitPoints { get; protected set; }	// Здоровье астероида
		public int Radius { get; protected set; }		// радиус астероида
		public float X { get; protected set; }			// Координаты в мире
		public float Y { get; protected set; }			// Координаты в мире
		public float VX { get; protected set; }			// Вектор скорости
		public float VY { get; protected set; }			// Вектор скорости
		public Point Model { get; protected set; }		// ссылка на графику
		public float Angle { get; protected set;}			// угол поворота (лучше в 3D)
		public float AngleDegress { get; protected set; }	// изменение угла во времени (лучше в 3D)
		public int Shield { get; protected set; }		// защита от массы (только для HP)
		public int Type { get; protected set; }			// тип в AsterAvalible
		public int ClashDistance { get; protected set; }// расстояние от центра Земли, когда астероид сталкнется с ней
		public bool Explodes { get; protected set; }

		protected const int MaxShield = 25;
		protected const int MinShield = 10;

		protected static Random Rand = new Random();

		public Asteroid()
		{
			//	Empty
		}

		public Asteroid(int mass, int hp, int rad, Point pos, float vx, float vy, Point model, int type)
		{
			Mass = mass;
			HitPoints = hp;
			Radius = rad;
			X = pos.X;
			Y = pos.Y;
			VX = vx;
			VY = vy;
			Model = model;
			Angle = (float)(Rand.NextDouble() * Math.PI);
			AngleDegress = (float)((Rand.NextDouble() - 0.5) * 0.02);
			Shield = Rand.Next(MinShield, MaxShield);
			Type = type;
			ClashDistance = Earth.GetRandClashDistance();
			Explodes = true;
		}

		public virtual Point Hit(Point pos, int fmass, int demage, string hiteffect)
		{
			HitPoints -= fmass / Shield;
			HitPoints -= demage;
			double a, r, vx, vy;
			r = Math.Sqrt((pos.X - X) * (pos.X - X) + (pos.Y - Y) * (pos.Y - Y));
			a = fmass / (Mass * 1.0);
			vx = ((pos.X - X) / r) * a;
			vy = ((pos.Y - Y) / r) * a;
			VX -= (float)vx;
			VY -= (float)vy;
			double d, b, rad;
			int x, y;
			d = Math.Sqrt((X - pos.X) * (X - pos.X) + (Y - pos.Y) * (Y - pos.Y));
			rad = d - Radius + Rand.Next(1, 4);
			b = (rad * rad - Radius * Radius + d * d) / (2 * d);
			x = (int)(X + (pos.X - X) / (d / (d - b)));
			y = (int)(Y + (pos.Y - Y) / (d / (d - b)));
			Explosions.Add(hiteffect, x, y);
			return new Point(x, y);
		}

		public virtual void ExplodeHit(Point pos, int demage, int mass)
		{
			HitPoints -= demage;
			double a, r, vx, vy;
			r = Math.Sqrt((pos.X - X) * (pos.X - X) + (pos.Y - Y) * (pos.Y - Y));
			a = mass / (Mass * 1.0);
			vx = ((pos.X - X) / r) * a;
			vy = ((pos.Y - Y) / r) * a;
			VX -= (float)vx;
			VY -= (float)vy;
		}

		public virtual void Move()
		{
			Angle += AngleDegress;
			if (Angle >= 2 * Math.PI)
				Angle -= (float)(2 * Math.PI);
			X += VX;
			Y += VY;
			if (Earth.IsClash(new Point((int)X, (int)Y), Radius, ClashDistance))
			{
				Earth.Demage(Mass, new Point((int)X, (int)Y));
				HitPoints = 0;
				Explodes = false;
			}
		}

		public virtual void Clash(int mass, float vx, float vy, float v)
		{
			HitPoints -= (int)((mass * v) / Shield + 2);
			X -= VX;
			Y -= VY;
			VX = vx;
			VY = vy;
			X += VX;
			Y += VY;
		}

		public virtual bool IsAlive()
		{
			if (HitPoints > 0)
				return true;
			else
				return false;
		}

		public TransAsteroid GetTransister()
		{
			return new TransAsteroid(new Point((int)X, (int)Y), Model, Angle, Radius);
		}

		public Point GetCoords()
		{
			return new Point((int)X, (int)Y);
		}

		public int GetRadius()
		{
			return Radius;
		}

		public int GetMass()
		{
			return Mass;
		}

		public float GetVX()
		{
			return VX;
		}

		public float GetVY()
		{
			return VY;
		}

		public bool IsExplode()
		{
			return Explodes;
		}

		public int GetIntType()
		{
			return Type;
		}

		public virtual TransInfo GetInfo()
		{
			string firstinfo, secondinfo;
			double v = Math.Sqrt(VX * VX + VY * VY);
			firstinfo = "Прочность : " + HitPoints.ToString();
			secondinfo = "Масса - " + Mass.ToString() + '\n';
			secondinfo += "Скорость - " + ((int)(v * 100)).ToString();
			List<int> tind = new List<int>();
			tind.Add(Model.X);
			tind.Add(Model.Y);
			return new TransInfo(ObjectType, firstinfo, secondinfo, Radius, false, tind, ObjectType, new Point((int)X, (int)Y));
		}
	}
}
