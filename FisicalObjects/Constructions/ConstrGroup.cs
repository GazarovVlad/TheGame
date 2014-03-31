using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FisicalObjects.Transist;
using GraphicObjects.ExplosionsSpace;
using FisicalObjects.Cosmos.Minerals;
using FisicalObjects.Cosmos.Asteroids;

namespace FisicalObjects.Constructions
{
    static class ConstrGroup
    {
        public static List<int[]> Ways { get; private set; }    //  pozMas/pozList pozMas/pozList
		public static List<Body>[] Mas { get; private set; }
        public static Body[] InfoMas { get; private set; }
		public static ConstrOption[] ConstrInfo { get; private set; }
		public static List<int> PowerList { get; private set; }

		private const double ExplCoef1 = 1;
		private const int ExplCoef2 = 5;

		private static bool ExplosionsExist;
		private static Body Ref;

        public const int RadVisibl = 110;

		public static void Inicialize()
		{
			ConstrLoder.LoadDataFromFile();
			InfoMas = ConstrLoder.InfoMas;
			ConstrInfo = ConstrLoder.ConstrInfo;
		}

        public static void CreateWorld()
        {
			Ref = null;
			ExplosionsExist = false;
            Ways = new List<int[]>();
            Mas = new List<Body>[InfoMas.Length];
			PowerList = new List<int>();
			int[] temp = new int[100];
			for (int i = 0; i < temp.Length; i++)
				temp[i] = 0;
			PowerList.AddRange(temp);
			for (int i = 0; i < Mas.Length; i++)
				Mas[i] = new List<Body>();
        }

		public static void SkipRef()
		{
			Ref = null;
		}

		public static TransInfo ChekRef()
		{
			if (Ref == null)
				return null;
			for (int i=0; i<Mas.Length; i++)
			for (int j=0; j<Mas[i].Count; j++)
				if (Ref.Equals(Mas[i][j]))
				{
					string name = ConstrInfo[i].Name;
					int price, maxhp, maxslots;
					price = ConstrInfo[i].Price;
					maxhp = ConstrInfo[i].MaxHP;
					maxslots = ConstrInfo[i].MaxSlots;
					return (Ref as IBody).GetInfo(name, maxhp, maxslots, price);
				}
			return null;
		}

        public static void DestroyWorld()
        {
            Ways.Clear();
			PowerList.Clear();
			for (int i = 0; i < Mas.Length; i++)
				Mas[i].Clear();
		}

		public static TransBuilding[] GetTransBuildings()
		{
			int count = 0;
			for (int i = 0; i < InfoMas.Length; i++)
				if (ConstrInfo[i].Type == "Building")
					count += Mas[i].Count;
			TransBuilding[] buildings = new TransBuilding[count];
			count = 0;
			for (int i = 0; i < InfoMas.Length; i++)
				if (ConstrInfo[i].Type == "Building")
					for (int j = 0; j < Mas[i].Count; j++)
					{
						buildings[count] = (Mas[i][j] as IBody).GetTransister() as TransBuilding;
						count++;
					}
			return buildings;
		}

		public static TransTower[] GetTransTowers()
		{
			int count = 0;
			for (int i = 0; i < InfoMas.Length; i++)
				if (ConstrInfo[i].Type == "Tower")
					count += Mas[i].Count;
			TransTower[] towers = new TransTower[count];
			count = 0;
			for (int i = 0; i < InfoMas.Length; i++)
				if (ConstrInfo[i].Type == "Tower")
					for (int j = 0; j < Mas[i].Count; j++)
					{
						towers[count] = (Mas[i][j] as IBody).GetTransister() as TransTower;
						count++;
					}
			return towers;
		}

		public static List<TransFire> GetTransFire()
		{
			TransFire temp;
			List<TransFire> data = new List<TransFire>();
			for (int i = 0; i < Mas.Length; i++)
				for (int j = 0; j < Mas[i].Count; j++)
				{
					temp = (Mas[i][j] as IBody).GetFireTransister();
					if (temp != null)
						data.Add(temp);
				}
			return data;
		}

		public static ConstrOption[] GetConstrInfo()
		{
			return ConstrInfo;
		}

