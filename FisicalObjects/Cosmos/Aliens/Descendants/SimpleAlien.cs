using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FisicalObjects.Cosmos.Aliens.Base;
using FisicalObjects.Transist;

namespace FisicalObjects.Cosmos.Aliens.Descendants
{
    class SimpleAlien : Alien
    {
        //изменение параметров на 1 такте
        private const float Retarder = 0.001f; //замедлитель
        private const float MaxDeltaPower = 0.008f;
        private const float MaxDeltaAngle = 0.01f;

        private static Point Center;//для задания случайной точки над Землей
        private double EndA;//курс, на который необходимо выйти (зависит от положения корабля и цели)
        private bool CourseIsTrue = false; //вышли на курс? (да -> перерасчет не треб-ся)

        //коррекция курса каждые CourseCorrFromCount тактов
        private const int CourseCorrectionInterval = 300;
        private int CourseCorrFromCount;

        //рандомное время (в тактах) между включениями и выключениями двигателя
        private int EngineInterval;
        private const int RandEngStart = 150;
        private const int RandEngEnd = 500;

        //выбранная траектория разворота 
        private static int ChangedTrajectTurn; //(-1 не выбрана, 1 - первая четверть и т.д. см. FindSpeed() )                                                                                   

        public static void Inicialize(int mapsize)
        {
            Center = new Point(mapsize / 2, mapsize / 2);
        }

        public SimpleAlien(Alien body)
        {
            Angle = body.Angle;
            HitPoints = body.HitPoints;
            Mass = body.Mass;
            Model = body.Model;
            Radius = body.Radius;
            VX = body.VX;
            VY = body.VY;
            X = body.X;
            Y = body.Y;
            Type = body.Type;
            ClashDistance = body.ClashDistance;
            Explodes = true;
            MaxPower = body.MaxPower;
            Power = body.Power;
            ChangeCourse(RandPlanetPoint());
            CourseCorrFromCount = CourseCorrectionInterval;
        }

        public override void Move()
        {
            ////вышли на курс?             
            if (Math.Abs(Angle - EndA) < (1.5f * MaxDeltaAngle))
                CourseIsTrue = true;
            else
                CourseIsTrue = false;

            //прирост курса, если еще не вышли
            if (!CourseIsTrue)
            {
                //учитываем, в какую сторону "ближе"
                float delta = (float)EndA - Angle;
                if (delta < 0) delta += (float)(2.0 * Math.PI);
                if (delta < Math.PI) Angle += MaxDeltaAngle;
                else Angle -= MaxDeltaAngle;

                ChangeCourse(null);//связано с постепенным выходом корабля на курс -> меняются координаты корабля -> меняется угол между кораблем и целью (т.е. нужен пересчет)
            }

            //изменение курса через каждые CourseCorrectionInterval тактов - рандомная точка в пределах планеты 
            //(можно удалить) 
            if (CourseCorrFromCount == 0)
            {
                ChangeCourse(RandPlanetPoint());
                CourseCorrFromCount = CourseCorrectionInterval;
            }
            CourseCorrFromCount--;

            //перерасчет текущей мощности двигателя
            if (Power - Retarder >= 0)
                Power -= Retarder;    //замедление
            if (EngineTurnOn) //если двигатель включен
                if (Power + MaxDeltaPower <= MaxPower)
                    Power += MaxDeltaPower;

            //случайное включение/выключение двигателя
            if (EngineInterval == 0)
            {
                EngineInterval = Rand.Next(RandEngStart, RandEngEnd);
                EngineTurnOn = !EngineTurnOn;
            }
            EngineInterval--;

            //расчет скорости и координат
            double v = Power / Mass;
            double vx = 0, vy = 0;
            FindSpeed(ref vx, ref vy, v);
            VX = (float)vx;
            VY = (float)vy;
            X += VX;
            Y += VY;

            //if (Earth.IsClash(new Point((int)X, (int)Y), Radius, ClashDistance))
            //{
            //    Earth.Demage(Mass, new Point((int)X, (int)Y));
            //    HitPoints = 0;
            //    Explodes = false;
            //}
        }

        private Point RandPlanetPoint()
        {
            int limit = 200;
            int x = Rand.Next(Center.X - limit, Center.X + limit);
            int y = Rand.Next(Center.Y - limit, Center.Y + limit);
            return new Point(x, y);
        }

        private void FindSpeed(ref double vx, ref double vy, double v)
        {
            if (ChangedTrajectTurn == -1)
            {
                if (X <= Target.X && Y <= Target.Y) ChangedTrajectTurn = 1; //первая четверть плоскости мира
                if (X > Target.X && Y < Target.Y) ChangedTrajectTurn = 2; //и т.д.               
                if (X < Target.X && Y > Target.Y) ChangedTrajectTurn = 3;
                if (X >= Target.X && Y >= Target.Y) ChangedTrajectTurn = 4;
            }
            switch (ChangedTrajectTurn)
            {
                case 1:
                    {
                        double a = Angle - 0.5 * Math.PI;
                        vx = v * Math.Cos(a);
                        vy = v * Math.Sin(a);
                        break;
                    }
                case 2:
                    {
                        double a = 1.5 * Math.PI - Angle;
                        vx = -v * Math.Cos(a);
                        vy = v * Math.Sin(a);
                        break;
                    }
                case 3:
                    {
                        double a = 0.5 * Math.PI - Angle;
                        vx = v * Math.Cos(a);
                        vy = -v * Math.Sin(a);
                        break;
                    }
                case 4:
                    {
                        double a = Angle - 1.5 * Math.PI;
                        vx = -v * Math.Cos(a);
                        vy = -v * Math.Sin(a);
                        break;
                    }
            }
        }

        public void ChangeCourse(Point? newTarget)
        {
            if (newTarget.HasValue)//задана новая цель ?
            {
                ChangedTrajectTurn = -1; //устанавливаем, что траектория разворота не выбрана
                Target.X = newTarget.Value.X;
                Target.Y = newTarget.Value.Y;
            }

            Point A = new Point((int)X, (int)Y); //положение корабля в пространстве
            Point B = new Point(Target.X, (int)Y); //для "создания" прямоугольного треугольника
            Point C = new Point(Target.X, Target.Y); //положение цели

            Line BC = new Line(B, C);
            Line AC = new Line(A, C);

            //перерасчет EndA = arcsin BC/AC , где A - корабль, С - Цель
            double a = Math.Asin((BC.Lenght / AC.Lenght));
            if (X <= Target.X && Y < Target.Y) { EndA = a + Math.PI / 2.0; return; }
            if (X > Target.X && Y < Target.Y) { EndA = 3.0 * Math.PI / 2.0 - a; return; }
            if (X < Target.X && Y > Target.Y) { EndA = Math.PI / 2.0 - a; return; }
            if (X >= Target.X && Y >= Target.Y) { EndA = 3.0 * Math.PI / 2.0 + a; return; }
        }
    }

    class Line
    {
        private Point A;
        private Point B;
        public double Lenght
        {
            get { return Math.Sqrt((B.X - A.X) * (B.X - A.X) + (B.Y - A.Y) * (B.Y - A.Y)); }
        }

        public Line(Point a, Point b)
        {
            A.X = a.X; A.Y = a.Y;
            B.X = b.X; B.Y = b.Y;
        }
    }

}