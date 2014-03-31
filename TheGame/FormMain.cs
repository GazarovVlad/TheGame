using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using UserControl.Interface;
using UserControl.Menus;
using UserControl;
using GraphicObjects.StarsSpace;
using GraphicObjects.ExplosionsSpace;
using ProgramObjects.InputDevices;
using ProgramObjects.InputDevices.Keyboard;
using ProgramObjects.ScreenGroup;
using ProgramObjects.Settings;
using TheGameDrawing.DrawingSpace;
using TheGameDrawing.TextureSpace;
using TheGameDrawing.MeshRendering.RenderingObjects;
using FisicalObjects;
using SupportedStructures;

namespace TheGame
{
    public partial class FormMain : Form
    {
        public static bool formIsActiv { private set; get; }
        private static bool ended = false;
        private static Point pos;
        private static bool clicked;

        public FormMain()
        {
            InitializeComponent();
            Keyboard.Inicialize();
			Settings.LoadFromFile();
            AIUnits.Inicialize(WorkSpace.MapLen,Planet.PlanetRadius);
            Drawing.Initialize(this);
            MenuMain.Initialize();
			ConstrPanelControl.Initialize();
			InformationWindow.Initialize(WorkSpace.DownBorder, WorkSpace.MiniMapBorder, WorkSpace.Frame);
            Textures.Load();
        }

        private void timerFirstStart_Tick(object sender, EventArgs e)   // Ожидание загрузки формы
        {
            pos = Point.Empty;
            clicked = false;
            formIsActiv = true;
            Mouse.EndTact();
            Keyboard.EndTact();
            timerTact.Start();
            timerFirstStart.Stop();
            timerFirstStart.Enabled = false;
            ended = true;
        }

        private void timerTact_Tick(object sender, EventArgs e)
        {
            if (ended)
            {
                ended = false;
                MouseBarrierProcess();
                if (Form.ActiveForm == this)
                    formIsActiv = true;
                else
                    formIsActiv = false;
                if (formIsActiv)
                {
                    Mouse.Find(MousePosition.X, MousePosition.Y, this.Location.X, this.Location.Y);
                    UserActions.Process();
                    if (!MenuMain.Showed)
                    {
                        ProgramObjects.ScreenGroup.Screen.Move();
                        Stars.ModifyPositions();
                        MiniMap.MiniScreenMove();
                        if (AIUnits.WorldExist)
                        {
                            AIUnits.Process();
							Explosions.Process();
							InformationWindow.Process();
                        }
                    }
                    if (MenuMain.restartApp)
                        timerRestart.Start();
                    if (MenuMain.closeApp)
                        Application.Exit();
                }
                Mouse.EndTact();
                Keyboard.EndTact();
                Drawing.Draw();
                MouseControl.Process();
                ended = true;
            }
        }

        private void FormMain_KeyUp(object sender, KeyEventArgs e)
        {
            Keyboard.AnyKeyUp(e);
        }

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            //Explosions.DestroyEarth(new Point(WorkSpace.MapLen / 2, WorkSpace.MapLen / 2), 250);
            Keyboard.AnyKeyDown(e);
        }

        private void FormMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (ProgramObjects.ScreenGroup.Screen.ScreenType == ScreenMode.fullscreen)
            {
                pos = MousePosition;
                clicked = true;
            }
            else
            {
                pos = new Point(e.X, e.Y);
                clicked = true;
            }
        }

        private void MouseBarrierProcess()
        {
            if (clicked)
                Mouse.Press(pos.X, pos.Y);
            clicked = false;
        }

        private void timerRestart_Tick(object sender, EventArgs e)
        {
            //Save game
            MenuMain.restartApp = false;
            Application.Restart();
        }
    }
}