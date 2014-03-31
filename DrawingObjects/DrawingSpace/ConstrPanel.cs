using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Microsoft.DirectX.Direct3D;
using ProgramObjects.InputDevices;
using ProgramObjects.ScreenGroup;
using UserControl.Interface;
using TheGameDrawing.TextureSpace;
using FisicalObjects;
using FisicalObjects.Transist;

namespace TheGameDrawing.DrawingSpace
{
    static class ConstrPanel
    {
        public static void DrawBuildingIcons()
        {
            Drawing.OurSprite.Begin(SpriteFlags.AlphaBlend);
            for (int i = 0; i < ConstrPanelControl.ConstrInfo.Length; i++)
            {
                Drawing.OurSprite.Draw2D(Textures.iconBorder, Point.Empty, 0, ConstrPanelControl.Slot[i], Color.White);
                if (AIUnits.MoneyCheck(i))
                    Drawing.OurSprite.Draw2D(Textures.constrIcons[ConstrPanelControl.ConstrInfo[i].IconIndex], Point.Empty, 0, ConstrPanelControl.Slot[i], Color.White);
                else
                    Drawing.OurSprite.Draw2D(Textures.constrIconsShine[ConstrPanelControl.ConstrInfo[i].IconIndex], Point.Empty, 0, ConstrPanelControl.Slot[i], Color.White);
            }
            Drawing.OurSprite.End();
        }

        public static void DrawConstr()
        {
            if (ConstrPanelControl.ConstrSelected)
            {
                Drawing.OurSprite.Begin(SpriteFlags.AlphaBlend);
                if ((WorkSpace.DX <= Mouse.DX) && (WorkSpace.DY <= Mouse.DY) && (WorkSpace.DX + WorkSpace.Space.Width >= Mouse.DX) && (WorkSpace.DY + WorkSpace.Space.Height >= Mouse.DY))
                {
                    if (AIUnits.CanPlace(ConstrPanelControl.SlotType, WorkSpace.Space.X + Mouse.DX - WorkSpace.DX, WorkSpace.Space.Y + Mouse.DY - WorkSpace.DY))
                        Drawing.OurSprite.Draw2D(Textures.constrIcons[ConstrPanelControl.ConstrInfo[ConstrPanelControl.SlotType].IconIndex], Point.Empty, 0, new Point(Mouse.DX - ConstrPanelControl.SlotSize / 2, Mouse.DY - ConstrPanelControl.SlotSize / 2), Color.White);
                    else
                        Drawing.OurSprite.Draw2D(Textures.constrIconsShine[ConstrPanelControl.ConstrInfo[ConstrPanelControl.SlotType].IconIndex], Point.Empty, 0, new Point(Mouse.DX - ConstrPanelControl.SlotSize / 2, Mouse.DY - ConstrPanelControl.SlotSize / 2), Color.White);
                }
                else
                    Drawing.OurSprite.Draw2D(Textures.constrIconsShine[ConstrPanelControl.ConstrInfo[ConstrPanelControl.SlotType].IconIndex], Point.Empty, 0, new Point(Mouse.DX - ConstrPanelControl.SlotSize / 2, Mouse.DY - ConstrPanelControl.SlotSize / 2), Color.White);
                if (ConstrPanelControl.ConstrInfo[ConstrPanelControl.SlotType].FireRang != 0)
                {
                    SurfaceDescription temp = Textures.radAttack.Get(ConstrPanelControl.ConstrInfo[ConstrPanelControl.SlotType].FireRang.ToString()).GetLevelDescription(0);
                    Point poz = new Point(Mouse.DX - temp.Width / 2, Mouse.DY - temp.Height / 2);
                    Drawing.OurSprite.Draw2D(Textures.radAttack.Get(ConstrPanelControl.ConstrInfo[ConstrPanelControl.SlotType].FireRang.ToString()), Point.Empty, 0, poz, Color.White);
                }
                Drawing.OurSprite.Draw2D(Textures.radVisibl, Point.Empty, 0, new Point(Mouse.DX - ConstrPanelControl.TextureRadVisiblSize / 2, Mouse.DY - ConstrPanelControl.TextureRadVisiblSize / 2), Color.White);
                Drawing.OurSprite.End();
            }
        }

        public static void DrawFutureWays()
        {
            if ((ConstrPanelControl.ConstrSelected) && (WorkSpace.DX < Mouse.DX) && (WorkSpace.RightBorder.X > Mouse.DX) && (WorkSpace.DY < Mouse.DY) && (WorkSpace.DownBorder.Y > Mouse.DY) && (AIUnits.CanPlace(ConstrPanelControl.SlotType, WorkSpace.Space.X + Mouse.DX - WorkSpace.DX, WorkSpace.Space.Y + Mouse.DY - WorkSpace.DY)))
            {
                List<TransWay> ways = AIUnits.GetFutureWays(ConstrPanelControl.SlotType, WorkSpace.Space.X + Mouse.DX - WorkSpace.LeftBorder.Width, WorkSpace.Space.Y + Mouse.DY - WorkSpace.UpBorder.Height);
                CustomVertex.TransformedColored[] lines = new CustomVertex.TransformedColored[ways.Count * 2];
                for (int i = 0; i < ways.Count; i++)
                {
                    lines[2 * i].Position = new Microsoft.DirectX.Vector4(ways[i].A.X - WorkSpace.Space.X + WorkSpace.DX, ways[i].A.Y - WorkSpace.Space.Y + WorkSpace.DY, 1, 1);
                    lines[2 * i].Color = ways[i].Coloring.ToArgb();
                    lines[2 * i + 1].Position = new Microsoft.DirectX.Vector4(ways[i].B.X - WorkSpace.Space.X + WorkSpace.DX, ways[i].B.Y - WorkSpace.Space.Y + WorkSpace.DY, 1, 1);
                    lines[2 * i + 1].Color = ways[i].Coloring.ToArgb();
                }
                Drawing.OurSprite.Begin(SpriteFlags.AlphaBlend);
                Drawing.OurDevice.VertexFormat = CustomVertex.TransformedColored.Format;
                if (lines.Length > 0)
                    Drawing.OurDevice.DrawUserPrimitives(PrimitiveType.LineList, lines.Length / 2, lines);
                Drawing.OurSprite.End();
            }
        }
    }
}
