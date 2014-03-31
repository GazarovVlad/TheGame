using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using FisicalObjects.Constructions.Descendants;

namespace FisicalObjects.Constructions
{
	static class ConstrLoder
	{
		public static Body[] InfoMas { get; private set; }
		public static ConstrOption[] ConstrInfo { get; private set; }

		private const string Path = "Settings\\ConstructionsInfo.txt";

		public static void LoadDataFromFile()
		{
			int id = 0, mass = 0, firer = 0, hp = 0, energy = 0, slots = 0, rad = 0, tbi = -1, tti = -1, ii = -1, explf = -1, rech = -1, fired = -1, firem = -1, price = -1, rr = -1;
			float shield = 0;
			bool connect = false;
			string type = "", name = "", explt = "", firet = "", targt = "", re = "", he = "";
			Loder data = new Loder(Path);
			while (data.Next())
			{
				if (data.Key == "Constructions")
				{
					InfoMas = new Body[Convert.ToInt32(data.Value)];
					ConstrInfo = new ConstrOption[Convert.ToInt32(data.Value)];
				}
				if (data.Key == "ID")
					id = Convert.ToInt32(data.Value);
				if (data.Key == "Type")
					type = data.Value;
				if (data.Key == "Name")
					name = data.Value;
				if (data.Key == "MaxHp")
					hp = Convert.ToInt32(data.Value);
				if (data.Key == "Mass")
					mass = Convert.ToInt32(data.Value);
				if (data.Key == "Price")
					price = Convert.ToInt32(data.Value);
				if (data.Key == "Energy")
					energy = Convert.ToInt32(data.Value);
				if (data.Key == "Connecting")
					connect = Convert.ToBoolean(data.Value);
				if (data.Key == "MaxSlots")
					slots = Convert.ToInt32(data.Value);
				if (data.Key == "RadSize")
					rad = Convert.ToInt32(data.Value);
				if (data.Key == "Shield")
					shield = (float)(Convert.ToDouble(data.Value));
				if (data.Key == "ExplType")
					explt = data.Value;
				if (data.Key == "ExplForse")
					explf = Convert.ToInt32(data.Value);
				if (data.Key == "Recharge")
					rech = Convert.ToInt32(data.Value);
				if (data.Key == "FireRang")
					firer = Convert.ToInt32(data.Value);
				if (data.Key == "TargetType")
					targt = data.Value;
				if (data.Key == "FireType")
					firet = data.Value;
				if (data.Key == "FireDemage")
					fired = Convert.ToInt32(data.Value);
				if (data.Key == "FireMass")
					firem = Convert.ToInt32(data.Value);
				if (data.Key == "RodRadius")
					rr = Convert.ToInt32(data.Value);
				if (data.Key == "RodEffect")
					re = data.Value;
				if (data.Key == "HitEffect")
					he = data.Value;
				if (data.Key == "IconIndex")	//	индекс текстуры иконки
					ii = Convert.ToInt32(data.Value);
				if (data.Key == "TBI")	//	индекс основной текстуры
					tbi = Convert.ToInt32(data.Value);
				if (data.Key == "TTI")	//	индекс текстуры турели (если есть)
					tti = Convert.ToInt32(data.Value);
				if (data.Key == "End")
				{
					Body body = new Body(hp, energy, rad, shield, mass);
					ConstrInfo[id] = new ConstrOption(name, type, hp, rad, slots, connect, explt, explf, ii, firer, price);
					if (type == Building.ConstrType)
						InfoMas[id] = new Building(0, 0, 0, body, tbi);
					if (type == Tower.ConstrType)
						InfoMas[id] = new Tower(0, 0, 0, body, firer, firem, fired, firet, targt, rech, tbi, tti, rr, re, he);
					firer = 0;
				}
			}
			data.EndReading();
		}

	}
}
