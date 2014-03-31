using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FisicalObjects.Transist;
using GraphicObjects.ExplosionsSpace;

namespace FisicalObjects.Cosmos.Aliens.Base
{
	class Alien:IAlien
	{
        public const string ObjectType = "Alien";

		public int Mass { get; protected set; }			// Масса корабля
		public int HitPoints { get; protected set; }	// Здоровье корабля
		public int Radius { get; protected set; }		// радиус корабля
		public float X { get; protected set; }			// Координаты в мире
		public float Y { get; protected set; }			// Координаты в мире
		public float VX { get; protected set; }			// Вектор скорости
		public float VY { get; protected set; }			// Вектор скорости
        // "угол поворота носа корабля в текущий момент времени" в радианах или "текущий курс"
        //(!)(понимать так: 0 -> смотрит и летит вверх; 3.14 -> вниз и т.д.)
        private float angle;
        public float Angle
        {
            get { return angle; }
            protected set
            {
                angle = value % (float)(2 * Math.PI);
                if (angle < 0)
                    angle += (float)(2 * Math.PI);
            }
        }
        protected bool EngineTurnOn = true;
        protected Point Target; //текущая цель        
		public int Type { get; protected set; }			// тип в AlienAvailible
		public int ClashDistance { get; protected set; }// расстояние от центра Земли, когда корабль столкнется с ней
		public bool Explodes { get; protected set; }
        public float MaxPower { get; protected set; }   //максимальная сила тяги двигателя
        public float Power { get; protected set; }      //текущая
        public Point Model { get; protected set; }		// ссылка на графику (x - индекс типа, y - индекс вида в типе)

		protected static Random Rand = new Random();

        public Alien()
		{
			//	Empty
		}

        public Alien(int mass, int hp, int rad, Point pos, float vx, float vy, Point model, float angle, int type, float maxPower, float power)
		{
			Mass = mass;
			HitPoints = hp;
			Radius = rad;
			X = pos.X;
			Y = pos.Y;
			VX = vx;
			VY = vy;
			Model = model;
			Angle = angle;
			Type = type;
			ClashDistance = Earth.GetRandClashDistance();
			Explodes = true;
            MaxPower = maxPower;
            Power = power;
		}

        //public virtual Point Hit(Point pos, int fmass, int demage, string hiteffect)
        //{
        //    HitPoints -= fmass / Shield;
        //    HitPoints -= demage;
        //    double a, r, vx, vy;
        //    r = Math.Sqrt((pos.X - X) * (pos.X - X) + (pos.Y - Y) * (pos.Y - Y));
        //    a = fmass / (Mass * 1.0);
        //    vx = ((pos.X - X) / r) * a;
        //    vy = ((pos.Y - Y) / r) * a;
        //    VX -= (float)vx;
        //    VY -= (float)vy;
        //    double d, b, rad;
        //    int x, y;
        //    d = Math.Sqrt((X - pos.X) * (X - pos.X) + (Y - pos.Y) * (Y - pos.Y));
        //    rad = d - Radius + Rand.Next(1, 4);
        //    b = (rad * rad - Radius * Radius + d * d) / (2 * d);
        //    x = (int)(X + (pos.X - X) / (d / (d - b)));
        //    y = (int)(Y + (pos.Y - Y) / (d / (d - b)));
        //    Explosions.Add(hiteffect, x, y);
        //    return new Point(x, y);
        //}

        //public virtual void ExplodeHit(Point pos, int demage, int mass)
        //{
        //    HitPoints -= demage;
        //    double a, r, vx, vy;
        //    r = Math.Sqrt((pos.X - X) * (pos.X - X) + (pos.Y - Y) * (pos.Y - Y));
        //    a = mass / (Mass * 1.0);
        //    vx = ((pos.X - X) / r) * a;
        //    vy = ((pos.Y - Y) / r) * a;
        //    VX -= (float)vx;
        //    VY -= (float)vy;
        //}

        public virtual void Move()
		{
			
		}

		public virtual bool IsAlive()
		{
			if (HitPoints > 0)
				return true;
			else
				return false;
		}

		public TransAlien GetTransister()
		{
			return new TransAlien(new Point((int)X, (int)Y), Model, Angle, Radius);
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

        public float GetAngle()
        {
            return Angle;
        }

		public virtual TransInfo GetInfo()
		{
			string firstinfo, secondinfo;

            string engineState = "";
            if (EngineTurnOn)
                engineState = "Вкл";
            else
                engineState = "Выкл";

			double v = Power / Mass;
            firstinfo = "Прочность : " + HitPoints.ToString() + '\n';
			secondinfo = "Масса - " + Mass.ToString() + '\n';
            secondinfo += "Двигатель - " + engineState + '\n';
            secondinfo += "Мощность двигателя - " + ((int)(Power * 100.0f)+1).ToString() + " из " + ((int)(MaxPower * 100.0f)).ToString() + '\n';
            secondinfo += "Скорость - " + (((int)(v * 100)) + 1).ToString() + '\n';
            secondinfo += "Курс - " + ((int)((180.0 * (double)Angle) / Math.PI) + 1).ToString() + "°" + '\n';
            secondinfo += "Координаты - (" + ((int)(X)).ToString() + "; " + ((int)(Y)).ToString() + ")" + '\n';
            secondinfo += "Цель - (" + ((int)(Target.X)).ToString() + "; " + ((int)(Target.Y)).ToString() + ")" + '\n';
            List<int> tind = new List<int>();
			tind.Add(Model.X);
			tind.Add(Model.Y);
			return new TransInfo(ObjectType, firstinfo, secondinfo, Radius, false, tind, ObjectType, new Point((int)X, (int)Y));
		}
	}    
}
