using System;
using System.Collections.Generic;
using System.Text;
using Transist;

namespace Buildings
{
    public class Tower1:Body,IBody
    {
        public const int OMaxHP = 500;
        //public const int OMass = 30;
        public const int ORadSize = 18;
        public const int OMaxSlots = 1;
        public const int TextureBaseIndex = 0;
        public const int TextureTurretIndex = 0;
        public const string Name = "Tower1";
        public int Angel { get; private set; }

        public Tower1(int pozMas, int pozList, int x, int y)
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
            Angel = 0;
        }

        public object Create(int pozMas, int pozList, int x, int y)
        {
            return new Tower1(pozMas, pozList, x, y);
        }

        public new object GetTransister()
        {
            return new Transist.TransTower(X, Y, TextureBaseIndex, TextureTurretIndex, Angel);
        }
    }
}
