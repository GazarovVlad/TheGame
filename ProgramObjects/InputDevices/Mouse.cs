using System;
using System.Collections.Generic;
using System.Text;
using ProgramObjects.ScreenGroup;
using SupportedStructures;

namespace ProgramObjects.InputDevices
{
	public static class Mouse
	{
		public static int DX { get; private set; }
		public static int DY { get; private set; }
		public static int PresedDX { get; private set; }
		public static int PresedDY { get; private set; }
		public static bool Pressed { get; private set; }
		public static bool LDown { get; private set; }
		public static bool RDown { get; private set; }
		public static int DownX { get; private set; }
		public static int DownY { get; private set; }
		//public static int UpX { get; private set; }
		//public static int UpY { get; private set; }


		public static void Find(int mousePositionX, int mousePositionY, int FormLocationX, int FormLocationY)
		{
			if (Screen.ScreenType == ScreenMode.fullscreen)
				FindFullScreen(mousePositionX, mousePositionY);
			else
				FindWindowed(mousePositionX, mousePositionY, FormLocationX, FormLocationY);
		}

		public static void Press(int x, int y)
		{
			PresedDX = x;
			PresedDY = y;
			Pressed = true;
			LDown = true;
		}

		public static void MouseDown(int x, int y)
		{
			RDown = true;
			DownX = x;
			DownY = y;
			WorkSpace.FirstDown(x, y);
		}

		public static void EndTact()
		{
			Pressed = false;
		}

		private static void FindWindowed(int mousePositionX, int mousePositionY, int FormLocationX, int FormLocationY)
		{
			DX = mousePositionX - FormLocationX - Screen.BorderLow;
			DY = mousePositionY - FormLocationY - Screen.BorderHigh;
		}

		private static void FindFullScreen(int mousePositionX, int mousePositionY)
		{
			DX = mousePositionX;
			DY = mousePositionY;
		}

		public static void Up()
		{
			RDown = false;
			LDown = false;
			//UpX = x;
			//UpY = y;
		}
	}
}
