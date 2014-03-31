using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX.Direct3D;
using TheGameDrawing.DrawingSpace;
using UserControl.Menus;

namespace TheGameDrawing.TextureSpace.TextureLoders
{
    class MenuTex
    {
        private const string PathMainMenu = "Sprites\\Menus\\MainMenu.png";
        private const string PathBackGround = "Sprites\\Menus\\BackGround.png";
        private const string PathCheckBox = "Sprites\\Elements\\checkbox";
        private const string End = ".png";

        public static void Load()
        {
            Textures.mainMenu = TextureLoader.FromFile(Drawing.OurDevice, PathMainMenu, MenuMain.TextureMenuWidth, MenuMain.TextureMenuHeight, 0, Usage.None, Format.Unknown, Pool.Default, Filter.None, Filter.None, 0);
            Textures.mainMenuBackGround = TextureLoader.FromFile(Drawing.OurDevice, PathBackGround, MenuMain.TextureBackGroundWidth, MenuMain.TextureBackGroundHeight, 0, Usage.None, Format.Unknown, Pool.Default, Filter.None, Filter.None, 0);
            Textures.checkBoxes = new Texture[6];
            for(int i = 0; i < Textures.checkBoxes.Length; i ++)
                Textures.checkBoxes[i] = TextureLoader.FromFile(Drawing.OurDevice, PathCheckBox + (i+1).ToString() + End, 20, 20, 0, Usage.None, Format.Unknown, Pool.Default, Filter.None, Filter.None, 0);
        }
    }
}
