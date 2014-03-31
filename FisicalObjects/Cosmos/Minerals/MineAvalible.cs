using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FisicalObjects.Cosmos.Minerals.Base;

namespace FisicalObjects.Cosmos.Minerals
{
	static class MineAvalible
	{
		public static string[] Types { get; private set; }
		public static int[] Counts { get; private set; }
		public static int[] Stages { get; private set; }
		public static int[] TexSizes { get; private set; }
		public static int[] RadSizes { get; private set; }
		public static string[] Explosions { get; private set; }
		public static int[][] ChangeStages { get; private set; }

		private static string Path = "Settings\\SpaceObjects.txt";
		private static string Starts = "Minerals:";
		private static string Ends = "End";
		private static Random Rand = new Random();

		private const int MassInResurse = 2;

		public static void Load()
		{
			Loder data = new Loder(Path, Starts, Ends);
			string[] temp;
			int id = -1;
			while (data.Next())
			{
				if (data.Key == "Types")
				{
					Types = data.Value.Split('/');
					ChangeStages = new int[Types.Length][];
				}
				if (data.Key == "Counts")
				{
					temp = data.Value.Split('/');
					Counts = new int[temp.Length];
					for (int i = 0; i < temp.Length; i++)
						Counts[i] = Convert.ToInt32(temp[i]);
				}
				if (data.Key == "Stages")
				{
					temp = data.Value.Split('/');
					Stages = new int[temp.Length];
					for (int i = 0; i < temp.Length; i++)
						Stages[i] = Convert.ToInt32(temp[i]);
				}
				if (data.Key == "TexureSizes")
				{
					temp = data.Value.Split('/');
					TexSizes = new int[temp.Length];
					for (int i = 0; i < temp.Length; i++)
						TexSizes[i] = Convert.ToInt32(temp[i]);
				}
				if (data.Key == "RadSizes")
				{
					temp = data.Value.Split('/');
					RadSizes = new int[temp.Length];
					for (int i = 0; i < temp.Length; i++)
						RadSizes[i] = Convert.ToInt32(temp[i]);
				}
				if (data.Key == "Explosions")
				{
					temp = data.Value.Split('/');
					Explosions = new string[temp.Length];
					for (int i = 0; i < temp.Length; i++)
						Explosions[i] = temp[i];
				}
				if (data.Key == "ID")
				{
					int j = 0;
					while (data.Value != Types[j])
						j++;
					id = j;
				}
				if (data.Key == "TextureCanges")
				{
					temp = data.Value.Split('/');
					ChangeStages[id] = new int[temp.Length];
					for (int i = 0; i < temp.Length; i++)
						ChangeStages[id][i] = Convert.ToInt32(temp[i]);
				}
			}
			data.EndReading();
			Mineral.SetStages(ChangeStages);
		}

		public static Mineral CreateMineral(Point poz, int mass)
		{
			int res = mass / MassInResurse;
			List<int> posibilities = new List<int>();
			List<int> less = new List<int>();
			List<int> more = new List<int>();
			for (int i = 0; i < Types.Length; i++)
				if ((res >= ChangeStages[i][ChangeStages[i].Length / 2]) && (res <= ChangeStages[i][ChangeStages[i].Length - 1]))
					posibilities.Add(i);
				else
				{
					if (res < ChangeStages[i][ChangeStages[i].Length / 2])
						more.Add(i);
					else
						less.Add(i);
				}
			if (posibilities.Count == 0)
			{
				int ind = 0, iless = 0, imore = 0;
				for (int i = 0; i < less.Count; i++)
					if (ChangeStages[less[iless]][ChangeStages[less[iless]].Length - 1] < ChangeStages[less[i]][ChangeStages[less[i]].Length - 1])	// самый большой из маленьких
						iless = i;
				for (int i = 0; i < more.Count; i++)
					if (ChangeStages[more[imore]][ChangeStages[more[imore]].Length - 1] > ChangeStages[more[i]][ChangeStages[more[i]].Length - 1])	// самый маленький из больших
						imore = i;
				if (less.Count == 0)
					ind = imore;
				else
				{
					if (more.Count == 0)
						ind = imore;
					else
					{
						int rless, rmore;
						rless = res - ChangeStages[less[iless]][ChangeStages[less[iless]].Length - 1];
						rmore = ChangeStages[more[imore]][ChangeStages[more[imore]].Length / 2] - res;
						if (rless < rmore)
							ind = less[iless];
						else
							ind = more[imore];
					}
				}
				return new Mineral(res, poz, RadSizes[ind], ind, Rand.Next(0, Counts[ind]), Explosions[ind]);
			}
			else
			{
				int ind = posibilities[Rand.Next(0, posibilities.Count)];
				return new Mineral(res, poz, RadSizes[ind], ind, Rand.Next(0, Counts[ind]), Explosions[ind]);
			}
		}

		public static Mineral CreateMineral(Point poz, string indifer)
		{
			int ind = 0,res;
			while (Types[ind] != indifer)
				ind++;
			res = ChangeStages[ind][ChangeStages[ind].Length - 1] + Rand.Next(ChangeStages[ind][0], ChangeStages[ind][ChangeStages[ind].Length / 3]);
			return new Mineral(res, poz, RadSizes[ind], ind, Rand.Next(0, Counts[ind]), Explosions[ind]);
		}

		public static int[] GetAllRads()
		{
			return RadSizes;
		}
	}
}
