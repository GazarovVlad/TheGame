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
		public const int PlanetRadius = 220; // 40.0 == 60 166.667

        public MeshObject Earth { get; private set; }
        public MeshObject Clouds { get; private set; }
		public bool Rotate { get; private set; }
        public Vector2 Pos;
        public List<ExplosionOnSurface> Explosions; //типы взрывов
        private List<CratersOnSurface> Craters; //типы кратеров

        private static string[] SupportedTextureFormats = { "*.png" };

		private float IncRotationEarth = 0.002f;
        private float IncRotationClouds = 0.003f;
        private bool placeOfRandomExplosion = true;

        public Planet(MeshObject earth, MeshObject clouds, Vector2 pos)
        {
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
            ExplosionOnSurface exp = new ExplosionOnSurface(stages, new Vector3(-IncRotationEarth, 0.0f, 0.0f));
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
				Craters.Add(new CratersOnSurface(craters[i], new Vector3(-IncRotationEarth, 0.0f, 0.0f)));
			//AddExplosion(1, 1);
        }

        public void AddExplosion(int x, int y)
        {
            Vector3 turn = new Vector3();

            Random rand = new Random();
			turn.X = 0.0f;//(float)rand.NextDouble() + (float)rand.NextDouble() + 3.3f;
            turn.Y = 0.0f;// (float)Math.PI;//(float)((rand.NextDouble()+rand.NextDouble()) * Math.PI); //от 0 до 2*PI
			turn.Z = 0.0f;//(float)rand.NextDouble() + 0.3f;
            if (placeOfRandomExplosion)
                turn.Z = -turn.Z;
            placeOfRandomExplosion = !placeOfRandomExplosion;

            int index = rand.Next(0, Explosions.Count);
            Explosions[index].AddExplosion(turn);
            index = rand.Next(0, Craters.Count);
            Craters[index].AddCrater(turn);
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

            //включать-выключать прозрачность рекомендуется чем пореже
            MeshRenderer.device.RenderState.AlphaBlendEnable = true;
            MeshRenderer.device.RenderState.SourceBlend = Blend.SourceAlpha;
            MeshRenderer.device.RenderState.DestinationBlend = Blend.InvSourceAlpha;

            foreach (CratersOnSurface c in Craters)
                c.Render(Rotate);
            foreach (ExplosionOnSurface expl in Explosions)
                expl.Render(Rotate);
            View.DrawMesh(Clouds, true);

            MeshRenderer.device.RenderState.AlphaBlendEnable = false;

            //временно, пока нет астероидов
            //AddExplosion(10,20);
        }
    }
}