		public static TransWay[] GetTransWays()
		{
			TransWay[] ways = new TransWay[Ways.Count];
			for (int i = 0; i < ways.Length; i++)
			{
				int x1 = Mas[Ways[i][0]][Ways[i][1]].X;
				int y1 = Mas[Ways[i][0]][Ways[i][1]].Y;
				int x2 = Mas[Ways[i][2]][Ways[i][3]].X;
				int y2 = Mas[Ways[i][2]][Ways[i][3]].Y;
				if (Mas[Ways[i][0]][Ways[i][1]].Powered)
					ways[i] = new TransWay(x1, y1, x2, y2, Color.Green);
				else
					ways[i] = new TransWay(x1, y1, x2, y2, Color.HotPink);
			}
			return ways;
		}

		public static TransConstrInfo[] GetTransConstrInfo()
		{
			TransConstrInfo[] info = new TransConstrInfo[InfoMas.Length];
			for (int i = 0; i < info.Length; i++)
				info[i] = new TransConstrInfo(ConstrInfo[i].Name, i, ConstrInfo[i].IconIndex, ConstrInfo[i].FireRang, ConstrInfo[i].RadSize);
			return info;
		}

		public static TransInfo GetTransInfo(Point constr)
		{
			string name = ConstrInfo[constr.X].Name;
			int price, maxhp, maxslots;
			price = ConstrInfo[constr.X].Price;
			maxhp = ConstrInfo[constr.X].MaxHP;
			maxslots = ConstrInfo[constr.X].MaxSlots;
			return (Mas[constr.X][constr.Y] as IBody).GetInfo(name, maxhp, maxslots, price);
		}
		
		public static int GetConstrPrice(int type)
		{
			return ConstrInfo[type].Price;
		}

		public static List<int> GetFireRangs()
		{
			List<int> data = new List<int>();
			for (int i = 0; i < ConstrInfo.Length; i++)
			{
				if (ConstrInfo[i].FireRang > 0)
				{
					bool flag = true;
					for (int j = 0; j < data.Count; j++)
						if (data[j] == ConstrInfo[i].FireRang)
						{
							flag = false;
							break;
						}
					if (flag)
						data.Add(ConstrInfo[i].FireRang);
				}
			}
			return data;
		}

		public static List<TransWay> GetFutureWays(int type, int xx, int yy)
		{
			List<TransWay> ways = new List<TransWay>();
			Body el = (InfoMas[type] as IBody).Create(0, xx, yy, ConstrInfo[type].MaxHP);
			int x2, y2, r;
			int[] temp;
			List<int[]> tW = new List<int[]>();
			int slots = (el as Body).Slots;
			int x = (el as Body).X;
			int y = (el as Body).Y;
			for (int i = 0; i < Mas.Length; i++)
				for (int j = 0; j < Mas[i].Count; j++)
				{
					if ((ConstrInfo[type].Connecting) || (ConstrInfo[i].Connecting))
					{
						x2 = Mas[i][j].X;
						y2 = Mas[i][j].Y;
						r = (int)Math.Sqrt((x - x2) * (x - x2) + (y - y2) * (y - y2));
						if ((Mas[i][j].Slots < ConstrInfo[i].MaxSlots) && (RadVisibl >= r))
						{
							if (ChekWay(x, y, x2, y2))
							{
								temp = new int[3];
								temp[0] = i;
								temp[1] = j;
								temp[2] = r;
								tW.Add(temp);
							}
						}
					}
				}
			for (int i = 0; i < tW.Count - 1; i++)
				for (int j = i + 1; j < tW.Count; j++)
				{
					if (tW[i][2] > tW[j][2])
					{
						temp = tW[i];
						tW[i] = tW[j];
						tW[j] = temp;
					}
				}
			for (int i = 0; (i < tW.Count) && (slots < ConstrInfo[type].MaxSlots); i++)
			{
				slots++;
				ways.Add(new TransWay(el.X, el.Y, Mas[tW[i][0]][tW[i][1]].X, Mas[tW[i][0]][tW[i][1]].Y, Color.Green));
			}
			return ways;
		}

		public static int[] GetConstr(int x, int y)   //  если нету - int[1], иначе - [type==pozMas, pozList]
		{
			int[] rez = { -1 };
			int x1, y1, r1;
			for (int i = 0; (i < Mas.Length) && (rez.Length == 1); i++)
				for (int j = 0; (j < Mas[i].Count) && (rez.Length == 1); j++)
				{
					x1 = Mas[i][j].X;
					y1 = Mas[i][j].Y;
					r1 = ConstrInfo[i].RadSize;
					if (r1 >= (int)Math.Sqrt((x1 - x) * (x1 - x) + (y1 - y) * (y1 - y)))
					{
						rez = new int[2];
						rez[0] = i;
						rez[1] = j;
						Ref = Mas[i][j];
					}
				}
			return rez;
		}

