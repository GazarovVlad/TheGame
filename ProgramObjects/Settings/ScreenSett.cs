using System;
using System.Collections.Generic;
using System.Text;
using ProgramObjects.ScreenGroup;
using SupportedStructures;
using System.IO;

namespace ProgramObjects.Settings
{
    public class ScreenSett
    {
        private const string ScreenPath = "Settings\\Screen.txt";
        private static Variables Data;
        private static string[] Names = 
        { 
            "width",
            "height",
            "borderLow",
            "borderHight",
            "step",
            "percentRad",
            "screenMode"
        };

        public static void Load()
        {
            Data = Loader.Load(ScreenPath);
            int width = Data.GetInt(Names[0]);
            int height = Data.GetInt(Names[1]);
            int borderLow = Data.GetInt(Names[2]);
            int borderHight = Data.GetInt(Names[3]);
            int step = Data.GetInt(Names[4]);
            double persentRad = Data.GetDouble(Names[5]);
            ScreenMode type = Data.GetScreenMode(Names[6]);
            //int w = Data.Get<int>(Names[0]);
            Screen.Initialize(width, height, type, persentRad, borderLow, borderHight, step);
        }

        public static void SaveWithChanges(int w, int h, ScreenMode screenMode)
        {
            StreamWriter writer = new StreamWriter(ScreenPath);
            writer.WriteLine(Names[0] + '=' + w.ToString());
            writer.WriteLine(Names[1] + '=' + h.ToString());
            writer.WriteLine(Names[6] + '=' + screenMode.ToString());
            writer.WriteLine(Names[2] + '=' + Screen.BorderLow.ToString());
            writer.WriteLine(Names[3] + '=' + Screen.BorderHigh.ToString());
            writer.WriteLine(Names[5] + "=0,03");
            writer.WriteLine(Names[4] + "=10");
            writer.Close();
        }
    }
}
