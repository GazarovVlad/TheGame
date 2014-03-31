using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Microsoft.DirectX.Direct3D;
using TheGameDrawing.TextureSpace.TextureLoders;

namespace TheGameDrawing.TextureSpace
{
    public static class Textures
    {
        public static Texture blackBackGround;

        public static Texture[][] allStars;
        
        public static Texture miniMap;
		public static Texture miniScreen;
		public static SpecialTextures miniBuildings;
		public static SpecialTextures miniTowers;
		public static SpecialTextures miniMinerals;
		public static SpecialTextures miniAsteroids;
        public static SpecialTextures miniAliens;

        public static Texture mainMenu;
        public static Texture mainMenuBackGround;

        public static Texture workSpMiniMapBorder;
        public static Texture workSpRightBorder;
        public static Texture workSpDownBorder;
        public static Texture workSpUpBorder;
        public static Texture workSpLeftBorder;

        public static Texture[] constrBuildings;
        public static Texture[] constrTowerBase;
        public static Texture[] constrTowerTurrets;

		public static Texture[][][] minerals;
        public static Texture[][] asteroids;
        public static Texture[][] aliens;

        public static Texture[] constrIcons;
        public static Texture[] constrIconsShine;
        public static Texture radVisibl;
        public static Texture iconBorder;
		public static SpecialTextures radAttack;

        public static Texture[][][] allExplosions;  //  [тип взрыва][тип стадии][стадия]

		public static Texture[] checkBoxes;

		public static Texture infoWindowStats;
		public static Texture infoWindowButton;
		public static Texture infoWindowIcon;
		public static Texture infoWindowFirstInfo;
		public static Texture infoWindowSecondInfo;
		public static Texture infoStructurePrice;
		public static SpecialTextures infoRads;

        public static void Load()
        {
            BackGroundTex.Load();
			BordersTex.Load();
			ConstructiosText.Load();
			ExplosionsTex.Load();
			MenuTex.Load();
			MiniMapTex.Load();
			StarsTex.Load();
			MineralsTex.Load();
            AliensTex.Load();
            AsteroidsTex.Load();
			UserInterfaceTex.Load();
			InfoWindowTex.Load();
        }
    }
}
