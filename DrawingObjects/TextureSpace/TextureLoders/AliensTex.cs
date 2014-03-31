using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX.Direct3D;
using GraphicObjects.StarsSpace;
using TheGameDrawing.DrawingSpace;
using FisicalObjects;

namespace TheGameDrawing.TextureSpace.TextureLoders
{
    static class AliensTex
    {
        private const string PathAliensDirectory = "Sprites\\Aliens\\";
        private const string NameEnds = ".png";
        
        public static void Load()
        {
			string[] types = AIUnits.GetAliensTypes();
			int[][] info = AIUnits.GetSpcAliensInfo();
            Textures.aliens = new Texture[types.Length][];
			for (int i = 0; i < types.Length; i++)
			{
				Textures.aliens[i] = new Texture[info[0][i]];
				ProcessAlienType(ref Textures.aliens[i], info[1][i], types[i]);
			}
        }

        private static void ProcessAlienType(ref Texture[] taliens, int tsizes, string indifer)
        {
			string alienPath = PathAliensDirectory + indifer + "\\";
            string alienName;
            for (int j = 0; j < taliens.Length; j++)
			{
				alienName = indifer + "_" + (j + 1).ToString();
				taliens[j] = TextureLoader.FromFile(Drawing.OurDevice, alienPath + alienName + NameEnds, tsizes, tsizes, 0, Usage.None, Format.Unknown, Pool.Default, Filter.None, Filter.None, 0);
            }
        }
    }
}
