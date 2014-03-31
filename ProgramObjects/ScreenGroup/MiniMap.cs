using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using ProgramObjects.InputDevices;

namespace ProgramObjects.ScreenGroup
{
    public static class MiniMap
    {
        public static int DX { get; private set; }  //  Координаты на окне или экране
        public static int DY { get; private set; }
        public static double MiniScreenX { get; private set; }
        public static double MiniScreenY { get; private set; }
        public static double MiniScreenHeight { get; private set; }
        public static double MiniScreenWidth { get; private set; }

        public const int Size = 150;

        public static void InitializeMiniMap()
        {
            DX = Screen.Width - WorkSpace.Frame - Size;
            DY = Screen.Height - WorkSpace.Frame - Size;
        }

        public static void InitializeMiniScreen()
        {
            MiniScreenHeight = (int)((Size * WorkSpace.Space.Height * 1.0) / WorkSpace.MapLen);
            MiniScreenWidth = (int)((Size * WorkSpace.Space.Width * 1.0) / WorkSpace.MapLen);
            MiniScreenMove();
        }

        public static void MiniScreenMove()
        {
            MiniScreenX = (Size * WorkSpace.Space.X * 1.0) / WorkSpace.MapLen;
            MiniScreenY = (Size * WorkSpace.Space.Y * 1.0) / WorkSpace.MapLen;
            if (WorkSpace.Space.X == 0) MiniScreenX = 0;
            if (WorkSpace.Space.Y == 0) MiniScreenY = 0;
            if (WorkSpace.Space.X == WorkSpace.MapLen - WorkSpace.Space.Width) MiniScreenX = Size - (int)MiniScreenWidth;
            if (WorkSpace.Space.Y == WorkSpace.MapLen - WorkSpace.Space.Height) MiniScreenY = Size - (int)MiniScreenHeight;
        }

        public static Point GetMovedPozition()
        {
            Point poz = new Point(WorkSpace.Space.X, WorkSpace.Space.Y);
            int mPX = Mouse.PresedDX;
            int mPY = Mouse.PresedDY;
            int iX = (int)(MiniScreenWidth / 2);
            int iY = (int)(MiniScreenHeight / 2);
            if ((mPX >= DX + iX) && (mPX <= DX + Size - iX) && (mPY >= DY + iY) && (mPY <= DY + Size - iY)) //  удачное попадание в центр
            {// mPX - X - iX    --  левая верхняя точка мини экрана на плоскости миникарты в которую идет перемещение
                poz.X = (int)(((mPX - DX - iX) * WorkSpace.MapLen * 1.0) / Size);
                poz.Y = (int)(((mPY - DY - iY) * WorkSpace.MapLen * 1.0) / Size);
            }
            else if (mPX >= DX + Size - iX)   //  попадание в правый край
            {
                poz.X = WorkSpace.MapLen - WorkSpace.Space.Width;
                if ((mPY >= DY + iY) && (mPY <= DY + Size - iY))    //  по центру
                {
                    poz.Y = (int)(((mPY - DY - iY) * WorkSpace.MapLen * 1.0) / Size);
                }
                else if (mPY < DY + iY)  // с верху
                {
                    poz.Y = 0;
                }
                else if (mPY > DY - iY)  //  с низу
                {
                    poz.Y = WorkSpace.MapLen - WorkSpace.Space.Height;
                }
            }
            else if (mPX <= DX + iX)  //  попадание в левый край
            {
                poz.X = 0;
                if ((mPY >= DY + iY) && (mPY <= DY + Size - iY))  //  по центру
                {
                    poz.Y = (int)(((mPY - DY - iY) * WorkSpace.MapLen * 1.0) / Size);
                }
                else if (mPY < DY + iY)   // с верху
                {
                    poz.Y = 0;
                }
                else if (mPY > DY - iY)  //  с низу
                {
                    poz.Y = WorkSpace.MapLen - WorkSpace.Space.Height;
                }
            }
            else if (mPY <= DY + iY )   //  попадание в верхний край по центру
            {
                poz.X = (int)(((mPX - DX - iX) * WorkSpace.MapLen * 1.0) / Size);
                poz.Y = 0;
            }
            else   //  попадание в нижний край по центру
            {
                poz.X = (int)(((mPX - DX - iX) * WorkSpace.MapLen * 1.0) / Size);
                poz.Y = WorkSpace.MapLen - WorkSpace.Space.Height;
            }
            return poz;
        }
    }
}
