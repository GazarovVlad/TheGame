using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FisicalObjects.Transist;
using FisicalObjects.Cosmos.Asteroids;

namespace FisicalObjects.Constructions.Descendants
{
    class Building : Body, IBody
    {
        public const string ConstrType = "Building";

        public int TextureBuildingIndex { get; private set; }

        public Building(int pozList, int x, int y, Body body, int tbi)
        {
            Group = -1;
            Slots = 0;
            X = x;
            Y = y;
            PozList = pozList;
            TextureBuildingIndex = tbi;
            HitPoints = body.HitPoints;
            Energy = body.Energy;
            Angle = body.Angle;
            Radius = body.Radius;
            Shield = body.Shield;
            Mass = body.Mass;
            Powered = false;
        }

        public Body Create(int pozList, int x, int y, int maxHP)
        {
            Body body = new Body(maxHP, Energy, Radius, Shield, Mass);
            return new Building(pozList, x, y, body, TextureBuildingIndex);
        }

        public object GetTransister()
        {
            return new Transist.TransBuilding(X, Y, TextureBuildingIndex, Angle, Radius);
        }

        public TransFire GetFireTransister()
        {
            return null;
        }

        public void DoAction()
        {
            Powered = false;
            if ((Group != -1) && (ConstrGroup.PowerList[Group] > 0))
            {
                Angle += 0.01f;
                if (Angle >= 2 * Math.PI)
                    Angle -= (float)(2 * Math.PI);
                Powered = true;
            }
            HitPoints -= (int)(AsterField.TryClash(new Point(X, Y), Radius, Mass) / Shield);
        }

        public void EndAction()
        {
        }

        public void CalibrateEnergy()
        {
            if (Group != -1)
                ConstrGroup.PowerList[Group] += Energy;
        }

        public void ExplodeHit(int demage)
        {
            HitPoints -= demage;
        }

        public TransInfo GetInfo(string name, int maxhp, int maxslots, int price)
        {
            string firstinfo, secondinfo;
            firstinfo = "Прочность: " + HitPoints.ToString() + '\n' + "Максимум: " + maxhp.ToString();
            secondinfo = "Энергия - ";
            if (Energy == 0)
                secondinfo += "независит";
            if (Energy < 0)
                secondinfo += "поглощает " + (-Energy).ToString();
            if (Energy > 0)
                secondinfo += "производит " + Energy.ToString();
            secondinfo += '\n';
            secondinfo += "Энергия сети - (" + ConstrGroup.PowerList[Group].ToString() + ")" + '\n';
            secondinfo += "Соиденения - " + Slots.ToString() + '\n';
            secondinfo += "Соединяемость - " + maxslots.ToString();
            List<int> tind = new List<int>();
            tind.Add(TextureBuildingIndex);
            return new TransInfo(name, firstinfo, secondinfo, Radius, true, tind, ConstrType, new Point(X, Y));
        }
    }
}
