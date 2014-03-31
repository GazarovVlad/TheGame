using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX;
using ProgramObjects.ScreenGroup;
using UserControl.Menus;
using TheGameDrawing.MeshRendering;
using TheGameDrawing.TextRendering;
using SupportedStructures;
using FisicalObjects;

namespace TheGameDrawing.DrawingSpace
{
    public static class Drawing
    {
        public static Microsoft.DirectX.Direct3D.Device OurDevice { get; private set; }
        public static Sprite OurSprite { get; private set; }

        public static void Initialize(System.Windows.Forms.Form OurForm)
        {
            PresentParameters presentParams = new PresentParameters();
            presentParams.SwapEffect = SwapEffect.Discard;
            Format current = Manager.Adapters[0].CurrentDisplayMode.Format;
            if (Screen.ScreenType == ScreenMode.fullscreen)
            {
                presentParams.Windowed = false;
                presentParams.BackBufferFormat = current;
                presentParams.BackBufferCount = 1;
                presentParams.BackBufferWidth = Screen.ResolutionW;
                presentParams.BackBufferHeight = Screen.ResolutionH;
            }
            else
            {
                presentParams.Windowed = true;
                OurForm.Size = new Size(Screen.ResolutionW, Screen.ResolutionH);
            }
            //presentParams.EnableAutoDepthStencil = true;
            //presentParams.AutoDepthStencilFormat = DepthFormat.D16;

            OurDevice = new Microsoft.DirectX.Direct3D.Device(0, DeviceType.Hardware, OurForm, CreateFlags.SoftwareVertexProcessing, presentParams);
            OurSprite = new Sprite(OurDevice);
            MeshRenderer.Initialize(OurDevice);
            TextRenderer.Initialize(OurDevice);
        }

        public static void Draw()
        {
            OurDevice.BeginScene();
            DrawGameProcess();
            OurDevice.EndScene();
            OurDevice.Present();
        }

        private static void DrawGameProcess()
        {
            //  Фон
            BackGround.DrawBlack();
            BackGround.DrawStars();

            //  Вспомогательные игровые объекты
            if (AIUnits.WorldExist)
                ConstrPanel.DrawFutureWays();

            // Отрисовка планеты
            MeshRenderer.planet.Render();

            //  Игровые объекты
            if (AIUnits.WorldExist)
                GameObjects.Draw();

            //  Рамки
            BackGround.DrawWorkSpace();
            MeshRenderer.planetIndicator.Render();
            BackGround.DrawMiniMap();
            BackGround.DrawMiniMapIndicator();
			if (AIUnits.WorldExist)
			{
				ConstrPanel.DrawBuildingIcons();
				InfoWindow.Draw();
			}

            //  Элементы контролируемые пользователем в данный момент времени
            if (AIUnits.WorldExist)
                ConstrPanel.DrawConstr();

            //  Меню
            if (MenuMain.Showed)
                Menus.DrawMainMenu();
        }

    }
}
