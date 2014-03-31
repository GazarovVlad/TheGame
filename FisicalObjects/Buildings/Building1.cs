using System;
using System.Collections.Generic;
using System.Text;
using Transist;

namespace Buildings
{
    public class Building1:Body,IBody
    {
        public const int OMaxHP = 100;
        //public const int OMass = 10;
        public const int ORadSize = 6;
        public const int OMaxSlots = 5;
        public const int TextureBuildingIndex = 0;
        public const string Name = "ConnectionPoint";

        public Building1(int pozMas, int pozList, int x, int y)
        {
            HP = MaxHP;
            Group = -1;
            PozList = pozList;
            PozMas = pozMas;
            X = x;
            Y = y;
            MaxHP = OMaxHP;
            //Mass = OMass;
            RadSize = ORadSize;
            MaxSlots = OMaxSlots;
            Slots = 0;
        }

        public object Create(int pozMas, int pozList, int x, int y)
        {
            return new Building1(pozMas, pozList, x, y);
        }

        public new object GetTransister()
        {
            return new Transist.TransBuilding(X, Y, TextureBuildingIndex);
        }
    }
}
