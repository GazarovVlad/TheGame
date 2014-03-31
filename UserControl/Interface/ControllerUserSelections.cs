using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FisicalObjects;
using FisicalObjects.Transist;
using GraphicObjects.ExplosionsSpace;
using ProgramObjects.InputDevices;
using ProgramObjects.ScreenGroup;
using SupportedStructures;

namespace UserControl.Interface
{
    public static class ControllerUserSelections
    {
        public static void ProcessConstrSelection()
        {
			TransInfo obj = AIUnits.GetObject(new Point(Mouse.PresedDX - WorkSpace.DX + WorkSpace.Space.X, Mouse.PresedDY - WorkSpace.DY + WorkSpace.Space.Y));
			if (obj != null)
			{
				InformationWindow.ObjectIsSelected(obj);
			}
			else
			{
				InformationWindow.Skip();
			}
        }
    }
}
