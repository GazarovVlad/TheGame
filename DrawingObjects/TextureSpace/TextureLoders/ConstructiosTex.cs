using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.DirectX.Direct3D;
using TheGameDrawing.DrawingSpace;
using UserControl.Interface;

namespace TheGameDrawing.TextureSpace.TextureLoders
{
    class ConstructiosText
    {
		private static StreamReader strReader;
		private static string key;
		private static string value;
		private static string[] TGroups = { "ConstructionBuildings:", "ConstructionTowerBase:", "ConstructionTowerTurrets:",
										  "ConstructionIcons:", "ConstructionIconsShine:"};

		private const string FName = "FName";
		private const string MainPath = "MainPath";
		private const string IconsPath = "IconPath";
		private const string TCount = "Count";
		private const string Path = "Settings\\ConstructionTexturesInfo.txt";

        public static void Load()
        {
			LoadDataFromFile();
		}

		public static void LoadDataFromFile()
		{
			string line;
			int tgroup = -1, id = -1;
			string mpath = "", ipath = "";
			strReader = new StreamReader(new FileStream(Path, FileMode.Open));
			while ((line = strReader.ReadLine()) != null)
			{
				for (int i = 0; i < TGroups.Length; i++)
				{
					if (TGroups[i] == line)
						tgroup = i;
				}
				if (ParseLine(line))
				{
					if (key == MainPath)
						mpath = value;
					if (key == IconsPath)
						ipath = value;
					if (key == TCount)
					{
						id = 0;
						switch (tgroup)
						{
							case 0: Textures.constrBuildings = new Texture[Convert.ToInt32(value)]; break;
							case 1: Textures.constrTowerBase = new Texture[Convert.ToInt32(value)]; break;
							case 2: Textures.constrTowerTurrets = new Texture[Convert.ToInt32(value)]; break;
							case 3: Textures.constrIcons = new Texture[Convert.ToInt32(value)]; break;
							case 4: Textures.constrIconsShine = new Texture[Convert.ToInt32(value)]; break;
						}
					}
					if (key == FName)
					{
						switch (tgroup)
						{
							case 0: Textures.constrBuildings[id] = TextureLoader.FromFile(Drawing.OurDevice, mpath + value, ConstrPanelControl.SlotSize, ConstrPanelControl.SlotSize, 0, Usage.None, Format.Unknown, Pool.Default, Filter.None, Filter.None, 0); break;
							case 1: Textures.constrTowerBase[id] = TextureLoader.FromFile(Drawing.OurDevice, mpath + value, ConstrPanelControl.SlotSize, ConstrPanelControl.SlotSize, 0, Usage.None, Format.Unknown, Pool.Default, Filter.None, Filter.None, 0); break;
							case 2: Textures.constrTowerTurrets[id] = TextureLoader.FromFile(Drawing.OurDevice, mpath + value, ConstrPanelControl.SlotSize, ConstrPanelControl.SlotSize, 0, Usage.None, Format.Unknown, Pool.Default, Filter.None, Filter.None, 0); break;
							case 3: Textures.constrIcons[id] = TextureLoader.FromFile(Drawing.OurDevice, ipath + value, ConstrPanelControl.SlotSize, ConstrPanelControl.SlotSize, 0, Usage.None, Format.Unknown, Pool.Default, Filter.None, Filter.None, 0); break;
							case 4: Textures.constrIconsShine[id] = TextureLoader.FromFile(Drawing.OurDevice, ipath + value, ConstrPanelControl.SlotSize, ConstrPanelControl.SlotSize, 0, Usage.None, Format.Unknown, Pool.Default, Filter.None, Filter.None, 0); break;
						}
						id++;
					}
				}
			}
			strReader.Close();
		}

		private static bool ParseLine(string line)
		{
			string[] splited = line.Split('=');
			if ((splited.Length != 2) || (splited[0].Length == 0) || (splited[1].Length == 0))
				return false;
			key = "";
			for (int i = 0; i < splited[0].Length; i++)
				if ((splited[0][i] != ' ') && (splited[0][i] != '\t'))
					key += splited[0][i];
			value = "";
			for (int i = 0; i < splited[1].Length; i++)
				if ((splited[1][i] != ' ') && (splited[1][i] != '\t'))
					value += splited[1][i];
			return true;
		}
    }
}
