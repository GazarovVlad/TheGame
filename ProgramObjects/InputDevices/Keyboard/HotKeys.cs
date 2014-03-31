using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramObjects.InputDevices.Keyboard
{
	public static class HotKeys
	{
		public static int Number { get; private set; }

		#region int[35] Keys
		private static int[] Keys = 
        {
            0x31,
            0x32,
            0x33,
            0x34,
            0x35,
            0x36,
            0x37,
            0x38,
            0x39,
            0x30,
            0x51,
            0x57,
            0x45,
            0x52,
            0x54,
            0x59,
            0x55,
            0x49,
            0x4f,
            0x50,
            0x41,
            0x53,
            0x44,
            0x46,
            0x47,
            0x48,
            0x4a,
            0x4b,
            0x4c,
            0x5a,
            0x58,
            0x43,
            0x56,
            0x42,
            0x4e,
            0x4d
        };
		#endregion
		private static int Count;
		private static bool pressed;

		public static bool Pressed
		{
			get
			{
				bool ret = pressed;
				pressed = false;
				return ret;
			}
			set
			{
				pressed = value;
			}
		}

		public static void Initialize(int count)
		{
			Count = count;
			Pressed = false;
		}

		public static bool IsHotKeyPressed(int code)
		{
			for (Number = 0; Number < Count; Number++)
			{
				if (Keys[Number] == code)
					return true;
			}
			return false;
		}
	}
}
