using System;

namespace ZuulCS
{
	public class Parser
	{
		private CommandLibrary commands;  // holds all valid command words

		public Parser()
		{
			commands = new CommandLibrary();
		}

		/**
	     * Ask and interpret the user input. Return a Command object.
	     */
		public Command GetCommand()
		{
			Console.Write("> ");     // print prompt

			string word1 = null;
			string word2 = null;
			string word3 = null;

			string[] words = Console.ReadLine().Split(' ');
			if (words.Length > 0) { word1 = words[0].ToLower(); }
			if (words.Length > 1) { word2 = words[1]; }
			if (words.Length > 2) { word3 = words[2]; }
			if (words.Length > 3)
			{
				for (int i = 2; i <= (words.Length - 2); i++)
				{
					if (i > 1) { word2 += " "; }
					word2 += words[i];
				}
				word3 = words[words.Length - 1];
			}

			// Now check whether this word is known. If so, create a command with it.
			if (commands.IsCommand(word1))
			{
				return new Command(word1, word2, word3);
			}

			// If not, create a "null" command (for unknown command).
			return new Command(null, null, null);
		}

		/**
	     * Print out a list of valid command words.
	     */
		public void ShowCommands()
		{
			commands.ShowAll();
		}
	}
}
