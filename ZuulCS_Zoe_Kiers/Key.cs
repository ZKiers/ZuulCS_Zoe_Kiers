using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZuulCS
{
	public class Key : Item
	{
		private String key;
		public Key(String name, double weight)
		{
			this.Name = name;
			this.Weight = weight;
		}
		public Key() { }
		public string GenerateKey()
		{
			System.Threading.Thread.Sleep(20);
			Random randomValue = new Random();
			String keyComponents = "1234567890qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";
			String output = "";
			for (int i = 0; i < 8; i++)
			{
				output += keyComponents[randomValue.Next(keyComponents.Length)];
			}
			key = output;
			return output;
		}
		public string GetKey()
		{
			return key;
		}
		public override string UseItem(Command command, Player player)
		{
			Room currentRoom = player.CurrentRoom;
			string input = command.ThirdWord;
			if(!command.HasThirdWord() || command.ThirdWord.ToLower() == "key")
			{
				TextEffects.ColoredMessage("Use it on what?\n", "DarkRed");
				return null;
			}
			Room target = currentRoom.GetExit(input);
			if (target == null)
			{
				TextEffects.ColoredMessage("There is no door there to unlock!\n", "DarkRed");
				return null;
			} else if (!target.IsLocked())
			{
				TextEffects.ColoredMessage("That door isn't locked!\n", "DarkRed");
				return null;
			} else if (!target.UseKey(this))
			{
				TextEffects.ColoredMessage("You fiddle with the key in the lock but you can't seem to unlock it.\n", "DarkRed");
				return null;
			}
			this.Consumed = true;
			return "You put the key in the lock and turn it.\nThe lock opens and the key disappears.\n";
		}
	}
}
