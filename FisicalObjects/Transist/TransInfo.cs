using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace FisicalObjects.Transist
{
	public class TransInfo
	{
		public string Name { get; private set; }
		public string FirstInfo { get; private set; }
		public string SecondInfo { get; private set; }
		public string Type { get; private set; }
		public string Radius { get; private set; }
		public string FireRadius { get; private set; }
		public bool Button { get; private set; }
		public List<int> TextureIndex { get; private set; }
		public Point Coords { get; private set; }

		public TransInfo(string name, string firstinfo, string secondinfo, int rad, bool button, List<int> texind, string type, Point coords)
		{
			Name = name;
			FirstInfo = firstinfo;
			SecondInfo = secondinfo;
			Radius = rad.ToString();
			Button = button;
			TextureIndex = texind;
			Type = type;
			Coords = coords;
			FireRadius = "";
		}

		public TransInfo(string name, string firstinfo, string secondinfo, int rad, bool button, List<int> texind, string type, Point coords, int firerad)
		{
			Name = name;
			FirstInfo = firstinfo;
			SecondInfo = secondinfo;
			Radius = rad.ToString();
			Button = button;
			TextureIndex = texind;
			Type = type;
			Coords = coords;
			FireRadius = firerad.ToString();
		}
	}
}
