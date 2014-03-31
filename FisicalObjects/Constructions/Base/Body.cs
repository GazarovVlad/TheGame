using System;
using System.Collections.Generic;
using System.Text;

namespace FisicalObjects.Constructions
{
    class Body
    {
        public bool Mark;		// Нужно для алгоритма групировки соедененных построек
		public bool Powered;	// Достаточно ли энергии для работы
        public int Slots;		// Количество задействованных соеденений
        public int Group;		// Номер группы, которой принадлежит данная постройка
		public int PozList;		// Позиция в списке построек данного типа
		public int Mass { get; protected set; }			// Текущее количество здоровья
		public int HitPoints { get; protected set; }			// Текущее количество здоровья
		public int Energy { get; protected set; }		// Потребляемая(-) или производимая(+) энергия
        public int X { get; protected set; }			// Мировая координата X
		public int Y { get; protected set; }			// Мировая координата Y
		public float Angle { get; protected set; }
		public int Radius { get; protected set; }
		public float Shield { get; protected set; }		// Уменьшает урон, зависящий от массы астероидов

		private static Random Rand = new Random();

		public Body()
		{
			//	Empty
		}

		public Body(int hp, int energy, int rad, float shield, int mass)
		{
			HitPoints = hp;
			Angle = 0;//(float)(Rand.NextDouble() * Math.PI);
			Energy = energy;
			Powered = false;
			Radius = rad;
			Shield = shield;
			Mass = mass;
		}
    }
}
