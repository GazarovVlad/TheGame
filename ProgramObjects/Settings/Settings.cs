using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ProgramObjects.ScreenGroup;
using GraphicObjects.StarsSpace;
using SupportedStructures;

namespace ProgramObjects.Settings
{
    public static class Settings
    {
        public static void LoadFromFile()
        {
            ScreenSett.Load();
            StarsSett.Load();
            ExplosionsSett.Load();
        }
    }
}