		public static void DoActions()
		{
			for (int i = 0; i < Mas.Length; i++)
				for (int j = 0; j < Mas[i].Count; j++)
					(Mas[i][j] as IBody).DoAction();
		}

		public static void CalibrateEnergy()
		{
			for (int i = 0; i < PowerList.Count; i++)
				PowerList[i] = 0;
			for (int i = 0; i < Mas.Length; i++)
				for (int j = 0; j < Mas[i].Count; j++)
					(Mas[i][j] as IBody).CalibrateEnergy();
		}

		public static void EndActions()
		{
			do
			{
				ExplosionsExist = false;
				for (int i = 0; i < Mas.Length; i++)
					for (int j = Mas[i].Count - 1; j >= 0; j--)
					{
						if (Mas[i][j].HitPoints <= 0)
							Destroy(i, j);
						else
							(Mas[i][j] as IBody).EndAction();
					}
			}
			while (ExplosionsExist);
		}

        public static bool Add(int type, Point poz)
        {
			if (ChekPlace(ConstrInfo[type].RadSize, poz.X, poz.Y))
            {
				Body newEl = (InfoMas[type] as IBody).Create(Mas[type].Count, poz.X, poz.Y, ConstrInfo[type].MaxHP);
				newEl.Slots = RebildWay(newEl, type);
                Mas[type].Add(newEl);
                RebildGroups();
				return true;
            }
            return false;
        }

        public static void Destroy(int pozMas, int pozList)
		{
			if (Ref != null)
			{
				if (Ref.Equals(Mas[pozMas][pozList]))
					Ref = null;
			}
			ExplosionsExist = true;
			Explosions.Add(ConstrInfo[pozMas].ExplType, Mas[pozMas][pozList].X, Mas[pozMas][pozList].Y);
			AsterField.Explode(new Point(Mas[pozMas][pozList].X, Mas[pozMas][pozList].Y), ConstrInfo[pozMas].ExplForse);
			Explode(pozMas, pozList);
            List<int[]> tW = new List<int[]>();
            List<int> removing = new List<int>();
            for (int i = 0; i < Ways.Count; i++)
            {
                if ((Ways[i][0] == pozMas) && (Ways[i][1] == pozList))
                {
                    int[] poz = { Ways[i][2], Ways[i][3] };
                    tW.Add(poz);
                    Mas[poz[0]][poz[1]].Slots--;
                    removing.Add(i);
                }
                if ((Ways[i][2] == pozMas) && (Ways[i][3] == pozList))
                {
                    int[] poz = { Ways[i][0], Ways[i][1] };
                    tW.Add(poz);
                    Mas[poz[0]][poz[1]].Slots--;
                    removing.Add(i);
                }
            }
            for (int i = removing.Count - 1; i >= 0; i--)
                Ways.RemoveAt(removing[i]);

            for (int i = pozList+1; i < Mas[pozMas].Count; i++)
                Mas[pozMas][i].PozList--;
            for (int i = 0; i < Ways.Count; i++)
            {
                if ((Ways[i][0] == pozMas) && (Ways[i][1] > pozList))
                    Ways[i][1]--;
                if ((Ways[i][2] == pozMas) && (Ways[i][3] > pozList))
                    Ways[i][3]--;
            }
            Mas[pozMas].RemoveAt(pozList);
            for (int i = 0; i < tW.Count; i++)
            {
                if ((tW[i][0] == pozMas) && (tW[i][1] > pozList))
                    tW[i][1]--;
				Mas[tW[i][0]][tW[i][1]].Slots = RebildWay(Mas[tW[i][0]][tW[i][1]], tW[i][0]);
            }
            RebildGroups();
        }

