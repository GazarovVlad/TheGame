using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ProgramObjects.InputDevices.Keyboard
{
    public static class OtherKeys
    {
        public static string CurrentPressedKey { get; private set; }
        public static bool EnterWasPressed { get; private set; }
        public static bool Flag { get; private set; }

        public static void Initialize()
        {
            Flag = true;
        }

        public static void Pick()
        {
            Flag = false;
        }

        public static void ProcessKeyDown(KeyEventArgs downKey)
        {
            CurrentPressedKey = downKey.KeyData.ToString();
            if (downKey.KeyCode == Keys.Return)
                EnterWasPressed = true;
        }

        public static void ProcessKeyUp()
        {
            Flag = true;
        }
    }
}
