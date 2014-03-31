using System;
using System.Collections.Generic;
using System.Text;
using Buildings;

namespace Constuctions
{
    static class BuildingGroup
    {
        public static List<int[]> Ways { get; private set; }    //  pozMas/pozList pozMas/pozList
        public static List<object>[] Mas { get; private set; }
        public static object[] InfoMas { get; private set; }

        public const int RadVisibl = 110;

        public static void CreateWorld()
        {
            Ways = new List<int[]>();
            Mas = new List<object>[2];
            Mas[0] = new List<object>();
            Mas[1] = new List<object>();
            InfoMas = new object[2];        //  !!! В начале buildings, а потом towers
            InfoMas[0] = new Building1(0, 0, 0, 0);
            InfoMas[1] = new Tower1(0, 0, 0, 0);
        }

        public static void DestroyWorld()
        {
            Ways.Clear();
        }

        public static bool Add(int type, int x, int y)
        {
            bool flag = ChekPlace(type, x, y);
            if (flag)
            {
                object newEl = (InfoMas[type] as IBody).Create(type, Mas[type].Count, x, y);
                (newEl as Body).Slots = RebildWay(newEl);
                Mas[type].Add(newEl);
                RebildGroups();
            }
            return flag;
        }

        public static void Delete(int pozMas, int pozList)
        {
            List<int[]> tW = new List<int[]>();
            List<int> removing = new List<int>();
            for (int i = 0; i < Ways.Count; i++)
            {
                if ((Ways[i][0] == pozMas) && (Ways[i][1] == pozList))
                {
                    int[] poz = { Ways[i][2], Ways[i][3] };
                    tW.Add(poz);
                    (Mas[poz[0]][poz[1]] as Body).Slots--;
                    removing.Add(i);
                }
                if ((Ways[i][2] == pozMas) && (Ways[i][3] == pozList))
                {
                    int[] poz = { Ways[i][0], Ways[i][1] };
                    tW.Add(poz);
                    (Mas[poz[0]][poz[1]] as Body).Slots--;
                    removing.Add(i);
                }
            }
            for (int i = removing.Count - 1; i >= 0; i--)
                Ways.RemoveAt(removing[i]);

            for (int i = pozList+1; i < Mas[pozMas].Count; i++)
                (Mas[pozMas][i] as Body).PozList--;
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
                (Mas[tW[i][0]][tW[i][1]] as Body).Slots = RebildWay(Mas[tW[i][0]][tW[i][1]]);
            }
            RebildGroups();
        }

        public static bool ChekPlace(int type, int x, int y) //  проверяет: не пересекает ли постройка другие постройки и линии связи
        {
            bool flag = true;
            {
                int x1, x2, y1, y2, r1, r2;
                r1 = (InfoMas[type] as Body).RadSize;
                x1 = x;
                y1 = y;
                for (int i = 0; (i < Mas.Length) && (flag); i++)
                    for (int j = 0; (j < Mas[i].Count) && (flag); j++)
                    {
                        x2 = (Mas[i][j] as Body).X;
                        y2 = (Mas[i][j] as Body).Y;
                        r2 = (Mas[i][j] as Body).RadSize;
                        if (r1 + r2 >= (int)Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2)))
                        {
                            flag = false;
                        }
                    }
            }
            if (flag)
            {
                int r = (InfoMas[type] as Body).RadSize;
                int x1, x2, y1, y2;
                double r1, r2, l, a, b, c, t, rr;
                for (int i = 0; i < Ways.Count; i++)
                {
                    x1 = (Mas[Ways[i][0]][Ways[i][1]] as Body).X;
                    y1 = (Mas[Ways[i][0]][Ways[i][1]] as Body).Y;
                    x2 = (Mas[Ways[i][2]][Ways[i][3]] as Body).X;
                    y2 = (Mas[Ways[i][2]][Ways[i][3]] as Body).Y;
                    l = Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
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

        public static bool ChekWay(int x1, int y1, int x2, int y2) //  проверяет: не пересекает ли путь другие постройки
        {
            bool flag = true;
            int x, y, r;
            double r1, r2, l, a, b, c, t, rr;
            l = Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
            for (int i = 0; (i < Mas.Length) && (flag); i++)
                for (int j = 0; (j < Mas[i].Count) && (flag); j++)
                {
                    x = (Mas[i][j] as Body).X;
                    y = (Mas[i][j] as Body).Y;
                    r = (Mas[i][j] as Body).RadSize;
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

        private static int RebildWay(object el) //  Соеденяет постройку путями, если возможно, существующие пути не трогает
        {
            int x2, y2, r;
            int[] temp;
            List<int[]> tW = new List<int[]>();
            int slots = (el as Body).Slots;
            int x = (el as Body).X;
            int y = (el as Body).Y;
            for (int i = 0; i < Mas.Length; i++)
                for (int j = 0; j < Mas[i].Count; j++)
                {
                    if ((i != (el as Body).PozMas) || (j != (el as Body).PozList))
                    {
                        x2 = (Mas[i][j] as Body).X;
                        y2 = (Mas[i][j] as Body).Y;
                        r = (int)Math.Sqrt((x - x2) * (x - x2) + (y - y2) * (y - y2));
                        if (((Mas[i][j] as Body).Slots < (Mas[i][j] as Body).MaxSlots) && (RadVisibl >= r))
                        {
                            bool flag = true;
                            for (int k = 0; k < Ways.Count; k++)
                            {
                                if ((Ways[k][0] == i) && (Ways[k][1] == j))
                                {
                                    if ((Ways[k][2] == (el as Body).PozMas) && (Ways[k][3] == (el as Body).PozList))
                                        flag = false;
                                }
                                if ((Ways[k][2] == i) && (Ways[k][3] == j))
                                {
                                    if ((Ways[k][0] == (el as Body).PozMas) && (Ways[k][1] == (el as Body).PozList))
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
            for (int i = 0; (i < tW.Count) && (slots < (el as Body).MaxSlots); i++)
            {
                (Mas[tW[i][0]][tW[i][1]] as Body).Slots++;
                slots++;
                temp = new int[4];
                temp[0] = (el as Body).PozMas;
                temp[1] = (el as Body).PozList;
                temp[2] = (Mas[tW[i][0]][tW[i][1]] as Body).PozMas;
                temp[3] = (Mas[tW[i][0]][tW[i][1]] as Body).PozList;
                Ways.Add(temp);
            }
            return slots;
        }

        private static void RebildGroups()      //  Наново формирует группы среди построек, опираясь на существующие пути
        {
            int wNewMas, wNewList;
            int tGroup = 0;
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
                    (Mas[i][j] as Body).Mark = true;
            for (int i = 0; i < Mas.Length; i++)
                for (int j = 0; j < Mas[i].Count; j++)
                    if ((Mas[i][j] as Body).Mark)
                    {
                        tGroup++;
                        (Mas[i][j] as Body).Mark = false;
                        (Mas[i][j] as Body).Group = tGroup;
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
                                if ((wNewMas != -1) && ((Mas[wNewMas][wNewList] as Body).Mark))
                                {
                                    (Mas[wNewMas][wNewList] as Body).Mark = false;
                                    (Mas[wNewMas][wNewList] as Body).Group = tGroup;
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
