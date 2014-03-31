using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX.Direct3D;
using GraphicObjects.ExplosionsSpace;
using TheGameDrawing.DrawingSpace;

namespace TheGameDrawing.TextureSpace.TextureLoders
{
    static class ExplosionsTex
    {
        private const string PathExplosions = "Sprites\\Explosions\\";
        private const string ExplosionNameStarts = "Explosion";
        private const string NameEnds = ".png";

        public static void Load()
        {
            Explosion();
        }

        private static void Explosion()
        {
            string folder;
            Textures.allExplosions = new Texture[Explosions.TypeNames.Length][][];
            for (int i = 0; i < Explosions.TypeNames.Length; i++)
            {
                folder = PathExplosions + Explosions.TypeNames[i] + "\\" + ExplosionNameStarts + Explosions.TypeNames[i] + "_";
                Textures.allExplosions[i] = new Texture[Explosions.TypeCounts[i]][];
                for (int j = 0; j < Explosions.TypeCounts[i]; j++)
                {
                    Textures.allExplosions[i][j] = new Texture[Explosions.Stages[i]];
                    for (int k = 0; k < Explosions.Stages[i]; k++)
                    {
                        string path = folder + (j + 1).ToString() + "-" + (k + 1).ToString() + NameEnds;
                        Textures.allExplosions[i][j][k] = TextureLoader.FromFile(Drawing.OurDevice, path, Explosions.TextureSizes[i], Explosions.TextureSizes[i], 0, Usage.None, Format.Unknown, Pool.Default, Filter.None, Filter.None, 0);
                    }
                }
            }
        }
    }
}
