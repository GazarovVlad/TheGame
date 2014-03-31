using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Microsoft.DirectX.Direct3D;
using FisicalObjects.Transist;
using GraphicObjects.ExplosionsSpace;

namespace TheGameDrawing.DrawingSpace
{
    static class Firing
    {
		private static Random Rand = new Random();

        public static void DrawFire(Point pos, Point targ, TransFire fire)
        {
			if (fire.FireType == "LaserGreen")
            {
                LargeLaser(pos, targ, Color.GreenYellow, 1);
            }
			if (fire.FireType == "Automatic")
			{
				Automatic(new Point(pos.X, pos.Y), targ, Color.Yellow);
				Explosions.AddSpecial(fire.RodEffect, pos.X, pos.Y);
			}
        }

		private static void Automatic(Point pos, Point targ, Color color)
		{
			int x1, y1, x2, y2;
			if (pos.X < targ.X)
			{
				x1 = Rand.Next(pos.X, targ.X);
				x2 = Rand.Next(pos.X, targ.X);
			}
			else
			{
				x1 = Rand.Next(targ.X, pos.X);
				x2 = Rand.Next(targ.X, pos.X);
			}
			if (pos.X == targ.X)
			{
				if (pos.Y < targ.Y)
				{
					y1 = Rand.Next(pos.Y, targ.Y);
					y2 = Rand.Next(pos.Y, targ.Y);
				}
				else
				{
					y1 = Rand.Next(targ.Y, pos.Y);
					y2 = Rand.Next(targ.Y, pos.Y);
				}
			}
			else
			{
				double a, b;
				a = (targ.Y - pos.Y) / (1.0 * (targ.X - pos.X));
				b = pos.Y - a * pos.X;
				y1 = (int)(a * x1 + b);
				y2 = (int)(a * x2 + b);
			}
			CustomVertex.TransformedColored[] line = new CustomVertex.TransformedColored[2];
			line[0].Position = new Microsoft.DirectX.Vector4(x1, y1, 1, 1);
			line[0].Color = color.ToArgb();
			line[1].Position = new Microsoft.DirectX.Vector4(x2, y2, 1, 1);
			line[1].Color = color.ToArgb();
			Drawing.OurDevice.VertexFormat = CustomVertex.TransformedColored.Format;
			Drawing.OurDevice.DrawUserPrimitives(PrimitiveType.LineList, 1, line);
		}

        private static void LargeLaser(Point pos, Point targ, Color color, double size)
        {
            List<Point> list = new List<Point>();
            bool flag;
            for (double i = -size; i <= size; i += 0.5)
                for (double j = -size; j <= size; j += 0.5)
                {
                    if (Math.Sqrt(i * i + j * j) <= size)
                    {
                        Point offset = new Point((int)i, (int)j);
                        flag = true;
                        for (int k = 0; k < list.Count; k++)
                            if ((offset.X == list[k].X) && (offset.Y == list[k].Y))
                            {
                                flag = false;
                                break;
                            }
                        if (flag)
                            list.Add(offset);
                    }
                }
            CustomVertex.TransformedColored[] line = new CustomVertex.TransformedColored[2 * list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                line[2 * i].Position = new Microsoft.DirectX.Vector4(pos.X + list[i].X, pos.Y + list[i].Y, 1, 1);
                line[2 * i].Color = color.ToArgb();
                line[2 * i + 1].Position = new Microsoft.DirectX.Vector4(targ.X + list[i].X, targ.Y + list[i].X, 1, 1);
                line[2 * i + 1].Color = color.ToArgb();
            }
            Drawing.OurDevice.VertexFormat = CustomVertex.TransformedColored.Format;
            Drawing.OurDevice.DrawUserPrimitives(PrimitiveType.LineList, list.Count, line);
        }
    }
}
