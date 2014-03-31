using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using ProgramObjects.InputDevices;
using ProgramObjects.InputDevices.Keyboard;
using ProgramObjects.ScreenGroup;
using UserControl.Interface;
using UserControl.Menus;
using SupportedStructures;
using FisicalObjects;

namespace UserControl
{
    public static class UserActions
    {
        public static void Process()
        {
            if (MenuMain.Showed)
                MenuMain.MousePositionControl();
            else
            {
                if (AIUnits.WorldExist)
                {
                    if (Mouse.Pressed)
                    {
						if ((Mouse.DX >= WorkSpace.DX) && (Mouse.DY >= WorkSpace.DY) && (Mouse.DX <= WorkSpace.DX + WorkSpace.Space.Width) && (Mouse.DY <= WorkSpace.DY + WorkSpace.Space.Height))
							ControllerUserSelections.ProcessConstrSelection();	// клик был в WorkSpace, подозрение на попытку выделить постройку
						else
						{
							if (InformationWindow.CheckClick())
							{
								int[] constr = AIUnits.GetConstr(InformationWindow.GetPosSelectedObj());
								AIUnits.DestroyConstr(constr[0], constr[1]);
								InformationWindow.Skip();
							}
						}
						if (ConstrPanelControl.ProcessMouse())	// возможно клик относится к панели построек (выбор постройки или ее размещение)
							InformationWindow.Skip();
                    }
                }
            }
            if (Keyboard.Pressed)
			{
				ConstrPanelControl.ProcessKeys();
                if (Keyboard.IsEscapeKeyPressed())
                {
					bool flag = true;
					if (AIUnits.WorldExist && ConstrPanelControl.ConstrSelected && flag)
					{
						flag = false;
						ConstrPanelControl.Skip();
					}
					if (AIUnits.WorldExist && InformationWindow.IsShow && flag)
					{
						flag = false;
						InformationWindow.Skip();
					}
					if (flag)
						MenuMain.Showed = true;
				}
            }
        }
    }
}
