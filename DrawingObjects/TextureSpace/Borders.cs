using System;
using System.Collections.Generic;
using Microsoft.DirectX.Direct3D;
using TheGame.ScreenGroup;

namespace TheGame.Global.TextureSpace
{
    static class Borders
    {
        private const string PathWorkSpaceMiniMapBorder = "Sprites\\WorkSpace\\MiniMapBorder.png";
        private const string PathWorkSpaceRightBorder = "Sprites\\WorkSpace\\RightBorder.png";
        private const string PathWorkSpaceDownBorder = "Sprites\\WorkSpace\\DownBorder.png";
        private const string PathWorkSpaceLeftBorder = "Sprites\\WorkSpace\\LeftBorder.png";
        private const string PathWorkSpaceUpBorder = "Sprites\\WorkSpace\\UpBorder.png";

        public static void Load()
        {
            Border();
        }

        private static void Border()
        {
            Textures.workSpMiniMapBorder = TextureLoader.FromFile(Drawing.OurDevice, PathWorkSpaceMiniMapBorder, WorkSpace.MiniMapBorder.Width, WorkSpace.MiniMapBorder.Height, 0, Usage.None, Format.Unknown, Pool.Default, Filter.None, Filter.None, 0);
            Textures.workSpRightBorder = TextureLoader.FromFile(Drawing.OurDevice, PathWorkSpaceRightBorder, WorkSpace.RightBorder.Width, WorkSpace.RightBorder.Height, 0, Usage.None, Format.Unknown, Pool.Default, Filter.None, Filter.None, 0);
            Textures.workSpDownBorder = TextureLoader.FromFile(Drawing.OurDevice, PathWorkSpaceDownBorder, WorkSpace.DownBorder.Width, WorkSpace.DownBorder.Height, 0, Usage.None, Format.Unknown, Pool.Default, Filter.None, Filter.None, 0);
            Textures.workSpLeftBorder = TextureLoader.FromFile(Drawing.OurDevice, PathWorkSpaceLeftBorder, WorkSpace.LeftBorder.Width, WorkSpace.LeftBorder.Height, 0, Usage.None, Format.Unknown, Pool.Default, Filter.None, Filter.None, 0);
            Textures.workSpUpBorder = TextureLoader.FromFile(Drawing.OurDevice, PathWorkSpaceUpBorder, WorkSpace.UpBorder.Width, WorkSpace.UpBorder.Height, 0, Usage.None, Format.Unknown, Pool.Default, Filter.None, Filter.None, 0);
        }
    }
}
