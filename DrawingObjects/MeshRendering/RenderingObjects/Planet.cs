using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System.Globalization;
using System.IO;
using SupportedStructures;
using System.Drawing;

namespace TheGameDrawing.MeshRendering.RenderingObjects
{
    public class Planet
	{
		public const float Planet3DRadius = 170;
		public static float Planet2DRadius { get; private set; }

        public MeshObject Earth { get; private set; }
        public MeshObject Clouds { get; private set; }
		public bool Rotate { get; private set; }
        public Vector2 Pos;
        public List<ExplosionOnSurface> Explosions; //типы взрывов
        
		private static Random rand = new Random();
        private static string[] SupportedTextureFormats = { "*.png" };

		private List<CratersOnSurface> Craters; //типы кратеров
		private float IncRotationEarth = 0.002f;
        private float IncRotationClouds = 0.003f;
        //private bool placeOfRandomExplosion = true;

        public Planet(MeshObject earth, MeshObject clouds, Vector2 pos)
        {
			Planet2DRadius = Planet3DRadius * View.ConstCoef;
            Earth = earth;
            Clouds = clouds;
			Rotate = true;
			Pos = pos;
			//Pos = new Vector2(pos.X - PlanetRadius, pos.Y - PlanetRadius);
            Earth.Pos = Pos;
            Clouds.Pos = Pos;
            Explosions = new List<ExplosionOnSurface>();
            Craters = new List<CratersOnSurface>();
        }

        private static string[] GetSupportedFilesInDir(string nameDir)
        {
            List<string> result = new List<string>();
            DirectoryInfo dir = new DirectoryInfo(nameDir);
            foreach (string format in SupportedTextureFormats)
            {
                FileInfo[] files = dir.GetFiles(format);
                foreach (FileInfo file in files)
                {
                    result.Add(file.FullName);
                }
            }
            return result.ToArray();
        }

        public void LoadExplosion(Device device, string pathToObject, string pathDirToTextures)
        {
            string[] fileNames = GetSupportedFilesInDir(pathDirToTextures);
            MeshObject[] stages = new MeshObject[fileNames.Length];
            for (int i = 0; i < fileNames.Length; i++)
            {
                stages[i] = MeshLoader.LoadMesh(device, pathToObject, fileNames[i]);
                stages[i].Pos = Pos;
            }
            ExplosionOnSurface exp = new ExplosionOnSurface(stages, new Vector3(IncRotationEarth, 0.0f, 0.0f));
            Explosions.Add(exp);
        }

        public void LoadCraters(Device device, string pathToObject, string pathDirToTextures)
        {
            string[] fileNames = GetSupportedFilesInDir(pathDirToTextures);
            MeshObject[] craters = new MeshObject[fileNames.Length];
            for (int i = 0; i < fileNames.Length; i++)
            {
                craters[i] = MeshLoader.LoadMesh(device, pathToObject, fileNames[i]);
                craters[i].Pos = Pos;
            }
            for (int i = 0; i < craters.Length; i++)
				Craters.Add(new CratersOnSurface(craters[i], new Vector3(IncRotationEarth, 0.0f, 0.0f)));
			//AddExplosion(1, 1);
        }

		public void AddExplosions(List<Point> expls)
		{
			foreach (Point ptr in expls)
				AddExplosion(ptr.X, ptr.Y);
		}

        public void AddExplosion(int x, int y)
        {
			Vector3 turn = Convert2Dto3D(x, y);
            /*if (placeOfRandomExplosion)
                turn.Z = -turn.Z;
            placeOfRandomExplosion = !placeOfRandomExplosion;*/
            int index = rand.Next(0, Explosions.Count);
            Explosions[index].AddExplosion(turn);
            index = rand.Next(0, Craters.Count);
            Craters[index].AddCrater(turn);
		}

		private Vector3 Convert2Dto3D(float x, float y)
		{
			float q1, q2, q3 = (float)((rand.NextDouble() + rand.NextDouble()) * Math.PI);
			float pr = Planet3DRadius;
			double z = -Math.Sqrt(pr * pr - x * x - y * y);
			q1 = (float)Math.Atan2(z, x);//x
			q2 = (float)Math.Atan2(y, Math.Sqrt(x * x + z * z));//z
			return new Vector3(q1 + ((float)Math.PI / 2), q2, q3);
		}

        public void Render()
        {
            Rotate = !UserControl.Menus.MenuMain.Showed;
            if (Rotate)
            {
                Earth.AddTurn(new Vector3(IncRotationEarth, 0.0f, 0.0f));
                Clouds.AddTurn(new Vector3(IncRotationClouds, 0.0f, 0.0f));
            }

            ///важна последовательность отрисовки
            View.DrawMesh(Earth, true);
            foreach (CratersOnSurface c in Craters)
                c.Render(Rotate);
            foreach (ExplosionOnSurface expl in Explosions)
                expl.Render(Rotate);
            View.DrawMesh(Clouds, true);
        }
    }
}
