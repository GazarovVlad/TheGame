using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ProgramObjects.InputDevices.Keyboard
{
    static class NavigateKeys
    {
        public static bool leftDown = false;
        public static bool rightDown = false;
        public static bool upDown = false;
        public static bool downDown = false;

        public static void ProcessKeyDown(KeyEventArgs downKey)
        {
            if (downKey.KeyCode == Keys.Left)
                leftDown = true;
            if (downKey.KeyCode == Keys.Right)
                rightDown = true;
            if (downKey.KeyCode == Keys.Up)
                upDown = true;
            if (downKey.KeyCode == Keys.Down)
                downDown = true;
        }

        public static void ProcessKeyUp(KeyEventArgs upKey)
        {
            if (upKey.KeyCode == Keys.Left)
                leftDown = false;
            if (upKey.KeyCode == Keys.Right)
                rightDown = false;
            if (upKey.KeyCode == Keys.Up)
                upDown = false;
            if (upKey.KeyCode == Keys.Down)
                downDown = false;
        }
    }
}
