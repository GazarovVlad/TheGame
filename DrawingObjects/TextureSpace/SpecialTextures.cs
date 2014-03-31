using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX.Direct3D;

namespace TheGameDrawing.TextureSpace
{
	public class SpecialTextures
	{
		private List<string> Keys = new List<string>();
		private List<Texture> Values = new List<Texture>();

		public void Add(string key, Texture value)
		{
			bool flag = true;
			foreach (string ekey in Keys)
				if (ekey == key)
					flag = false;
			if (flag)
			{
				Keys.Add(key);
				Values.Add(value);
			}
		}

		public Texture Get(string key)
		{
			for (int i = 0; i < Keys.Count; i++)
				if (Keys[i] == key)
					return Values[i];
			return null;
		}
	}
}
