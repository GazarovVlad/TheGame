using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.Direct3D;
using System.Drawing;
using TheGameDrawing.DrawingSpace;
using UserControl.Menus.Elements;
using SupportedStructures;

namespace TheGameDrawing.TextRendering
{
    public static class TextRenderer
    {
        private static Dictionary<int, Microsoft.DirectX.Direct3D.Font> Fonts;

        public static void Initialize(Microsoft.DirectX.Direct3D.Device device)
        {
            Fonts = new Dictionary<int, Microsoft.DirectX.Direct3D.Font>();
            //FontFamily[] fontFamily = FontFamily.Families; все поддерживаемые шрифты

            System.Drawing.Font font = new System.Drawing.Font("Constantia", 13.5f);
            Microsoft.DirectX.Direct3D.Font Font = new Microsoft.DirectX.Direct3D.Font(device, font);
            Fonts.Add((int)OurFonts.mainMenu, Font);

            font = new System.Drawing.Font("Arial", 10.5f);
            Font = new Microsoft.DirectX.Direct3D.Font(device, font);
            Fonts.Add((int)OurFonts.settingMenu, Font);

            font = new System.Drawing.Font("Arial", 7.0f);
            Font = new Microsoft.DirectX.Direct3D.Font(device, font);
			Fonts.Add((int)OurFonts.smallFont, Font);

			font = new System.Drawing.Font("Arial", 8.5f);
			Font = new Microsoft.DirectX.Direct3D.Font(device, font);
			Fonts.Add((int)OurFonts.infoFont, Font);
        }

        public static void DrawText(Button b, Color c)
        {
            Fonts[(int)b.FontName].DrawText(Drawing.OurSprite, b.Name, b.Rect, DrawTextFormat.Center, c);
        }

        public static void DrawText(CheckBox cb, Color c)
        {
            Fonts[(int)cb.FontName].DrawText(Drawing.OurSprite, cb.Name, cb.Rect, DrawTextFormat.Right, c);
        }

        public static void DrawText(Label l, Color c)
        {
            Fonts[(int)l.FontName].DrawText(Drawing.OurSprite, l.Name, l.Rect, DrawTextFormat.Center, c);
		}

        public static void DrawText(ListBox lb, Color c)
        {
            Fonts[(int)lb.FontName].DrawText(Drawing.OurSprite, lb.Name, lb.Rect, DrawTextFormat.Left, c);
        }
    }
}
