using System;
using System.Collections.Generic;
using System.Text;

namespace Buildings
{
    public class Body
    {
        public bool Mark;
        public int Slots;
        public int HP;
        public int Group;
        public int PozList;
        public int MaxSlots { get; protected set; }
        public int PozMas { get; protected set; }
        public int X { get; protected set; }
        public int Y { get; protected set; }
        //public int Speed { get; protected set; }
        //public int VektX { get; protected set; }
        //public int VektY { get; protected set; }
        public int MaxHP { get; protected set; }
        //public int Mass { get; protected set; }
        public int RadSize { get; protected set; }
    }
}
