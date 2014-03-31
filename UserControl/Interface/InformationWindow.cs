using System;
using System.Collections.Generic;
using Microsoft.DirectX.Direct3D;
using System.Drawing;
using ProgramObjects.ScreenGroup;
using SupportedStructures;
using System.Text;
using ProgramObjects.InputDevices;
using FisicalObjects;
using FisicalObjects.Transist;
using UserControl.Menus.Elements;

namespace UserControl.Interface
{
	public class InformationWindow
	{
		public static Rectangle RectStats { get; private set; }
		public static Rectangle RectIcon { get; private set; }
		public static Rectangle RectFirstInfo { get; private set; }
		public static Rectangle RectSecondInfo { get; private set; }
		public static Rectangle RectButton { get; private set; }
		public static Rectangle RectCurrentPrice { get; private set; }
		public static bool IsShow { get; private set; }
		public static Label LabStats { get; private set; }
		public static Label LabFirstInfo { get; private set; }
		public static Label LabSecondInfo { get; private set; }
		public static Label LabCurrentPrice { get; private set; }

		private const int Rad = 10;
		private const int RectStatsWidth = 140;
		private const int RectStatsHeight = 44;
		private const int RectCurrentPriceHeight = 44;
		private const int ButtonWidth = 80;
		private const int ButtonHeight = 30;
		private const int FirstInfoWidth = 110;
		private const int FirstInfoHeight = 30;
		private const int SecondInfoWidth = 200;

		private static string CurrentStructureInfo;

		private static TransInfo ObjSelected;

		public static void Initialize(Field downBorder, Field minimapBorder, int frame)
		{
			SkipCurrentStructureInfo();
			IsShow = false;
			RectStats = new Rectangle(downBorder.X + Rad, downBorder.Y + Rad + frame, RectStatsWidth, RectStatsHeight);
			RectIcon = new Rectangle(RectStats.X + RectStats.Width + Rad + FirstInfoWidth / 2 - ConstrPanelControl.SlotSize / 2, downBorder.Y + Rad + frame, ConstrPanelControl.SlotSize, ConstrPanelControl.SlotSize);
			RectFirstInfo = new Rectangle(RectStats.X + RectStats.Width + Rad, RectIcon.Y + RectIcon.Height + Rad, FirstInfoWidth, FirstInfoHeight);
			RectSecondInfo = new Rectangle(RectFirstInfo.X + RectFirstInfo.Width + Rad, downBorder.Y + Rad + frame, SecondInfoWidth, downBorder.Height - 2 * Rad - frame);
			RectButton = new Rectangle(RectFirstInfo.X + FirstInfoWidth / 2 - ButtonWidth / 2, RectFirstInfo.Y + RectFirstInfo.Height + Rad, ButtonWidth, ButtonHeight);
			RectCurrentPrice = new Rectangle(minimapBorder.X + Rad + frame, minimapBorder.Y - Rad - RectCurrentPriceHeight, minimapBorder.Width - 2 * Rad - frame, RectCurrentPriceHeight);
			LabStats = new Label("", RectStats, true, true, SupportedStructures.OurFonts.infoFont);
			LabFirstInfo = new Label("", RectFirstInfo, true, true, SupportedStructures.OurFonts.infoFont);
			LabSecondInfo = new Label("", RectSecondInfo, true, true, SupportedStructures.OurFonts.infoFont);
			LabCurrentPrice = new Label("", RectCurrentPrice, true, true, SupportedStructures.OurFonts.infoFont);
		}

		public static void Skip()
		{
			IsShow = false;
			AIUnits.SkipObject();
		}

		public static void SkipCurrentStructureInfo()
		{
			CurrentStructureInfo = "";
		}

		public static void SetCurrentStructureInfo(string text)
		{
			CurrentStructureInfo = text;
		}

		public static bool IsButton()
		{
			return ObjSelected.Button;
		}

		public static Point GetPosSelectedObj()
		{
			return ObjSelected.Coords;
		}

		public static string GetRadSelectedObj()
		{
			return ObjSelected.Radius;
		}

		public static string GetTypeSelectedObj()
		{
			return ObjSelected.Type;
		}

		public static string GetFireRadSelectedObj()
		{
			return ObjSelected.FireRadius;
		}

		public static List<int> GetTexIndSelectedObj()
		{
			return ObjSelected.TextureIndex;
		}

		public static void ObjectIsSelected(TransInfo obj)
		{
			IsShow = true;
			ObjSelected = obj;
		}

		public static bool CheckClick()
		{
			if (!IsShow)
				return false;
			if (!ObjSelected.Button)
				return false;
			if ((Mouse.DX >= RectButton.X) && (Mouse.DY >= RectButton.Y) && (Mouse.DX <= RectButton.X + RectButton.Width) && (Mouse.DY <= RectButton.Y + RectButton.Height))
				return true;
			else
				return false;
		}

		public static void Process()
		{
			string temp = "Счет: " + AIUnits.Score.ToString() + '\n';
			temp += "Минералы: " + AIUnits.Minerals.ToString() + '\n';
			temp += "Земля: " + AIUnits.GetEarthHP().ToString() + '\n';
			LabStats.Name = temp;
			LabCurrentPrice.Name = CurrentStructureInfo;
			if (IsShow)
			{
				ObjSelected = AIUnits.CheckObject();
				if (ObjSelected != null)
				{
					LabFirstInfo.Name = ObjSelected.FirstInfo;
					LabSecondInfo.Name = ObjSelected.Name + '\n' + ObjSelected.SecondInfo;
				}
				else
				{
					Skip();
				}
			}
		}
	}
}
