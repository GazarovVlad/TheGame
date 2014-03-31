using System;
using System.Collections.Generic;
using System.Text;
using GraphicObjects.StarsSpace;
using ProgramObjects.ScreenGroup;
using System.IO;

namespace ProgramObjects.Settings
{
    public class StarsSett
    {
        private const string StarsPath = "Settings\\Stars.txt";
        private static Variables Data;
        private static string[] Names = 
        {
            "minCount",
            "maxCount",
            "koefMin",
            "koefMax",
            "size",
            "count",
            "types",
            "outMapRad",
            "typesCount"
        };

        public static void Load()
        {
			Data = Loader.Load(StarsPath);
            int[] minCount = Data.GetIntArr(Names[0]);
            int[] maxCount = Data.GetIntArr(Names[1]);
            int koefMin = Data.GetInt(Names[2]);
            int koefMax = Data.GetInt(Names[3]);
            int[] size = Data.GetIntArr(Names[4]);
            int[] count = Data.GetIntArr(Names[5]);
            string[] types = Data.GetStringArr(Names[6]);
            double[] outMapRad = Data.GetDoubleArr(Names[7]);
            int typesCount = Data.GetInt(Names[8]);
            Stars.Initialize(minCount, maxCount, size, count, koefMin, koefMax, types, typesCount, outMapRad, WorkSpace.Space, WorkSpace.MapLen);
        }

        public static void SaveWithChanges(string strMinCount, string strMaxCount)
        {
            StreamWriter writer = new StreamWriter(StarsPath);
            writer.WriteLine(Names[0] + '=' + strMinCount);
            writer.WriteLine(Names[1] + '=' + strMaxCount);
            writer.WriteLine(Names[2] + '=' + Stars.KoefMin.ToString());
            writer.WriteLine(Names[3] + '=' + Stars.KoefMax.ToString());
            writer.WriteLine(Names[4] + '=' + Stars.Size[0] + ' ' + Stars.Size[1] + ' ' + Stars.Size[2]);
            writer.WriteLine(Names[5] + '=' + Stars.Count[0] + ' ' + Stars.Count[1] + ' ' + Stars.Count[2]);
            writer.WriteLine(Names[7] + '=' + Stars.OutMapRad[0] + ' ' + Stars.OutMapRad[1] + ' ' + Stars.OutMapRad[2]);
            writer.WriteLine(Names[6] + '=' + Stars.Types[0] + ' ' + Stars.Types[1] + ' ' + Stars.Types[2]);
            writer.WriteLine(Names[8] + '=' + Stars.Types.Length.ToString());
            writer.Close();
        }
    }
}
