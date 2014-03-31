using System;
using System.Collections.Generic;
using System.Text;
using ProgramObjects.InputDevices;
using SupportedStructures;
using UserControl.Menus.Elements;
using FisicalObjects;
using ProgramObjects.InputDevices.Keyboard;

namespace UserControl.Menus
{
    public static class MouseControl
    {
        private static bool gameStarted = false;
        private static bool gameSaved = false;
        private static bool fromConfirmExitGame = false;
        private static bool fromConfirmExitWindows = false;
        private static bool fromConfirmSave = false;

        public static void Process()
        {
            if (MenuMain.Showed)
            {
                #region 0 - Game
                if (MenuMain.Menus[0].Showed)//Game - 0
                {
                    if (MenuMain.Menus[0].Buttons[0].Pressed)//'Продолжить'
                    {
                        MenuMain.Showed = false;
                        gameSaved = false;
                    }
                    if (MenuMain.Menus[0].Buttons[1].Pressed)//'Настройки'
                    {
                        MenuMain.Menus[0].SetState(false);
                        MenuMain.CheckCurrentSett();
                        MenuMain.Menus[3].SetState(true);
                    }

                    if (MenuMain.Menus[0].Buttons[2].Pressed)//'Выход из игры'
                    {
                        //BeginNewGame();
                        //MenuMain.Menus[0].SetState(false);
                        //PreviousMenu();
                        EndGame();
                    }
                    if (MenuMain.Menus[0].Buttons[3].Pressed)//'Выход в Windows'
                    {
                        MenuMain.closeApp = true;
                    }

                }
                #endregion

                #region 1 - Main
                if (MenuMain.Menus[1].Showed)//Main - 1
                {
                    if (MenuMain.Menus[1].Buttons[0].Pressed)//'Новая игра'
                    {
                        MenuMain.Showed = false;
                        MenuMain.ShowedBackGround = false;
                        MenuMain.Menus[1].SetState(false);
                        MenuMain.Menus[0].SetState(true);
                        gameStarted = true;
                        gameSaved = false;
                        if (AIUnits.WorldExist)
                            AIUnits.DestroyWorld();
                        AIUnits.CreateWorld();
                    }
                    if (MenuMain.Menus[1].Buttons[1].Pressed)//'Настройки'
                    {
                        MenuMain.Menus[1].SetState(false);
                        MenuMain.CheckCurrentSett();
                        MenuMain.Menus[3].SetState(true);
                    }
                    if (MenuMain.Menus[1].Buttons[2].Pressed)//'Таблица рекордов'
                    {
                        MenuMain.Menus[1].SetState(false);
                        MenuMain.Menus[2].SetState(true);
                    }
                    if (MenuMain.Menus[1].Buttons[3].Pressed)//'Выход в Windows'
                    {
                        MenuMain.closeApp = true;
                    }
                }
                #endregion

                #region 2 - ScoreBoard
                if (MenuMain.Menus[2].Showed)//ScoreBoard - 2
                {
                    if (MenuMain.Menus[2].Buttons[0].Pressed)//'Назад'
                    {
                        MenuMain.Menus[2].SetState(false);
                        PreviousMenu();
                    }
                }
                #endregion

                #region 3 - Settings
                if (MenuMain.Menus[3].Showed)//Settings - 3
                {
                    if (MenuMain.Menus[3].Buttons[0].Pressed)//'Назад'
                    {
                        MenuMain.Menus[3].SetState(false);
                        PreviousMenu();
                    }
                    //если изменены настройки
                    if (MenuMain.checkBoxSetChanged)
                    {
                        MenuMain.Menus[3].Labels[5].Showed = true;
                        MenuMain.Menus[3].Buttons[1].EnableSet(true);
                    }
                    if (MenuMain.Menus[3].Buttons[1].Pressed)//'Применить'
                    {
                        MenuMain.ActionsInResultOfChangingSettings();
                        MenuMain.DisableSett();
                        MenuMain.restartApp = true;
                    }

                }
                #endregion

                #region 4 - EndGame
                if (MenuMain.Menus[4].Showed)//EndGame - 4
                {
                    if (OtherKeys.Flag)
                    {
                        Score.CurrentPlayer += OtherKeys.CurrentPressedKey;
                        MenuMain.Menus[4].Labels[2].Name = Score.CurrentPlayer;
                        OtherKeys.Pick();
                    }

                    if (OtherKeys.EnterWasPressed)
                    {
                        MenuMain.Menus[4].SetState(false);
                        Score.Save();
                        Score.Load();
                        MenuMain.Menus[1].SetState(true);
                    }
                }
                #endregion

                MenuMain.CleanPressedElements();
            }
        }

        private static void PreviousMenu()
        {
            if (gameStarted)
                MenuMain.Menus[0].SetState(true);
            else
                MenuMain.Menus[1].SetState(true);
        }
        private static void BeginNewGame()
        {
            //начать новую игру
            MenuMain.ShowedBackGround = true;
            gameSaved = false;
            gameStarted = false;
            if (AIUnits.WorldExist)
                AIUnits.DestroyWorld();
            AIUnits.CreateWorld();
        }

        private static void EndGame()
        {
            MenuMain.Menus[0].SetState(false);
            OtherKeys.Pick();
            MenuMain.Menus[4].SetState(true);
			Score.CurrentScore = (int)AIUnits.Score;
			MenuMain.Menus[4].Labels[0].Name = "Ваш счет: " + Score.CurrentScore.ToString();
        }


    }
}
