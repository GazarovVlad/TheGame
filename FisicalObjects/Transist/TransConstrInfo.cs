using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace FisicalObjects.Transist
{
    public class TransConstrInfo
    {
        public string Name { get; private set; }
		public int Type { get; private set; }
		public int IconIndex { get; private set; }
		public int FireRang { get; private set; }
		public int Radius { get; private set; }

		public TransConstrInfo(string name, int type, int ii, int firer, int rad)
        {
			Name = name;
			Type = type;
			IconIndex = ii;
			FireRang = firer;
			Radius = rad;
        }
    }
}
