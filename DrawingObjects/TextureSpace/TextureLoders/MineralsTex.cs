using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX.Direct3D;
using GraphicObjects.StarsSpace;
using TheGameDrawing.DrawingSpace;
using FisicalObjects;

namespace TheGameDrawing.TextureSpace.TextureLoders
{
    static class MineralsTex
    {
        private const string PathMineDirectory = "Sprites\\Minerals\\";
        private const string MineNameStarts = "Mineral";
        private const string NameEnds = ".png";

        public static void Load()
        {
            string[] types = AIUnits.GetMineralsTypes();
            int[][] info = AIUnits.GetSpcMineralsInfo();
            Textures.minerals = new Texture[types.Length][][];
            for (int i = 0; i < types.Length; i++)
            {
                Textures.minerals[i] = new Texture[info[0][i]][];
                for (int j = 0; j < info[0][i]; j++)
                    Textures.minerals[i][j] = new Texture[info[1][i]];
                ProcessMineralType(ref Textures.minerals[i], info[2][i], types[i]);
            }
        }

        private static void ProcessMineralType(ref Texture[][] tminerals, int tsizes, string indifer)
        {
            string minePath = PathMineDirectory + indifer + "\\";
            string mineName;
            for (int j = 0; j < tminerals.Length; j++)
            {
                mineName = MineNameStarts + indifer + "_" + (j + 1).ToString() + "-";
                for (int k = 0; k < tminerals[j].Length; k++)
                {
                    tminerals[j][k] = TextureLoader.FromFile(Drawing.OurDevice, minePath + mineName + (k + 1).ToString() + NameEnds, tsizes, tsizes, 0, Usage.None, Format.Unknown, Pool.Default, Filter.None, Filter.None, 0);
                }
            }
        }
    }
}
