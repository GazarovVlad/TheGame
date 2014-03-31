using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ProgramObjects.InputDevices.Keyboard
{
    public static class Keyboard
    {
        public static bool Pressed { get; private set; }

        private static KeyEventArgs PressedKey;

        public static void Inicialize()
        {
            NavigateKeys.leftDown = false;
            NavigateKeys.rightDown = false;
            NavigateKeys.upDown = false;
            NavigateKeys.downDown = false;
            OtherKeys.Initialize();
            Pressed = false;
            PressedKey = null;
        }

        public static void AnyKeyDown(KeyEventArgs key)
        {
            Pressed = true;
            PressedKey = key;
            if (IsItNavigateKey(key))
                NavigateKeys.ProcessKeyDown(key);
            if (HotKeys.IsHotKeyPressed(key.KeyValue))
                HotKeys.Pressed = true;
            OtherKeys.ProcessKeyDown(key);
        }

        public static void AnyKeyUp(KeyEventArgs key)
        {
            if (IsItNavigateKey(key))
                NavigateKeys.ProcessKeyUp(key);
            OtherKeys.ProcessKeyUp();
        }

        public static bool IsEscapeKeyPressed()
        {
            if ((Pressed) && (PressedKey.KeyCode == Keys.Escape))
                return true;
            else
                return false;
        }

        public static void EndTact()
        {
            Pressed = false;
        }

        private static bool IsItNavigateKey(KeyEventArgs key)
        {
            if ((key.KeyCode == Keys.Left) || (key.KeyCode == Keys.Right) || (key.KeyCode == Keys.Up) || (key.KeyCode == Keys.Down))
                return true;
            else
                return false;
        }
    }
}
