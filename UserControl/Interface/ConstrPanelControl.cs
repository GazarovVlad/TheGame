using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FisicalObjects;
using FisicalObjects.Transist;
using ProgramObjects.InputDevices;
using ProgramObjects.InputDevices.Keyboard;
using ProgramObjects.ScreenGroup;

namespace UserControl.Interface
{
	public class ConstrPanelControl
	{
		public static bool ConstrSelected { get; private set; }
		public static int SlotType { get; private set; }
		public static int SlotSize { get; private set; }
		public static List<Point> Slot { get; private set; }
		public static TransConstrInfo[] ConstrInfo { get; private set; }

		public const int TextureRadVisiblSize = 220;

		private const int Border = 5;
		private const int Count = 3;

		public static void Initialize()
		{
			ConstrSelected = false;
			ConstrInfo = AIUnits.GetTransConstrInfo();
			SlotSize = (int)((WorkSpace.RightBorder.Width - (Count + 1) * Border) / (1.0 * Count));
			Slot = new List<Point>();
			int startX = WorkSpace.RightBorder.X + WorkSpace.UpBorder.Height + Border;
			int startY = WorkSpace.RightBorder.Y + Border;
			Slot.Add(new Point(startX, startY));
			for (int i = 1; i < Count; i++)
				Slot.Add(new Point(startX + i * SlotSize + i * Border, startY));
			for (int i = Count; i < AIUnits.GetConstrCount(); i++)
			{
				Slot.Add(new Point(Slot[i - Count].X, Slot[i - Count].Y + SlotSize + Border));
			}
			HotKeys.Initialize(Slot.Count);
		}

		public static bool ProcessMouse()
		{
			if (ConstrSelected)
				TryBuild();
			return CheckUserClicking();
		}

		public static void ProcessKeys()
		{
			CheckHotKeyPressed();
		}

		public static void Skip()
		{
			ConstrSelected = false;
			InformationWindow.SkipCurrentStructureInfo();
		}

		private static void CheckHotKeyPressed()
		{
			if (!ConstrSelected && HotKeys.Pressed)
			{
				ConstrSelected = true;
				InformationWindow.SetCurrentStructureInfo(AIUnits.GetInfoByType(HotKeys.Number));
				SlotType = HotKeys.Number;
			}
		}

		private static bool CheckUserClicking()
		{
			int x = Mouse.PresedDX, y = Mouse.PresedDY;
			for (int i = 0; i < Slot.Count; i++)
				if (x > Slot[i].X && x < Slot[i].X + SlotSize && y > Slot[i].Y && y < Slot[i].Y + SlotSize)
				{
					ConstrSelected = true;
					InformationWindow.SetCurrentStructureInfo(AIUnits.GetInfoByType(i));
					SlotType = i;
					InformationWindow.Skip();
					return true;
				}
			return false;
		}

		private static void TryBuild()
		{
			if (Mouse.PresedDX > WorkSpace.DX && Mouse.PresedDX < WorkSpace.DX + WorkSpace.Space.Width && Mouse.PresedDY > WorkSpace.DY && Mouse.PresedDY < WorkSpace.DY + WorkSpace.Space.Height)
			{
				ConstrSelected = false;
				InformationWindow.SkipCurrentStructureInfo();
				Point poz = new Point(WorkSpace.Space.X + Mouse.PresedDX - WorkSpace.DX, WorkSpace.Space.Y + Mouse.PresedDY - WorkSpace.DY);
				AIUnits.AddConstr(SlotType, poz);
			}
			if ((Mouse.PresedDX < MiniMap.DX) || (Mouse.PresedDY < MiniMap.DY))
			{
				ConstrSelected = false;
				InformationWindow.SkipCurrentStructureInfo();
			}
		}
	}
}