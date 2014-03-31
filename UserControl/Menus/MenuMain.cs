using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using ProgramObjects.ScreenGroup;
using UserControl.Menus.Elements;
using GraphicObjects.StarsSpace;
using ProgramObjects.Settings;
using Microsoft.DirectX.Direct3D;
using SupportedStructures;

namespace UserControl.Menus
{
    public class MenuMain
    {
        public static bool Showed = true;
        public static bool ShowedBackGround = true;
        public static Point Position { get; private set; }

        public const int TextureMenuWidth = 325;
        public const int TextureMenuHeight = 450;
        public const int TextureBackGroundWidth = 1280;
		public const int TextureBackGroundHeight = 1024;
		public const int Border = 35;

        public static bool checkBoxSetChanged = false;
        public static bool restartApp = false;
        public static bool closeApp = false;

        public static MenuItem[] Menus { get; private set; }

        public static void Initialize()
        {
            Position = new Point((Screen.Width - TextureMenuWidth) / 2, (Screen.Height - TextureMenuHeight) / 2);
            Menus = new MenuItem[5];
            Menus[0] = new MenuItem();//Game
            Menus[1] = new MenuItem();//Main
            Menus[2] = new MenuItem();//ScoreBoard
            Menus[3] = new MenuItem();//Settings
            Menus[4] = new MenuItem();//EndGame

            int w = 200, h = 15, s = 20, H = 20, o = 400;
            int centerXonW = MenuMain.Position.X + (MenuMain.TextureMenuWidth - w) / 2;
            int centerX = MenuMain.Position.X + (MenuMain.TextureMenuWidth) / 2;

            string butName = "НАЗАД";
            Rectangle rect = new Rectangle(centerXonW, MenuMain.Position.Y + o, w, h);
            Button back = new Button(butName, rect, true, true, OurFonts.settingMenu);

            int y = MenuMain.Position.Y + h;
            rect = new Rectangle(centerXonW, y, w, h);
            Label menuName = new Label("menuName", rect, true, true, OurFonts.settingMenu);

            {   //Game - 0
                string[] butNames = { "ПРОДОЛЖИТЬ", "НАСТРОЙКИ", "ВЫХОД ИЗ ИГРЫ", "ВЫХОД В WINDOWS" };
                int count = butNames.Length;
                List<Button> ButtonsGM = new List<Button>();
                Rectangle[] rects = new Rectangle[count];
                int x = MenuMain.Position.X + (MenuMain.TextureMenuWidth - w) / 2;
                int incY = (MenuMain.TextureMenuHeight - count * H) / (count + 1);
                rects[0] = new Rectangle(x, MenuMain.Position.Y + incY, w, H);
                for (int i = 1; i < count; i++)
                    rects[i] = new Rectangle(x, rects[i - 1].Y + H + incY, w, H);
                for (int i = 0; i < count; i++)
                    ButtonsGM.Add(new Button(butNames[i], rects[i], true, true, OurFonts.mainMenu));
                Menus[0].Buttons.AddRange(ButtonsGM);
            }

            {   //Main - 1
                string[] butNames = { "НОВАЯ ИГРА", "НАСТРОЙКИ", "ТАБЛИЦА РЕКОРДОВ", "ВЫХОД В WINDOWS" };
                int count = butNames.Length;
                List<Button> ButtonsMM = new List<Button>();
                Rectangle[] rects = new Rectangle[count];
                int x = MenuMain.Position.X + (MenuMain.TextureMenuWidth - w) / 2;
                int incY = (MenuMain.TextureMenuHeight - count * H) / (count + 1);
                rects[0] = new Rectangle(x, MenuMain.Position.Y + incY, w, H);
                for (int i = 1; i < count; i++)
                    rects[i] = new Rectangle(x, rects[i - 1].Y + H + incY, w, H);
                for (int i = 0; i < count; i++)
                    ButtonsMM.Add(new Button(butNames[i], rects[i], true, true, OurFonts.mainMenu));
                Menus[1].Buttons.AddRange(ButtonsMM);
                Menus[1].SetState(true);
            }

            {   //ScoreBoard - 2
                Score.Load();

                menuName.Name = "Рекорды";
                Menus[2].Labels.Add(menuName.GetCopy());

                rect = new Rectangle(MenuMain.Position.X + s, y + H, w, s);
                Rectangle rect2 = new Rectangle(centerX + (int)(0.3 * w), y + H, 4 * s, s);
                Label player = new Label(Score.PlayerNames[0], rect, true, true, OurFonts.mainMenu);
                Label score = new Label(Score.PlayerScores[0].ToString(), rect2, true, true, OurFonts.mainMenu);
                Menus[2].Labels.Add(player.GetCopy());
                Menus[2].Labels.Add(score.GetCopy());
                for (int i = 1; i < Score.PlayerNames.Count; i++)
                {
                    rect.Y += s;
                    rect2.Y += s;
                    player.Rect = rect;
                    score.Rect = rect2;
                    player.Name = Score.PlayerNames[i];
                    score.Name = Score.PlayerScores[i].ToString();
                    Menus[2].Labels.Add(player.GetCopy());
                    Menus[2].Labels.Add(score.GetCopy());
                }

                Menus[2].Buttons.Add(back.GetCopy());
            }

            {   //Settings - 3
                string[] lblNames = { "Колличество звезд", "Разрешение экрана", "Режим" };
                string[] chkBNames = { "мало  ", "средне", "много ", "800x600", "1024x768", "1280x800", "1600x1200", "оконный", "полноэкранный" };

                menuName.Name = "Настройки игры";
                Menus[3].Labels.Add(menuName.GetCopy());

                y = MenuMain.Position.Y + h;
                rect = new Rectangle(centerXonW, y, w, h);
                y += 2 * s;
                rect = new Rectangle(centerXonW, y, w, h);
                Menus[3].Labels.Add(new Label(lblNames[0], rect, true, true, OurFonts.settingMenu));

                Rectangle[] rects = new Rectangle[3];
                int incX = 80;
                y += s;
                int sumX = MenuMain.Position.X + 40;
                for (int i = 0; i < rects.Length; i++)
                {
                    rects[i] = new Rectangle(sumX, y, incX, h);
                    sumX += incX + 10;
                }
                int AminL = 15, AminM = 30, AminH = 60;
                List<CheckBox> c = new List<CheckBox>();
                c.Add(new CheckBox(chkBNames[0], rects[0], true, Stars.MinCount[0] == AminL, true, OurFonts.settingMenu));
                c.Add(new CheckBox(chkBNames[1], rects[1], true, Stars.MinCount[0] == AminM, true, OurFonts.settingMenu));
                c.Add(new CheckBox(chkBNames[2], rects[2], true, Stars.MinCount[0] == AminH, true, OurFonts.settingMenu));
                Menus[3].CheckBoxes.Add(c);

                y += (int)(1.5 * s);
                rect = new Rectangle(centerXonW, y, w, h);
                Menus[3].Labels.Add(new Label(lblNames[1], rect, true, true, OurFonts.settingMenu));

                rects = new Rectangle[4];
                incX = 90;
                y += s;
                sumX = MenuMain.Position.X + 80;
                for (int i = 0; i < rects.Length; i++)
                {
                    if (i == 2)
                    {
                        y += (int)(1.2 * s); ;
                        sumX = MenuMain.Position.X + 80;
                    }
                    rects[i] = new Rectangle(sumX, y, incX, 15);
                    sumX += incX + 10;
                }
                bool[] modesAllow = CheckAllowDisplayMode();
                string str = Screen.ResolutionW.ToString() + 'x' + Screen.ResolutionH.ToString();
                List<CheckBox> c2 = new List<CheckBox>();
                c2.Add(new CheckBox(chkBNames[3], rects[0], true, chkBNames[3] == str, modesAllow[0], OurFonts.settingMenu));
                c2.Add(new CheckBox(chkBNames[4], rects[1], true, chkBNames[4] == str, modesAllow[1], OurFonts.settingMenu));
                c2.Add(new CheckBox(chkBNames[5], rects[2], true, chkBNames[5] == str, modesAllow[2], OurFonts.settingMenu));
                c2.Add(new CheckBox(chkBNames[6], rects[3], true, chkBNames[6] == str, modesAllow[3], OurFonts.settingMenu));
                Menus[3].CheckBoxes.Add(c2);

                y += (int)(1.5 * s); ;
                rect = new Rectangle(centerXonW, y, w, h);
                Menus[3].Labels.Add(new Label(lblNames[2], rect, true, true, OurFonts.settingMenu));

                rects = new Rectangle[2];
                incX = 100;
                y += s;
                sumX = MenuMain.Position.X + 40;
                rects[0] = new Rectangle(sumX, y, 80, h);
                sumX += incX + 10;
                rects[1] = new Rectangle(sumX, y, 140, h);
                List<CheckBox> c3 = new List<CheckBox>();
                c3.Add(new CheckBox(chkBNames[7], rects[0], true, ScreenMode.windowed == Screen.ScreenType, true, OurFonts.settingMenu));
                c3.Add(new CheckBox(chkBNames[8], rects[1], true, ScreenMode.fullscreen == Screen.ScreenType, true, OurFonts.settingMenu));
                Menus[3].CheckBoxes.Add(c3);

                rect = new Rectangle(centerX - 4 * s, MenuMain.Position.Y + o - s, 3 * s - h, h);
                back.Rect = rect;
                Menus[3].Buttons.Add(back.GetCopy());

                butName = "ПРИМЕНИТЬ";
                rect = new Rectangle(centerX + s, MenuMain.Position.Y + o - s, 4 * s + 5, h);
                Button accept = new Button(butName, rect, true, false, OurFonts.settingMenu);
                Menus[3].Buttons.Add(accept);

                string lblName = "ПЕРЕЗАПУСК...";
                rect = new Rectangle(centerXonW, MenuMain.Position.Y + o + (int)(0.3 * s), w, h);
                Label restart = new Label(lblName, rect, false, View.attention, OurFonts.settingMenu);
                Menus[3].Labels.Add(restart);

                lblName = "Изменения будут применены при следующем запуске игры";
                rect = new Rectangle(MenuMain.Position.X + h, MenuMain.Position.Y + o - (int)(0.2 * s), w + 5 * s, h);
                Label hint = new Label(lblName, rect, false, true, OurFonts.smallFont);
                Menus[3].Labels.Add(hint);

                lblName = "Применить и перезапустить сейчас";
                rect = new Rectangle(MenuMain.Position.X + (int)(0.9 * h), MenuMain.Position.Y + o + (int)(0.2 * s), w, h);
                Label butHint = new Label(lblName, rect, false, true, OurFonts.smallFont);
                Menus[3].Buttons[1].Hint = butHint;
            }

            {   //EndGame - 4
                string lblName = "Ваш счет: " + Score.CurrentScore.ToString();
				rect = new Rectangle(Position.X + Border, Position.Y + Border + (int)(TextureMenuHeight * 0.25), TextureMenuWidth - 2 * Border, s);
                Label end = new Label(lblName, rect, true, true, OurFonts.mainMenu);
                Menus[4].Labels.Add(end.GetCopy());

                end.Name = "Ваше имя:";
				rect = new Rectangle(Position.X + Border, Position.Y + Border + (int)(TextureMenuHeight * 0.5), 90, s);
                end.Rect = rect;
                Menus[4].Labels.Add(end.GetCopy());

                lblName = "";
				rect = new Rectangle(rect.X + rect.Width, Position.Y + Border + (int)(TextureMenuHeight * 0.5), TextureMenuWidth - 2 * Border - rect.Width, s);
                Label playerName = new Label(lblName, rect, true, true, OurFonts.mainMenu);
                Menus[4].Labels.Add(playerName.GetCopy());
                //// end.Name = "Осталось:"
            }

            CleanPressedElements();
        }

