using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using ProgramObjects.InputDevices;
using SupportedStructures;

namespace ProgramObjects.ScreenGroup
{
    public static class Screen
    {
        public static ScreenMode ScreenType { get; private set; }
        public static int Height { get; private set; }
        public static int Width { get; private set; }
        public static int ResolutionH { get; private set; }
        public static int ResolutionW { get; private set; }
        public static int BorderLow { get; private set; }
		public static int BorderHigh { get; private set; }

		public static void Initialize(int width, int height, ScreenMode screenType, double persentRad, int borderLow, int borderHigh, int step)
		{
			ScreenType = screenType;
			ResolutionH = height;
			ResolutionW = width;
			Width = width;
			Height = height;
			if (ScreenType == ScreenMode.windowed)
			{
				Width -= 2 * borderLow;
				Height -= borderLow + borderHigh;
			}
			BorderLow = borderLow;
			BorderHigh = borderHigh;

			WorkSpace.Initialize(persentRad, step);
		}

        public static void Move()
        {
            //  Mouse
            if (!Mouse.RDown)
            {
                if (ScreenType == ScreenMode.fullscreen)
                    WorkSpace.MoveFullScreen();
                /*else
					WorkSpace.MoveWindowed();*/
            }

            //  Keyboard
			WorkSpace.MoveKeyboard();

            //  MiniMap
            if (Mouse.LDown)
                if ((Mouse.DX > MiniMap.DX) && (Mouse.DX < MiniMap.DX + MiniMap.Size) && (Mouse.DY > MiniMap.DY) && (Mouse.DY < MiniMap.DY + MiniMap.Size))
                    WorkSpace.MoveByMiniMap();

            //  MouseDoun
            // зажим мыши//перемещение зажимом мыши
            if (Mouse.RDown && ((Mouse.DX >= WorkSpace.DX) && (Mouse.DY >= WorkSpace.DY) && (Mouse.DX <= WorkSpace.DX + WorkSpace.Space.Width) && (Mouse.DY <= WorkSpace.DY + WorkSpace.Space.Height)))
            {
                WorkSpace.MoveByMouseDown();
            }
        }
    }
}