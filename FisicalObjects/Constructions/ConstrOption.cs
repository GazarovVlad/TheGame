using System;
using System.Collections.Generic;
using System.Text;

namespace FisicalObjects.Constructions
{
	class ConstrOption
	{
		public string Name { get; protected set; }		// Имя или название
		public string Type { get; protected set; }		// Однозначно определяет производный класс
		public int MaxHP { get; protected set; }		// Максимальный запас здоровья
		public int RadSize { get; protected set; }		// Физический радиус постройки
		public int MaxSlots { get; protected set; }		// Максимально возможное количество задействованных соеденений
		public bool Connecting { get; protected set; }	// true - соеденяется со всеми, false - только с true
		public string ExplType { get; protected set; }	// Имя взрыва, возникающего при смерти
		public int ExplForse { get; protected set; }	// Сила взрыва, возникающего при смерти
		public int IconIndex { get; protected set; }	// Номер текстуры иконки
		public int FireRang { get; protected set; }		// Вынужденая необходимость
		public int Price { get; protected set; }		// Цена (сколько необходимо минералов для создания)

		public ConstrOption()
		{
			//	Empty
		}

		public ConstrOption(string name, string type, int hp, int rad, int slots, bool connect, string explType, int explForse, int ii, int firer, int price)
		{
			Name = name;
			Type = type;
			MaxHP = hp;
			RadSize = rad;
			MaxSlots = slots;
			Connecting = connect;
			ExplType = explType;
			ExplForse = explForse;
			IconIndex = ii;
			FireRang = firer;
			Price = price;
		}
	}
}
