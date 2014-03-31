using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX.Direct3D;
using TheGame.ScreenGroup;
using FisicalObjects;
using TheGame.UserControlInterface;

namespace TheGame.Global.TextureSpace
{
    class Constructios
    {
        private const string PathBuildingConnectionPoint = "Sprites\\Constructions\\ConnectionPoint.png";
        private const string PathTowerBase = "Sprites\\Constructions\\Towers\\Base\\";
        private const string PathTowerTurret = "Sprites\\Constructions\\Towers\\Turrets\\";
        private const string BaseNameStarts = "Base";
        private const string TurretNameStarts = "Turret";
        private const string ConstructionIconsNameStarts = "ConstructionBuilding";
        private const string ConstructionIconsShineNameStarts = "ConstructionBuildingShine";
        private const string PathConstructionIcons = "Sprites\\Constructions\\Icons\\";
        private const string PathRadVisibl = "Sprites\\RadVisibl.png";
        private const string NameEnds = ".png";

        public static void Load()
        {
            radVisibl = TextureLoader.FromFile(Drawing.OurDevice, PathRadVisibl, ConstructionPanelControl.TextureRadVisiblSize, ConstructionPanelControl.TextureRadVisiblSize, 0, Usage.None, Format.Unknown, Pool.Default, Filter.None, Filter.None, 0);
            ConstructionIcons();
            Constructions();
        }

        private static void Constructions()
        {
            string name;
            int textureSize = (WorkSpace.RightBorder.Width - 3 * 10) / 2;  //  десятку брать из другого класа
            string path = PathBuildingConnectionPoint;
            Textures.constructionBuildings = new Texture[AIUnits.BuildingCount];
            Textures.constructionBuildings[0] = TextureLoader.FromFile(Drawing.OurDevice, path, textureSize, textureSize, 0, Usage.None, Format.Unknown, Pool.Default, Filter.None, Filter.None, 0);

            Textures.constructionTowerBase = new Texture[AIUnits.TowerBaseCount];
            for (int i = 0; i < AIUnits.TowerBaseCount; i++)
            {
                path = PathTowerBase;
                name = BaseNameStarts + (i + 1).ToString();
                path += name + NameEnds;
                Textures.constructionTowerBase[i] = TextureLoader.FromFile(Drawing.OurDevice, path, textureSize, textureSize, 0, Usage.None, Format.Unknown, Pool.Default, Filter.None, Filter.None, 0);
            }

            Textures.constructionTowerTurrets = new Texture[AIUnits.TowerTurretCount];
            for (int i = 0; i < AIUnits.TowerTurretCount; i++)
            {
                path = PathTowerTurret;
                name = TurretNameStarts + (i + 1).ToString();
                path += name + NameEnds;
                Textures.constructionTowerTurrets[i] = TextureLoader.FromFile(Drawing.OurDevice, path, textureSize, textureSize, 0, Usage.None, Format.Unknown, Pool.Default, Filter.None, Filter.None, 0);
            }
        }

        private static void ConstructionIcons()
        {
            string path;
            Textures.constructionIcons = new Texture[ConstructionPanelControl.NumberOfSlots];
            Textures.constructionIconsShine = new Texture[ConstructionPanelControl.NumberOfSlots];
            for (int i = 0; i < Textures.constructionIcons.Length; i++)
            {
                path = PathConstructionIcons + ConstructionIconsNameStarts + (i + 1).ToString() + NameEnds;
                Textures.constructionIcons[i] = TextureLoader.FromFile(Drawing.OurDevice, path, ConstructionPanelControl.SlotSize, ConstructionPanelControl.SlotSize, 0, Usage.None, Format.Unknown, Pool.Default, Filter.None, Filter.None, 0);
                path = PathConstructionIcons + ConstructionIconsShineNameStarts + (i + 1).ToString() + NameEnds;
                Textures.constructionIconsShine[i] = TextureLoader.FromFile(Drawing.OurDevice, path, ConstructionPanelControl.SlotSize, ConstructionPanelControl.SlotSize, 0, Usage.None, Format.Unknown, Pool.Default, Filter.None, Filter.None, 0);
            }
        }
    }
}
