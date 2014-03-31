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
            if (ScreenType == ScreenMode.fullscreen)
                WorkSpace.MoveFullScreen();
            else
                WorkSpace.MoveWindowed();

            //  Keyboard
            WorkSpace.MoveKeyboard();

            //  MiniMap
            if (Mouse.Pressed)
                if ((Mouse.PresedDX > MiniMap.DX) && (Mouse.PresedDX < MiniMap.DX + MiniMap.Size) && (Mouse.PresedDY > MiniMap.DY) && (Mouse.PresedDY < MiniMap.DY + MiniMap.Size))
                    WorkSpace.MoveByMiniMap();
        }
    }
}