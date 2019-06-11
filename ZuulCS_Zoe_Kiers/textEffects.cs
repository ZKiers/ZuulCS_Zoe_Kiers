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
		public static void ErrorMessage(String input)
		{
			Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.WriteLine(input);
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