        public static void CheckCurrentSett()
        {
            int AminL = 15, AminM = 30, AminH = 60;
            Menus[3].CheckBoxes[0][0].State = Stars.MinCount[0] == AminL;
            Menus[3].CheckBoxes[0][1].State = Stars.MinCount[0] == AminM;
            Menus[3].CheckBoxes[0][2].State = Stars.MinCount[0] == AminH;

            string[] posRes = { "800x600", "1024x768", "1280x800", "1600x1200"};
            string str = Screen.ResolutionW.ToString() + 'x' + Screen.ResolutionH.ToString();
            Menus[3].CheckBoxes[1][0].State = posRes[0] == str;
            Menus[3].CheckBoxes[1][1].State = posRes[1] == str;
            Menus[3].CheckBoxes[1][2].State = posRes[2] == str;
            Menus[3].CheckBoxes[1][3].State = posRes[3] == str;

            Menus[3].CheckBoxes[2][0].State = ScreenMode.windowed == Screen.ScreenType;
            Menus[3].CheckBoxes[2][1].State = ScreenMode.fullscreen == Screen.ScreenType;

            Menus[3].Labels[5].Showed = false;
            checkBoxSetChanged = false;
        }

        public static void MousePositionControl()
        {
            bool changed;
            for (int i = 0; i < Menus.Length; i++)
                if (Menus[i].Showed)
                {
                    changed = Menus[i].MousePositionControl();
                    if (i == 3 && changed)//Settings
                        checkBoxSetChanged = true;
                }
        }

