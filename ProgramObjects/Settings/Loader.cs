using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace ProgramObjects.Settings
{
    static class Loader
    {
        private static Variables Data;
        private static StreamReader reader;

        public static Variables Load(string fileName)
        {
            Data = new Variables();
            Reader(fileName);
            return Data;
        }

        private static void Reader(string fileName)
        {
            reader = new StreamReader(fileName);
            while (!reader.EndOfStream)
            {
                string[] temp = reader.ReadLine().Split('=');
                try
                {
                    Data.Values.Add(temp[1]);
                    Data.Names.Add(temp[0]);
                }
                catch 
                {
                    //вставить обработчик критикал эрор
                }
                #region OLD
                //switch (reader.ReadLine())
                //{
                //    case "int":
                //        {
                //            ReadInt();
                //            break;
                //        }
                //    case "int[]":
                //        {
                //            ReadIntArr();
                //            break;
                //        }
                //    case "double":
                //        {
                //            ReadDouble(); 
                //            break;
                //        }
                //    case "bool":
                //        {
                //            ReadBool(); 
                //            break;
                //        }
                //    default: break;
                //}
                #endregion
            }
            reader.Close();
        }
        #region OLD
        //private static void ReadInt() 
        //{
        //    string[] split = reader.ReadLine().Split('=');
        //    Data.IntNames.Add(split[0]);
        //    Data.IntValues.Add(Convert.ToInt32(split[1]));
        //}

        //private static void ReadIntArr()
        //{
        //    string[] split = reader.ReadLine().Split('=');
        //    Data.IntArrNames.Add(split[0]);
        //    string[] values = split[1].Split(' ');
        //    int[] temp = new int[values.Length];
        //    for (int i = 0; i < values.Length; i++)
        //        temp[i] = Convert.ToInt32(values[i]);
        //    Data.IntArrValues.Add(temp);
        //}

        //private static void ReadBool()
        //{
        //    string[] split = reader.ReadLine().Split('=');
        //    Data.BoolNames.Add(split[0]);
        //    Data.BoolValues.Add(Convert.ToBoolean(split[1]));
        //}

        //private static void ReadDouble()
        //{
        //    string[] split = reader.ReadLine().Split('=');
        //    Data.DoubleNames.Add(split[0]);
        //    Data.DoubleValues.Add(Convert.ToDouble(split[1]));
        //}
        #endregion
    }
}
