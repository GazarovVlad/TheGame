using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace UserControl.Menus
{
    public static class Score
    {
        public static List<string> PlayerNames { get; private set; }
        public static List<int> PlayerScores { get; private set; }

        public static string CurrentPlayer { get; set; }
        public static int CurrentScore { get; set; }

        private const int maxNameLength = 20;
        private const int maxScore = 99999999;
        private const string path = "ScoreBoard.txt";
        private static char[] splitterInFile = { ' ' };

        private static bool AddRecord(string playerName, int playerScore)
        {
            if (playerName.Length <= maxNameLength && playerScore <= maxScore)
            {
                PlayerNames.Add(playerName);
                PlayerScores.Add(playerScore);
            }
            else
                return false;
            return true;
        }

        public static void Load()
        {
            StreamReader reader = new StreamReader(path);
            int count = Convert.ToInt32(reader.ReadLine());
            PlayerNames = new List<string>();
            PlayerScores = new List<int>();
            string line;
            string[] lineHalf = new string[2];
            for (int i = 0; i < count; i++)
            {
                line = reader.ReadLine();
                lineHalf = line.Split(splitterInFile, 2);
                AddRecord(lineHalf[0], Convert.ToInt32(lineHalf[1]));
            }
            reader.Close();
        }

        public static void Save()
        {
            StreamWriter writer = new StreamWriter(path);
            writer.WriteLine((PlayerNames.Count + 1).ToString());
            for (int i = 0; i < PlayerNames.Count; i++)
                writer.WriteLine(PlayerNames[i].ToString() + ' ' + PlayerScores[i].ToString());
            writer.WriteLine(CurrentPlayer + ' ' + CurrentScore.ToString());
            writer.Close();
        }
    }
}
