using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Microsoft.DirectX.Direct3D;
using TheGameDrawing.TextureSpace;
using UserControl.Menus;
using UserControl.Menus.Elements;
using SupportedStructures;
using TheGameDrawing.TextRendering;

namespace TheGameDrawing.DrawingSpace
{
    static class Menus
    {
        private static Color colorCurrent = Color.FromArgb(230, 230, 230);
        private static Color colorShine = Color.Gold;
        private static Color colorUnable = Color.Gray;
        private static Color colorAttention = Color.Red;

        public static void DrawMainMenu()
        {
            Drawing.OurSprite.Begin(SpriteFlags.AlphaBlend);
            if (MenuMain.ShowedBackGround)
                Drawing.OurSprite.Draw2D(Textures.mainMenuBackGround, Point.Empty, 0, Point.Empty, Color.White);
            Drawing.OurSprite.Draw2D(Textures.mainMenu, Point.Empty, 0, MenuMain.Position, Color.White);

            for (int i = 0; i < MenuMain.Menus.Length; i++)
                if (MenuMain.Menus[i].Showed)
                {
                    for (int j = 0; j < MenuMain.Menus[i].Buttons.Count; j++)
                    {
                        if (MenuMain.Menus[i].Buttons[j].View == View.current && MenuMain.Menus[i].Buttons[j].Showed)
                            TextRenderer.DrawText(MenuMain.Menus[i].Buttons[j], colorCurrent);
                        if (MenuMain.Menus[i].Buttons[j].View == View.shine && MenuMain.Menus[i].Buttons[j].Showed)
                        {
                            TextRenderer.DrawText(MenuMain.Menus[i].Buttons[j], colorShine);
                            if (MenuMain.Menus[i].Buttons[j].Hint != null)
                                TextRenderer.DrawText(MenuMain.Menus[i].Buttons[j].Hint, colorCurrent);
                        }
                        if (MenuMain.Menus[i].Buttons[j].View == View.unable && MenuMain.Menus[i].Buttons[j].Showed)
                            TextRenderer.DrawText(MenuMain.Menus[i].Buttons[j], colorUnable);
                        if (MenuMain.Menus[i].Buttons[j].View == View.attention && MenuMain.Menus[i].Buttons[j].Showed)
                            TextRenderer.DrawText(MenuMain.Menus[i].Buttons[j], colorAttention);
                    }
                    for (int j = 0; j < MenuMain.Menus[i].CheckBoxes.Count; j++)
                    {
                        for (int k = 0; k < MenuMain.Menus[i].CheckBoxes[j].Count; k++)
                        {
                            if (MenuMain.Menus[i].CheckBoxes[j][k].View == View.current && MenuMain.Menus[i].CheckBoxes[j][k].Showed)
                            {
                                if (MenuMain.Menus[i].CheckBoxes[j][k].State)
                                    Drawing.OurSprite.Draw2D(Textures.checkBoxes[0], Point.Empty, 0, MenuMain.Menus[i].CheckBoxes[j][k].Rect.Location, Color.White);
                                else
                                    Drawing.OurSprite.Draw2D(Textures.checkBoxes[3], Point.Empty, 0, MenuMain.Menus[i].CheckBoxes[j][k].Rect.Location, Color.White);
                                TextRenderer.DrawText(MenuMain.Menus[i].CheckBoxes[j][k], colorCurrent);
                            }
                            if (MenuMain.Menus[i].CheckBoxes[j][k].View == View.shine && MenuMain.Menus[i].CheckBoxes[j][k].Showed)
                            {
                                if (MenuMain.Menus[i].CheckBoxes[j][k].State)
                                    Drawing.OurSprite.Draw2D(Textures.checkBoxes[1], Point.Empty, 0, MenuMain.Menus[i].CheckBoxes[j][k].Rect.Location, Color.White);
                                else
                                    Drawing.OurSprite.Draw2D(Textures.checkBoxes[4], Point.Empty, 0, MenuMain.Menus[i].CheckBoxes[j][k].Rect.Location, Color.White);
                                TextRenderer.DrawText(MenuMain.Menus[i].CheckBoxes[j][k], colorShine);
                            }
                            if (MenuMain.Menus[i].CheckBoxes[j][k].View == View.unable && MenuMain.Menus[i].CheckBoxes[j][k].Showed)
                            {
                                if (MenuMain.Menus[i].CheckBoxes[j][k].State)
                                    Drawing.OurSprite.Draw2D(Textures.checkBoxes[2], Point.Empty, 0, MenuMain.Menus[i].CheckBoxes[j][k].Rect.Location, Color.White);
                                else
                                    Drawing.OurSprite.Draw2D(Textures.checkBoxes[5], Point.Empty, 0, MenuMain.Menus[i].CheckBoxes[j][k].Rect.Location, Color.White);
                                TextRenderer.DrawText(MenuMain.Menus[i].CheckBoxes[j][k], colorUnable);
                            }
                            if (MenuMain.Menus[i].CheckBoxes[j][k].View == View.attention && MenuMain.Menus[i].CheckBoxes[j][k].Showed)
                            {
                                if (MenuMain.Menus[i].CheckBoxes[j][k].State)
                                    Drawing.OurSprite.Draw2D(Textures.checkBoxes[2], Point.Empty, 0, MenuMain.Menus[i].CheckBoxes[j][k].Rect.Location, Color.White);
                                else
                                    Drawing.OurSprite.Draw2D(Textures.checkBoxes[5], Point.Empty, 0, MenuMain.Menus[i].CheckBoxes[j][k].Rect.Location, Color.White);
                                TextRenderer.DrawText(MenuMain.Menus[i].CheckBoxes[j][k], colorAttention);
                            }
                        }
                    }
                    for (int j = 0; j < MenuMain.Menus[i].Labels.Count; j++)
                    {
                        if (MenuMain.Menus[i].Labels[j].View == View.attention && MenuMain.Menus[i].Labels[j].Showed)
                            TextRenderer.DrawText(MenuMain.Menus[i].Labels[j], colorAttention);
                        if (MenuMain.Menus[i].Labels[j].View == View.current && MenuMain.Menus[i].Labels[j].Showed)
                            TextRenderer.DrawText(MenuMain.Menus[i].Labels[j], colorCurrent);
                        if (MenuMain.Menus[i].Labels[j].View == View.shine && MenuMain.Menus[i].Labels[j].Showed)
                            TextRenderer.DrawText(MenuMain.Menus[i].Labels[j], colorShine);
                        if (MenuMain.Menus[i].Labels[j].View == View.unable && MenuMain.Menus[i].Labels[j].Showed)
                            TextRenderer.DrawText(MenuMain.Menus[i].Labels[j], colorUnable);
                    }
                }

            Drawing.OurSprite.End();
        }

    }
}
