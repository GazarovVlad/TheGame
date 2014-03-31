using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FisicalObjects.Cosmos.Minerals.Base;

namespace FisicalObjects.Cosmos.Minerals
{
	static class MineCluster
	{
		private const int WorldClasterIndent = 250;
		private const int WorldMineralIndent = 250;

		private static Random Rand = new Random();

		public static List<Mineral> Create(int world, int clusters, int clustersize, int resource, bool rconcentrated)
		{
			// Размер игрового мира
			// Радиус Земли
			// Приближонное кол-во скоплений минералов (домнажается на [3;5])
			// Приближонное кол-во ресурса на 1 скопление (домнажается на [3000;6500])
			// Сконцентрированы-ли миниралы скопления в его центре
			// Сконцентрирован-ли ресурс в самых больших минералах
			// Минимальная близость к Земле
			List<Mineral> minerals = new List<Mineral>();
			clusters *= Rand.Next(3, 6);
			for (int i = 0; i < clusters; i++)
			{
				 int res = resource * Rand.Next(9000, 19501);
				 minerals = CreateOneCluster(world, res, clustersize, rconcentrated, minerals);
			}
			return minerals;
		}

		private static List<Mineral> CreateOneCluster(int world, int resource, int size, bool rc, List<Mineral> minerals)
		{
			List<int> resources = Splite(resource, rc);
			int x = -1, y = -1;
			int earthindent = Earth.GetMineralMinRadius();
			Point earthpos = Earth.GetPosition();
			Point poz;
			while (!(((((x > WorldClasterIndent) && (x < earthpos.X - earthindent)) || ((x > earthpos.X + earthindent) && (x < world - WorldClasterIndent))) && (y > WorldClasterIndent) && (y < world - WorldClasterIndent)) ||
					((((y > WorldClasterIndent) && (y < earthpos.Y - earthindent)) || ((y > earthpos.Y + earthindent) && (y < world - WorldClasterIndent))) && (x > WorldClasterIndent) && (x < world - WorldClasterIndent))))
			{
				x = Rand.Next(1, world);
				y = Rand.Next(1, world);
			}
			poz = new Point(x, y);
			for (int i = 0; i < resources.Count; i++)
			{
				bool flag = false;
				Mineral mineral = null;
				while(!flag)
				{
					x = Rand.Next(poz.X - size, poz.X + size);
					y = Rand.Next(poz.Y - size, poz.Y + size);
					while (!(((((x > WorldMineralIndent) && (x < earthpos.X - earthindent)) || ((x > earthpos.X + earthindent) && (x < world - WorldMineralIndent))) && (y > WorldMineralIndent) && (y < world - WorldMineralIndent)) ||
							((((y > WorldMineralIndent) && (y < earthpos.Y - earthindent)) || ((y > earthpos.Y + earthindent) && (y < world - WorldMineralIndent))) && (x > WorldMineralIndent) && (x < world - WorldMineralIndent))))
					{
						x = Rand.Next(poz.X - size, poz.X + size);
						y = Rand.Next(poz.Y - size, poz.Y + size);
					}
					mineral = MineAvalible.CreateMineral(new Point(x,y),resources[i]);
					flag = true;
					for (int j = 0; (j < minerals.Count) && (flag); j++)
						if (minerals[j].Radius + mineral.Radius >= (int)Math.Sqrt((minerals[j].Position.X - mineral.Position.X) * (minerals[j].Position.X - mineral.Position.X) + (minerals[j].Position.Y - mineral.Position.Y) * (minerals[j].Position.Y - mineral.Position.Y)))
							flag = false;
				}
				minerals.Add(mineral);
			}
			return minerals;
		}

		private static List<int> Splite(int resource, bool rc)
		{
			List<int> resources = new List<int>();
			List<int> resmap = new List<int>();
			for (int i = 0; i < MineAvalible.ChangeStages.Length; i++)
				resmap.Add(MineAvalible.ChangeStages[i][MineAvalible.ChangeStages[i].Length - 1]);
			resmap.Sort();	// сортирует по возрастанию
			if (rc)
			{
				int ind = resmap.Count - 1;
				while (resource > 0)
				{
					while ((ind != -1) && (resmap[ind] * 1.4 > resource))
						ind--;
					if (ind == -1)
						resource = 0;
					else
					{
						resources.Add(Rand.Next((int)(resmap[ind] * 0.5), (int)(resmap[ind] * 1.1)));
						resource -= resources[resources.Count - 1];
					}
				}
			}
			else
			{
				int ind = 0;
				int steps;
				while (resource > 0)
				{
					steps = 31;
					do
					{
						steps--;
						ind = Rand.Next(0, resmap.Count);
					}
					while ((resmap[ind] > resource) && (steps > 0));
					if (steps == 0)
						resource = 0;
					else
					{
						resources.Add(Rand.Next((int)(resmap[ind] * 0.5), (int)(resmap[ind] * 1.1)));
						resource -= resources[resources.Count - 1];
					}
				}
			}
			return resources;
		}
	}
}
