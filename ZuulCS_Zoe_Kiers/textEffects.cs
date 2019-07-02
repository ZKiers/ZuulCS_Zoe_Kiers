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
			if (input != null) { Console.WriteLine(input); }
			Console.ResetColor();
		}
		public static void CheckNullWriteLine(String input)
		{
			if (input != null)
			{
				Console.WriteLine(input);
			}
		}
	}
}
