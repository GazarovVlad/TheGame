using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FisicalObjects.Transist;

namespace FisicalObjects.Cosmos.Minerals.Base
{
	class Mineral
	{
		public const string ObjectType = "Mineral";
		public const string NullExplosion = "Null";	// Значит минерал не унечтожается по истощению ресурса

		public Point Position { get; private set; }		// Координаты в мире
		public int Radius { get; private set; }		// Радиус
		public int TexMain { get; private set; }	// Тип текстур минерала (какой группе принадлежит)
		public int Resource { get; private set; }	// Количество минерала, которое можно добыть
		public string Explosion { get; private set; }

		private float Angle;		// Случайный угол поворота текстуры
		private int TexType;		// Конкретная цепочка текстур (из данной группы какой конкретно тип)
		private int TexStage;		// Текстура в данной цепочке текстур (конкретная позиция текстуры в типе)
		private static int[][] TexStages;					// [40][200][500][900][12000], res=746 => tn=3
		private static Random Rand = new Random();

		public Mineral(int res, Point pos, int rad, int texMain, int texTipe, string explosion)
		{
			Resource = res;
			Position = pos;
			Radius = rad;
			TexMain = texMain;
			TexType = texTipe;
			Explosion = explosion;
			TexStage = TexStages[TexMain].Length;
			while ((TexStage != 0) && (TexStages[TexMain][TexStage - 1] > Resource))
				TexStage--;
			Angle = (float)(Rand.Next(0, 360) * (Math.PI / 180.0));
		}

		public static void SetStages(int[][] texStages)
		{
			TexStages = texStages;
		}

		public int Mine(int demage)
		{
			Resource -= demage;
			while ((TexStage != 0) && (TexStages[TexMain][TexStage - 1] > Resource))
				TexStage--;
			if (Resource > 0)
				return demage;
			else
			{
				int res = demage + Resource;
				Resource = 0;
				return res;
			}
		}

		public TransMineral GetTransist()
		{
			return new TransMineral(Position, TexMain, TexType, TexStage, Angle, Radius);
		}

		public TransInfo GetInfo()
		{
			string firstinfo, secondinfo;
			firstinfo = "Минералы : " + Resource.ToString();
			secondinfo = "";
			List<int> tind = new List<int>();
			tind.Add(TexMain);
			tind.Add(TexType);
			tind.Add(TexStage);
			return new TransInfo(ObjectType, firstinfo, secondinfo, Radius, false, tind, ObjectType, Position);
		}
	}
}
