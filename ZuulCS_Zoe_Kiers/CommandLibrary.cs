using System;

namespace ZuulCS
{
	public class CommandLibrary
	{
		// an array that holds all valid command words
		private string[] validCommands;
		private string[] commandDescription;

		/**
	     * Constructor - initialise the command words.
	     */
		public CommandLibrary()
		{
			validCommands = new string[] {
				"Go",
				"Quit",
				"Help",
				"Look",
				"Inventory",
				"Take",
				"Drop",
				"Use",
				"Attack",
				"Equip",
				"Unequip"
			};
			commandDescription = new string[]
			{
				"Usage: go 'direction'<br>For directions use the 'look' command.<br>Use 'go back' to go to the previous room.",
				"Saves and quits the game.",
				"The command you just entered",
				"Gives you the description of the room.",
				"Shows you your health and inventory.",
				"Usage: take 'item name'<br>Used to take items from the room into your inventory.",
				"Usage: drop 'item name'<br>Used to drop items from your inventory into the room.",
				"Usage: use 'item name'<br>Usage if it's a key: use 'item name' 'target location'<br>Uses an item on yourself or a key to the targeted door.",
				"Attack with the item you currently have equipped.",
				"Usage: equip 'item name'<br>Equips the targeted item to your equipment slot.",
				"Unequips the currently equipped weapon."
			};
		}

		/**
	     * Check whether a given string is a valid command word.
	     * Return true if it is, false if it isn't.
	     */
		public bool IsCommand(string instring)
		{
			for (int i = 0; i < validCommands.Length; i++)
			{
				if (validCommands[i].ToLower() == instring)
				{
					return true;
				}
			}
			// if we get here, the string was not found in the commands
			return false;
		}

		/**
	     * Print all valid commands to Console.WriteLine.
	     */
		public void ShowAll()
		{
			for (int i = 0; i < validCommands.Length; i++)
			{
				Console.WriteLine("╟─ " + validCommands[i]);
				string[] thisDescription = commandDescription[i].Split(new string[] { "<br>" }, StringSplitOptions.None);
				string output = "";
				for (int i_ = 0; i_ < thisDescription.Length; i_++)
				{
					output += "║   <S>" + thisDescription[i_] + "<S>\n";
				}
				TextEffects.SecondaryColoredMessage(output, "White", "DarkGray");
			}
		}
	}
}
