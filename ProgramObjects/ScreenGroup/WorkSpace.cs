using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using SupportedStructures;
using ProgramObjects.InputDevices;
using ProgramObjects.InputDevices.Keyboard;
using GraphicObjects.StarsSpace;

namespace ProgramObjects.ScreenGroup
{
	public static class WorkSpace
	{
		public static Field Space;
		public const int MapLen = 3072;

		public static int DX { get; private set; }  //  Координаты на окне или экране
		public static int DY { get; private set; }
		public static int Step { get; private set; }

		public static Field MiniMapBorder;
		public static Field DownBorder;
		public static Field RightBorder;
		public static Field LeftBorder;
		public static Field UpBorder;

		public const int Frame = 5;

		private static int Rad;

		private static int SpaceX;
		private static int SpaceY;
		private static int DownX;
		private static int DownY;

		public static void Initialize(double persentRad, int step)
		{
			Step = step;
			MiniMapBorder.Width = MiniMap.Size + 2 * Frame;
			MiniMapBorder.Height = MiniMapBorder.Width;
			RightBorder.Width = MiniMapBorder.Width;
			DownBorder.Height = MiniMapBorder.Height;
			LeftBorder.Width = Frame;
			UpBorder.Height = Frame;
			Space.Width = Screen.Width - LeftBorder.Width - RightBorder.Width;
			Space.Height = Screen.Height - DownBorder.Height - UpBorder.Height;
			Space.X = (int)(MapLen / 2.0 - Space.Width / 2.0);
			Space.Y = (int)(MapLen / 2.0 - Space.Height / 2.0);
			Rad = (int)(Screen.Width * persentRad);
			MiniMap.InitializeMiniMap();

			MiniMapBorder.X = MiniMap.DX - Frame;
			MiniMapBorder.Y = MiniMap.DY - Frame;

			RightBorder.X = MiniMapBorder.X;
			RightBorder.Y = 0;
			RightBorder.Height = MiniMapBorder.Y;

			DownBorder.X = 0;
			DownBorder.Y = MiniMapBorder.Y;
			DownBorder.Width = MiniMapBorder.X;

			LeftBorder.X = 0;
			LeftBorder.Y = 0;
			LeftBorder.Height = MiniMapBorder.Y;

			UpBorder.X = Frame;
			UpBorder.Y = 0;
			UpBorder.Width = MiniMapBorder.X - Frame;

			DX = LeftBorder.Width;
			DY = UpBorder.Height;
			MiniMap.InitializeMiniScreen();
		}

		public static void MoveWindowed()
		{
			if ((Mouse.DY >= DY) && (Mouse.DY <= Space.Height + DY))
			{
				if ((Mouse.DX <= Space.Width + DX) && (Mouse.DX >= Space.Width + DX - Rad))
				{
					if (Space.X + Space.Width + Step <= MapLen)
						Space.X += Step;
					else
						Space.X = MapLen - Space.Width;
				}
				if ((Mouse.DX >= DX) && (Mouse.DX <= Rad + DX))
				{
					if (Space.X - Step >= 0)
						Space.X -= Step;
					else
						Space.X = 0;
				}
			}
			if ((Mouse.DX >= DX) && (Mouse.DX <= Space.Width + DX))
			{
				if ((Mouse.DY <= Space.Height + DY) && (Mouse.DY >= Space.Height + DY - Rad))
				{
					if (Space.Y + Space.Height + Step <= MapLen)
						Space.Y += Step;
					else
						Space.Y = MapLen - Space.Height;
				}
				if ((Mouse.DY >= DY) && (Mouse.DY <= DY + Rad))
				{
					if (Space.Y - Step >= 0)
						Space.Y -= Step;
					else
						Space.Y = 0;
				}
			}
			Stars.Space = Space;
		}

		public static void MoveFullScreen()
		{
			if (Mouse.DX == Screen.Width - 1)
			{
				if (Space.X + Space.Width + Step <= MapLen)
					Space.X += Step;
				else
					Space.X = MapLen - Space.Width;
			}
			if (Mouse.DX == 0)
			{
				if (Space.X - Step >= 0)
					Space.X -= Step;
				else
					Space.X = 0;
			}
			if (Mouse.DY == Screen.Height - 1)
			{
				if (Space.Y + Space.Height + Step <= MapLen)
					Space.Y += Step;
				else
					Space.Y = MapLen - Space.Height;
			}
			if (Mouse.DY == 0)
			{
				if (Space.Y - Step >= 0)
					Space.Y -= Step;
				else
					Space.Y = 0;
			}
			Stars.Space = Space;
		}

		public static void MoveKeyboard()
		{
			if (!Mouse.RDown)
			{
				if (NavigateKeys.rightDown)
				{
					if (Space.X + Space.Width + Step <= MapLen)
						Space.X += Step;
					else
						Space.X = MapLen - Space.Width;
				}
				if (NavigateKeys.downDown)
				{
					if (Space.Y + Space.Height + Step <= MapLen)
						Space.Y += Step;
					else
						Space.Y = MapLen - Space.Height;
				}
				if (NavigateKeys.leftDown)
				{
					if (Space.X - Step >= 0)
						Space.X -= Step;
					else
						Space.X = 0;
				}
				if (NavigateKeys.upDown)
				{
					if (Space.Y - Step >= 0)
						Space.Y -= Step;
					else
						Space.Y = 0;
				}
			}
			Stars.Space = Space;
		}

		public static void MoveByMiniMap()
		{
			Point temp = MiniMap.GetMovedPozition();
			Space.X = temp.X;
			Space.Y = temp.Y;
			Stars.Space = Space;
		}

		public static void FirstDown(int x, int y)
		{
			SpaceX = Space.X;
			SpaceY = Space.Y;
			DownX = Mouse.DownX;
			DownY = Mouse.DownY;
		}

		public static void MoveByMouseDown()
		{
			//if()
			int moveX, moveY;
			int dx, dy;
			int x = Mouse.DX;
			int y = Mouse.DY;
			dx = x - DownX;
			dy = y - DownY;

			if (dx > 0)
			{
				if ((moveX = SpaceX - dx) >= 0)
					Space.X = moveX;
				else//if(S)
					Space.X = 0;
			}
			else
			{
				if ((moveX = SpaceX + Math.Abs(dx)) + Space.Width <= MapLen)
					Space.X = moveX;
				else//if(S)
					Space.X = MapLen - Space.Width;
			}

			if (dy > 0)
			{
				if ((moveY = SpaceY - dy) >= 0)
					Space.Y = moveY;
				else//if(S)
					Space.Y = 0;
			}
			else
			{
				if ((moveY = SpaceY + Math.Abs(dy)) + Space.Height <= MapLen)
					Space.Y = moveY;
				else//if(S)
					Space.Y = MapLen - Space.Height;
			}
		}
	}
}
