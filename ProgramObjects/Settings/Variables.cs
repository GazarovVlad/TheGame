using System;
using System.Collections.Generic;
using System.Text;
using SupportedStructures;

namespace ProgramObjects.Settings
{
    class Variables
    {
        //public List<int> IntValues = new List<int>();
        //public List<string> IntNames = new List<string>();

        //public List<int[]> IntArrValues = new List<int[]>();
        //public List<string> IntArrNames = new List<string>();

        //public List<double> DoubleValues = new List<double>();
        //public List<string> DoubleNames = new List<string>();

        //public List<bool> BoolValues = new List<bool>();
        //public List<string> BoolNames = new List<string>();

        public List<string> Values = new List<string>();
        public List<string> Names = new List<string>();

        //ПРИДУМАТЬ ОБРАБОТКУ ОШИБОК

        //public T Get<T>(string name)
        //{
        //    return T;
        //}

        public int GetInt(string name)
        {
            int ret = 0;
            for (int i = 0; i < Names.Count; i++)
                if (Names[i] == name)
                {
                    Names.RemoveAt(i);
                    ret = Convert.ToInt32(Values[i]);
                    Values.RemoveAt(i);
                    break;
                    //return Values[i];
                }
            return ret;
        }

        public int[] GetIntArr(string name)
        {
            int[] ret = new int[1];
            for (int i = 0; i < Names.Count; i++)
                if (Names[i] == name)
                {
                    Names.RemoveAt(i);
                    string[] temp = Values[i].Split(' ');
                    ret = new int[temp.Length];
                    for (int j = 0; j < temp.Length; j++)
                        ret[j] = Convert.ToInt32(temp[j]);
                    Values.RemoveAt(i);
                    break; 
                }
            //if()
            //return null;
            return ret;
        }

        public bool GetBool(string name)
        {
            bool ret = false;
            for (int i = 0; i < Names.Count; i++)
                if (Names[i] == name)
                {
                    Names.RemoveAt(i);
                    ret = Convert.ToBoolean(Values[i]);
                    Values.RemoveAt(i);
                    break;
                }
            return ret;
            //return false;
        }

        public double GetDouble(string name)
        {
            double ret = 0;
            for (int i = 0; i < Names.Count; i++)
                if (Names[i] == name)
                {
                    Names.RemoveAt(i);
                    ret = Convert.ToDouble(Values[i]);
                    Values.RemoveAt(i);
                    break;
                }
            return ret;
        }

        public double[] GetDoubleArr(string name)
        {
            double[] ret = new double[1];
            for (int i = 0; i < Names.Count; i++)
                if (Names[i] == name)
                {
                    Names.RemoveAt(i);
                    string[] temp = Values[i].Split(' ');
                    ret = new double[temp.Length];
                    for (int j = 0; j < temp.Length; j++)
                        ret[j] = Convert.ToDouble(temp[j]);
                    Values.RemoveAt(i);
                    break;
                }
            return ret;
        }

        public string[] GetStringArr(string name)
        {
            string[] ret = new string[1];
            for (int i = 0; i < Names.Count; i++)
                if (Names[i] == name)
                {
                    Names.RemoveAt(i);
                    ret = Values[i].Split(' ');
                    Values.RemoveAt(i);
                    break;
                }
            return ret;
        }

        public ScreenMode GetScreenMode(string name)
        {
            ScreenMode ret = ScreenMode.windowed;
            for (int i = 0; i < Names.Count; i++)
                if (Names[i] == name)
                {
                    Names.RemoveAt(i);
                    string s = ScreenMode.fullscreen.ToString();
                    if (Values[i] == ScreenMode.fullscreen.ToString())
                        ret = ScreenMode.fullscreen;
                    Values.RemoveAt(i);
                    break;
                }
            return ret;
        }
    }
}
