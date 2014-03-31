using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace FisicalObjects
{
	class Loder
	{
		public string Key { get; private set; }
		public string Value { get; private set; }

		private StreamReader StrReader;
		private string EndLine;

		public Loder(string path)
		{
			StrReader = new StreamReader(new FileStream(path, FileMode.Open));
			EndLine = "Nun";
		}

		public Loder(string path, string startLine, string endLine)
		{
			StrReader = new StreamReader(new FileStream(path, FileMode.Open));
			Plase(startLine);
			EndLine = endLine;
		}

		public void EndReading()
		{
			StrReader.Close();
		}

		public bool Next()
		{
			string line = "";
			while (((line = StrReader.ReadLine()) != null) && ((EndLine == "Nun") || (line != EndLine)))
			{
				if (ParseLine(line))
				{
					return true;
				}
			}
			return false;
		}

		private bool ParseLine(string line)
		{
			string[] splited = line.Split('=');
			if ((splited.Length != 2) || (splited[0].Length == 0) || (splited[1].Length == 0))
				return false;
			Key = "";
			for (int i = 0; i < splited[0].Length; i++)
				if ((splited[0][i] != ' ') && (splited[0][i] != '\t'))
					Key += splited[0][i];
			Value = "";
			for (int i = 0; i < splited[1].Length; i++)
				if ((splited[1][i] != ' ') && (splited[1][i] != '\t'))
					Value += splited[1][i];
			return true;
		}

		private void Plase(string spcLine)
		{
			string line = "";
			while ((line = StrReader.ReadLine()) != null)
			{
				if (line == spcLine)
					break;
			}
		}
	}
}
