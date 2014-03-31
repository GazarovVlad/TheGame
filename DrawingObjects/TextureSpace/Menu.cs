using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX.Direct3D;

namespace TheGame.Global.TextureSpace
{
    class Menu
    {
        private const string PathMainMenu = "Sprites\\Menus\\MainMenu\\MainMenu.png";
        private const string PathMainMenuButtons = "Sprites\\Menus\\MainMenu\\Buttons\\Button\\";
        private const string PathMainMenuButtonsShine = "Sprites\\Menus\\MainMenu\\Buttons\\ButtonShine\\";
        private const string NameEnds = ".png";

        public static void Load()
        {
            Textures.mainMenu = TextureLoader.FromFile(Drawing.OurDevice, PathMainMenu, MainMenu.TextureMenuWidth, MainMenu.TextureMenuHeight, 0, Usage.None, Format.Unknown, Pool.Default, Filter.None, Filter.None, 0);
            MainMenuButtons();
        }

        private static void MainMenuButtons()
        {
            string path;
            Textures.mainMenuButtons = new Texture[MainMenu.ButtonNames.Length];
            Textures.mainMenuButtonsShine = new Texture[MainMenu.ButtonNames.Length];
            for (int i = 0; i < Textures.mainMenuButtons.Length; i++)
            {
                path = PathMainMenuButtons + MainMenu.ButtonNames[i] + NameEnds;
                Textures.mainMenuButtons[i] = TextureLoader.FromFile(Drawing.OurDevice, path, MainMenu.TextureButtonWidth, MainMenu.TextureButtonHeight, 0, Usage.None, Format.Unknown, Pool.Default, Filter.None, Filter.None, 0);
                path = PathMainMenuButtonsShine + MainMenu.ButtonNames[i] + NameEnds;
                Textures.mainMenuButtonsShine[i] = TextureLoader.FromFile(Drawing.OurDevice, path, MainMenu.TextureButtonWidth, MainMenu.TextureButtonHeight, 0, Usage.None, Format.Unknown, Pool.Default, Filter.None, Filter.None, 0);
            }
        }
    }
}
