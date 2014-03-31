using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX.Direct3D;
using GraphicObjects.StarsSpace;
using TheGameDrawing.DrawingSpace;

namespace TheGameDrawing.TextureSpace.TextureLoders
{
    static class StarsTex
    {
        private const string PathStarsDirectory = "Sprites\\Stars\\";
        private const string StarNameStarts = "Star";
        private const string NameEnds = ".png";
        
        public static void Load()
        {
            Textures.allStars = new Texture[Stars.AllStars.Length][];
            for (int i = 0; i < Textures.allStars.Length; i++)
            {
                Textures.allStars[i] = new Texture[Stars.Count[i]];
                ProcessStarType(ref Textures.allStars[i], i, Stars.Count[i], Stars.Size[i]);
            }
        }

        private static void ProcessStarType(ref Texture[] stars, int i, int starsCount, int starsSize)
        {
            string starPath;
            string starName;
            for (int j = 0; j < starsCount; j++)
            {
                starPath = PathStarsDirectory;
                starName = StarNameStarts + Stars.Types[i] + (j + 1).ToString();
                starPath += starName + NameEnds;
                stars[j] = TextureLoader.FromFile(Drawing.OurDevice, starPath, starsSize, starsSize, 0, Usage.None, Format.Unknown, Pool.Default, Filter.None, Filter.None, 0);
            }
        }
    }
}
