using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GraphicObjects.ExplosionsSpace;

namespace ProgramObjects.Settings
{
    class ExplosionsSett
    {
        static string[] Names =
        {
            "stages",
            "typeCounts",
            "typeNames",
            "textureSizes"
        };
        static Variables Data;
		const string ExplosionsPath = "Settings\\Explosions.txt";

        public static void Load()
        {
            Data = Loader.Load(ExplosionsPath);
            int[] stages = Data.GetIntArr(Names[0]);
            int[] types = Data.GetIntArr(Names[1]);
            string[] sizeNames = Data.GetStringArr(Names[2]);
            int[] textureSizes = Data.GetIntArr(Names[3]);
            Explosions.Initialize(stages, types, sizeNames, textureSizes);
        }
    }
}
