using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZuulCS
{
	public static class TextEffects
	{
		//this is a library for	printing out multiple types of text effects
		public static void ColoredMessage(string input, string color)
		{
			switch (color)
			{
				case "Black":
					Console.ForegroundColor = ConsoleColor.Black;
					break;
				case "Blue":
					Console.ForegroundColor = ConsoleColor.Blue;
					break;
				case "Cyan":
					Console.ForegroundColor = ConsoleColor.Cyan;
					break;
				case "DarkBlue":
					Console.ForegroundColor = ConsoleColor.DarkBlue;
					break;
				case "DarkCyan":
					Console.ForegroundColor = ConsoleColor.DarkCyan;
					break;
				case "DarkGray":
					Console.ForegroundColor = ConsoleColor.DarkGray;
					break;
				case "DarkGreen":
					Console.ForegroundColor = ConsoleColor.DarkGreen;
					break;
				case "DarkMagenta":
					Console.ForegroundColor = ConsoleColor.DarkMagenta;
					break;
				case "DarkRed":
					Console.ForegroundColor = ConsoleColor.DarkRed;
					break;
				case "DarkYellow":
					Console.ForegroundColor = ConsoleColor.DarkYellow;
					break;
				case "Gray":
					Console.ForegroundColor = ConsoleColor.Gray;
					break;
				case "Green":
					Console.ForegroundColor = ConsoleColor.Green;
					break;
				case "Magenta":
					Console.ForegroundColor = ConsoleColor.Magenta;
					break;
				case "Red":
					Console.ForegroundColor = ConsoleColor.Red;
					break;
				case "White":
					Console.ForegroundColor = ConsoleColor.White;
					break;
				case "Yellow":
					Console.ForegroundColor = ConsoleColor.Yellow;
					break;
			}
			if (input != null) { Console.Write(input); }
			Console.ResetColor();
		}
		public static void SecondaryColoredMessage(string input, string color, string secondColor)
		{
			string[] split = input.Split(new string[] { "<S>" }, StringSplitOptions.None);
			for (int i = 0; i < split.Length; i++)
			{
				if (i % 2 == 0)
				{
					ColoredMessage(split[i], color);
				} else
				{
					ColoredMessage(split[i], secondColor);
				}
			}
		}
		public static void CheckNullWriteLine(String input)
		{
			if (input != null)
			{
				Console.WriteLine(input);
			}
		}
		public static string GenerateLine(string input, int length)
		{
			string output = "";
			for (int i = 0; i < length; i++)
			{
				output += input;
			}
			return output;
		}
        public static string GenerateLineText(string input, string text, int length)
        {
            string output = "";
            int halfLength = (length - text.Length) / 2;
            output += GenerateLine(input, halfLength);
            output += text;
            output += GenerateLine(input, halfLength);
            if (((halfLength * 2) + text.Length) % 2 == 0) { output += input; }
            return output;
        }
		public static string GenerateProgressBar(int width, int maxAmount, int currentAmount)
		{
			String empty = "░";
			String full = "█";
			String output = "";
			float filledAmount = (float)width / (((float)maxAmount / ((float)currentAmount / 100)) / 100);
			output += GenerateLine(full, (int)filledAmount);
			output += GenerateLine(empty, width - (int)filledAmount);
			return output;
		}
		public static string FinishLine(int length, string input, string filler)
		{
			string output = "";
			output += input;
			for (int i = input.Length; i < length; i++)
			{
				output += filler;
			}
			return output;
		}
	}
}
