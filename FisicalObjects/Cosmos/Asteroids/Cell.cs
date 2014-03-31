using System;
using System.Drawing;
using System.Collections.Generic;
using FisicalObjects.Cosmos.Asteroids.Base;
using FisicalObjects.Cosmos.Asteroids.Descendants;

namespace FisicalObjects.Cosmos.Asteroids
{
    class Cell
	{
		public const int Size = 150;						// Размер ячейки (длинна стороны)

		public static Point MaxInd { get; private set; }	// Количество строк и столбцов в "сетке"

		public Point Index { get; private set; }			// Центр ячейки 4*4*4*4*4*3 = 12*256
		public List<int> AsterLinks { get; private set; }
		
		public Cell(Point index)
		{
			Index = index;
			AsterLinks = new List<int>();
		}

		public void ClearAsterLinks()
		{
			AsterLinks.Clear();
		}

		public void AddAsterLink(int ind)
		{
			AsterLinks.Add(ind);
		}

		public static void Inicialize(int worldsize)
		{
			MaxInd = new Point(worldsize / Size, worldsize / Size);
		}

		public static Point GetCellIndex(Point pos)
		{
			int i = pos.X / Size;
			int j = pos.Y / Size;
			if (i < 0) i = 0;
			if (j < 0) j = 0;
			if (i > MaxInd.X) i = MaxInd.X;
			if (j > MaxInd.Y) j = MaxInd.Y;
			return new Point(i, j);
		}

		public static List<Point> GetMeaningfulCells(Point pos, int rad)
		{
			List<Point> cells = new List<Point>();
			int i1, i2, j1, j2;
			i1 = (pos.X - rad) / Size;
			i2 = (pos.X + rad) / Size;
			j1 = (pos.Y - rad) / Size;
			j2 = (pos.Y + rad) / Size;
			if (i1 < 0) i1 = 0;
			if (j1 < 0) j1 = 0;
			if (i2 < 0) i2 = 0;
			if (j2 < 0) j2 = 0;
			if (i2 > MaxInd.X) i2 = MaxInd.X;
			if (j2 > MaxInd.Y) j2 = MaxInd.Y;
			if (i1 > MaxInd.X) i1 = MaxInd.X;
			if (j1 > MaxInd.Y) j1 = MaxInd.Y;
			for (int i = i1; i <= i2; i++)
				for (int j = j1; j <= j2; j++)
					cells.Add(new Point(i, j));
			return cells;
		}
	}
}