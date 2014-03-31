using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicObjects.StarsSpace
{
    public class Star
    {
        public int I;   //  Позиция в своем массиве текстур
        public double X;
        public double Y;

        private static int CentScreenPozX = (int)Stars.MapLen / 2;
        private static int CentScreenPozY = (int)Stars.MapLen / 2;

        private static Random rand = new Random();
        private double Speed;
        private double ZeroX;
        private double ZeroY;

        public Star(int i, double outMapRad, double xMin, double yMin, double xMax, double yMax)
        {
            double koef = rand.Next(Stars.KoefMin, Stars.KoefMax) / 1000.0;
            double max = CentScreenPozX;
            I = i;
            ZeroX = rand.Next((int)xMin, (int)xMax);
            ZeroY = rand.Next((int)yMin, (int)yMax);
            if (max < CentScreenPozY)
                max = CentScreenPozY;
            Speed = (outMapRad / max) * koef;
            ModifyPosition();
        }

        public void ModifyPosition()
        {
            X = ZeroX + (CentScreenPozX - Stars.Space.X) * Speed;
            Y = ZeroY + (CentScreenPozY - Stars.Space.Y) * Speed;
            X -= Stars.Space.X;
            Y -= Stars.Space.Y;
        }
    }
}