        public static void CleanPressedElements()
        {
            foreach (MenuItem m in Menus)
                m.CleanPressedElements();
        }

        public static void ActionsInResultOfChangingSettings()
        {
            checkBoxSetChanged = false;

            string setLitMin = "15 100 250", setLitMax = "25 200 400";
            string setMedMin = "30 200 500", setMedMax = "50 400 900";
            string setHavMin = "60 400 1000", setHavMax = "100 800 1800";
            string resMin = "", resMax = "";
            if (Menus[3].CheckBoxes[0][0].State)
            {
                resMin = setLitMin;
                resMax = setLitMax;
            }
            if (Menus[3].CheckBoxes[0][1].State)
            {
                resMin = setMedMin;
                resMax = setMedMax;
            }
            if (Menus[3].CheckBoxes[0][2].State)
            {
                resMin = setHavMin;
                resMax = setHavMax;
            }
            StarsSett.SaveWithChanges(resMin, resMax);

            int w = Screen.ResolutionW;
            int h = Screen.ResolutionH;
            ScreenMode scrM = Screen.ScreenType;
            if (Menus[3].CheckBoxes[1][0].State)
            {
                w = 800;
                h = 600;
            }
            if (Menus[3].CheckBoxes[1][1].State)
            {
                w = 1024;
                h = 768;
            }
            if (Menus[3].CheckBoxes[1][2].State)
            {
                w = 1280;
                h = 800;
            }
            if (Menus[3].CheckBoxes[1][3].State)
            {
                w = 1600;
                h = 1200;
            }
            if (Menus[3].CheckBoxes[2][0].State)
                scrM = ScreenMode.windowed;
            if (Menus[3].CheckBoxes[2][1].State)
                scrM = ScreenMode.fullscreen;
            ScreenSett.SaveWithChanges(w, h, scrM);
        }

        public static void DisableSett()
        {
            foreach (Button b in Menus[3].Buttons)
                b.EnableSet(false);
            foreach (List<CheckBox> lc in Menus[3].CheckBoxes)
                foreach (CheckBox c in lc)
                    c.EnableSet(false);
            Menus[3].Labels[4].Showed = true;
            Menus[3].Labels[5].Showed = false;
        }

        private static bool[] CheckAllowDisplayMode()
        {
            bool[] modesAllow = { false, false, false, false };
            DisplayModeCollection displayModes = Manager.Adapters.Default.SupportedDisplayModes;
            foreach (DisplayMode dm in displayModes)
            {
                if (dm.Width == 800 && dm.Height == 600)
                    modesAllow[0] = true;
                if (dm.Width == 1024 && dm.Height == 768)
                    modesAllow[1] = true;
                if (dm.Width == 1280 && dm.Height == 800)
                    modesAllow[2] = true;
                if (dm.Width == 1600 && dm.Height == 1200)
                    modesAllow[3] = true;
            }
            return modesAllow;
        }

    }
}