        public static bool ChekPlace(int rad, int x, int y) //  проверяет: не пересекает ли постройка другие постройки и линии связи
        {
			if (Earth.IsClash(new Point(x, y), rad))
				return false;
			if (!MineGroup.ChekPlace(new Point(x, y), rad))
				return false;
            bool flag = true;
            {
                int x1, x2, y1, y2, r2;
                x1 = x;
                y1 = y;
                for (int i = 0; (i < Mas.Length) && (flag); i++)
                    for (int j = 0; (j < Mas[i].Count) && (flag); j++)
                    {
                        x2 = Mas[i][j].X;
                        y2 = Mas[i][j].Y;
						r2 = ConstrInfo[i].RadSize;
                        if (rad + r2 >= (int)Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2)))
                        {
                            flag = false;
                        }
                    }
            }
            if (flag)
            {
                int x1, x2, y1, y2;
                double r1, r2, l, a, b, c, t, rr;
                for (int i = 0; i < Ways.Count; i++)
                {
                    x1 = Mas[Ways[i][0]][Ways[i][1]].X;
                    y1 = Mas[Ways[i][0]][Ways[i][1]].Y;
                    x2 = Mas[Ways[i][2]][Ways[i][3]].X;
                    y2 = Mas[Ways[i][2]][Ways[i][3]].Y;
                    l = Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
                    r1 = Math.Sqrt((x - x1) * (x - x1) + (y - y1) * (y - y1));
                    r2 = Math.Sqrt((x2 - x) * (x2 - x) + (y2 - y) * (y2 - y));
                    if ((r1 <= rad) || (r2 <= rad))
                        flag = false;
                    else
                    {
                        if ((r1 >= Math.Sqrt(r2 * r2 + l * l)) || (r2 >= Math.Sqrt(r1 * r1 + l * l)))
                        {
                            if (r1 < r2)
                                rr = r1;
                            else
                                rr = r2;
                        }
                        else
                        {
                            a = y2 - y1;
                            b = x1 - x2;
                            c = -x1 * (y2 - y1) + y1 * (x2 - x1);
                            t = Math.Sqrt(a * a + b * b);
                            if (c > 0)
                            {
                                a *= -1;
                                b *= -1;
                                c *= -1;
                            }
                            rr = (a * x + b * y + c) / t;
                            if (rr < 0)
                                rr *= -1;
                        }
                        if (rr <= rad)
                            flag = false;
                    }
                }
            }
            return flag;
		}

		public static bool ChekPlace(int type, Point poz)
		{
			return ChekPlace(ConstrInfo[type].RadSize, poz.X, poz.Y);
		}

        public static bool ChekWay(int x1, int y1, int x2, int y2) //  проверяет: не пересекает ли путь другие постройки
        {
            bool flag = true;
            int x, y, r;
            double r1, r2, l, a, b, c, t, rr;
            l = Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
            for (int i = 0; (i < Mas.Length) && (flag); i++)
                for (int j = 0; (j < Mas[i].Count) && (flag); j++)
                {
                    x = Mas[i][j].X;
                    y = Mas[i][j].Y;
					r = ConstrInfo[i].RadSize;
                    if (((x != x1) || (y != y1)) && ((x != x2) || (y != y2)))
                    {
                        r1 = Math.Sqrt((x - x1) * (x - x1) + (y - y1) * (y - y1));
                        r2 = Math.Sqrt((x2 - x) * (x2 - x) + (y2 - y) * (y2 - y));
                        if ((r1 <= r) || (r2 <= r))
                            flag = false;
                        else
                        {
                            if ((r1 >= Math.Sqrt(r2 * r2 + l * l)) || (r2 >= Math.Sqrt(r1 * r1 + l * l)))
                            {
                                if (r1 < r2)
                                    rr = r1;
                                else
                                    rr = r2;
                            }
                            else
                            {
                                a = y2 - y1;
                                b = x1 - x2;
                                c = -x1 * (y2 - y1) + y1 * (x2 - x1);
                                t = Math.Sqrt(a * a + b * b);
                                if (c > 0)
                                {
                                    a *= -1;
                                    b *= -1;
                                    c *= -1;
                                }
                                rr = (a * x + b * y + c) / t;
                                if (rr < 0)
                                    rr *= -1;
                            }
                            if (rr <= r)
                                flag = false;
                        }
                    }
                }
            return flag;
        }

		private static void Explode(int pm, int pl)
		{
			int x1, x2, y1, y2, r1, r2;
			x1 = Mas[pm][pl].X;
			y1 = Mas[pm][pl].Y;
			r1 = Mas[pm][pl].Radius;
			for (int i = 0; i < Mas.Length; i++)
				for (int j = 0; j < Mas[i].Count; j++)
					if ((i != pm) || (j != pl))
					{
						x2 = Mas[i][j].X;
						y2 = Mas[i][j].Y;
						r2 = ConstrInfo[i].RadSize;
						double length = Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
						if (ConstrInfo[pm].ExplForse + r2 * ExplCoef1 > length)
							(Mas[i][j] as IBody).ExplodeHit(ExplCoef2 * ConstrInfo[pm].ExplForse);
					}
		}

        private static int RebildWay(Body el, int type) //  Соеденяет постройку путями, если возможно, существующие пути не трогает
        {
            int x2, y2, r;
            int[] temp;
            List<int[]> tW = new List<int[]>();
            int slots = el.Slots;
            int x = el.X;
            int y = el.Y;
            for (int i = 0; i < Mas.Length; i++)
                for (int j = 0; j < Mas[i].Count; j++)
                {
                    if ((i != type) || (j != el.PozList))
                    {
						if ((ConstrInfo[type].Connecting) || (ConstrInfo[i].Connecting))
						{
							x2 = Mas[i][j].X;
							y2 = Mas[i][j].Y;
							r = (int)Math.Sqrt((x - x2) * (x - x2) + (y - y2) * (y - y2));
							if ((Mas[i][j].Slots < ConstrInfo[i].MaxSlots) && (RadVisibl >= r))
							{
								bool flag = true;
								for (int k = 0; k < Ways.Count; k++)
								{
									if ((Ways[k][0] == i) && (Ways[k][1] == j))
									{
										if ((Ways[k][2] == type) && (Ways[k][3] == el.PozList))
											flag = false;
									}
									if ((Ways[k][2] == i) && (Ways[k][3] == j))
									{
										if ((Ways[k][0] == type) && (Ways[k][1] == el.PozList))
											flag = false;
									}
								}
								if ((flag) && (ChekWay(x, y, x2, y2)))
								{
									temp = new int[3];
									temp[0] = i;
									temp[1] = j;
									temp[2] = r;
									tW.Add(temp);
								}
							}
						}
                    }
                }
            for (int i = 0; i < tW.Count - 1; i++)
                for (int j = i + 1; j < tW.Count; j++)
                {
                    if (tW[i][2] > tW[j][2])
                    {
                        temp = tW[i];
                        tW[i] = tW[j];
                        tW[j] = temp;
                    }
                }
			for (int i = 0; (i < tW.Count) && (slots < ConstrInfo[type].MaxSlots); i++)
            {
                Mas[tW[i][0]][tW[i][1]].Slots++;
                slots++;
                temp = new int[4];
                temp[0] = type;
                temp[1] = el.PozList;
                temp[2] = tW[i][0];
                temp[3] = tW[i][1];
                Ways.Add(temp);
            }
            return slots;
        }

        private static void RebildGroups()      //  Наново формирует группы среди построек, опираясь на существующие пути
        {
            int wNewMas, wNewList;
            int tGroup = -1;
            int stekN = 0, stekT;
            int[][] stek = new int[2][];
            for (int i = 0; i < Mas.Length; i++)
                stekN += Mas[i].Count;
            stek[0] = new int[stekN+1];
            stek[1] = new int[stekN + 1];
            for (int i = 0; i < stek[0].Length; i++)
            {
                stek[0][i] = 0;
                stek[1][i] = 0;
            }
            stekN = 0;
            for (int i = 0; i < Mas.Length; i++)
                for (int j = 0; j < Mas[i].Count; j++)
                    Mas[i][j].Mark = true;
			PowerList.Clear();
            for (int i = 0; i < Mas.Length; i++)
                for (int j = 0; j < Mas[i].Count; j++)
                    if (Mas[i][j].Mark)
                    {
                        tGroup++;
                        Mas[i][j].Mark = false;
                        Mas[i][j].Group = tGroup;
						while (PowerList.Count <= tGroup)
							PowerList.Add(0);
                        stek[0][0] = i;
                        stek[1][0] = j;
                        stekN++;
                        stekT = 0;
                        while (stekN > 0)
                        {
                            for (int k = 0; k < Ways.Count; k++)
                            {
                                wNewMas = -1;
                                wNewList = -1;
                                if ((Ways[k][0] == stek[0][stekT]) && (Ways[k][1] == stek[1][stekT]))
                                {
                                    wNewMas = Ways[k][2];
                                    wNewList = Ways[k][3];
                                }
                                if ((Ways[k][2] == stek[0][stekT]) && (Ways[k][3] == stek[1][stekT]))
                                {
                                    wNewMas = Ways[k][0];
                                    wNewList = Ways[k][1];
                                }
                                if ((wNewMas != -1) && (Mas[wNewMas][wNewList].Mark))
                                {
                                    Mas[wNewMas][wNewList].Mark = false;
                                    Mas[wNewMas][wNewList].Group = tGroup;
                                    stek[0][stekN] = wNewMas;
                                    stek[1][stekN] = wNewList;
                                    stekN++;
                                }
                            }
                            for (int k = stekT; k < stekN; k++)
                            {
                                stek[0][k] = stek[0][k+1];
                                stek[1][k] = stek[1][k+1];
                            }
                            stekN--;
                            stekT = stekN - 1;
                        }
                    }
        }
    }
}
