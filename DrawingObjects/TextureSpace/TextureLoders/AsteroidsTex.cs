using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX.Direct3D;
using GraphicObjects.StarsSpace;
using TheGameDrawing.DrawingSpace;
using FisicalObjects;

namespace TheGameDrawing.TextureSpace.TextureLoders
{
    static class AsteroidsTex
    {
        private const string PathAsterDirectory = "Sprites\\Asteroids\\";
        private const string NameEnds = ".png";
        
        public static void Load()
        {
			string[] types = AIUnits.GetAsteroidsTypes();
			int[][] info = AIUnits.GetSpcAsteroidsInfo();
            Textures.asteroids = new Texture[types.Length][];
			for (int i = 0; i < types.Length; i++)
			{
				Textures.asteroids[i] = new Texture[info[0][i]];
				ProcessAsterType(ref Textures.asteroids[i], info[1][i], types[i]);
			}
        }

        private static void ProcessAsterType(ref Texture[] tasters, int tsizes, string indifer)
        {
			string asterPath = PathAsterDirectory + indifer + "\\";
            string asterName;
            for (int j = 0; j < tasters.Length; j++)
			{
				asterName = indifer + "_" + (j + 1).ToString();
				tasters[j] = TextureLoader.FromFile(Drawing.OurDevice, asterPath + asterName + NameEnds, tsizes, tsizes, 0, Usage.None, Format.Unknown, Pool.Default, Filter.None, Filter.None, 0);
            }
        }
    }
}
