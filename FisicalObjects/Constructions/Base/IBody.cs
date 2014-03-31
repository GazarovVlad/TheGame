using System;
using System.Collections.Generic;
using System.Text;
using FisicalObjects.Transist;

namespace FisicalObjects.Constructions
{
    interface IBody
    {
		Body Create(int pozList, int x, int y, int maxHP);
		object GetTransister();
		TransFire GetFireTransister();
		void ExplodeHit(int demage);
		void DoAction();
		void EndAction();
		void CalibrateEnergy();
		TransInfo GetInfo(string name, int maxhp, int maxslots, int price);
    }
}
