using System;
using System.Collections.Generic;
using System.Text;

namespace SupportedStructures
{
    public struct Field
    {
        public int X;
        public int Y;
        public int Width;
        public int Height;
    }

    /// <summary>
    /// Current type of control (type of loaded texture)
    /// </summary>
    public enum View
    {
        current = 0, shine = 1, unable = 2, attention = 3
    }

    public enum ScreenMode
    {
        windowed = 0, fullscreen = 1
    }

    public enum OurFonts
    {
        mainMenu = 0, settingMenu = 1, smallFont = 2, infoFont = 3
    }
}
