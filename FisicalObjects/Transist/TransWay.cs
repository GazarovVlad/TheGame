using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace FisicalObjects.Transist
{
    public class TransWay
    {
        public Point A { get; private set; }
        public Point B { get; private set; }
		public Color Coloring { get; private set; }

        public TransWay(int x1, int y1, int x2, int y2, Color color)
        {
            A = new Point(x1, y1);
            B = new Point(x2, y2);
			Coloring = color;
        }
    }
}
