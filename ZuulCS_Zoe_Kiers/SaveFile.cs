using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ZuulCS
{
	public static class SaveFile
	{
		private static JavaScriptSerializer serializer = new JavaScriptSerializer();
		public static string saveFileName { get; } = "Save.JSON";
		public static void GenerateSaveFile(Player player)
		{
			string output = serializer.Serialize(player);
			for (int i = 0; i <= (player.inventory.contents.Count - 1); i++)
			{
				output += "\n";
				if (player.inventory.contents[i] is Weapon)
				{
					output += "Weapon-";
				} else if (player.inventory.contents[i] is HealthPotion)
				{
					output += "HealthPotion-";
				} else if (player.inventory.contents[i] is Key)
				{
					output += "Key-";
				} else
				{
					output += "Item-";
				}
				output += serializer.Serialize(player.inventory.contents[i]);
			}
			System.IO.File.WriteAllText(saveFileName, output);
		}
		public static Player LoadPlayerFromSaveFile()
		{
			string[] input = System.IO.File.ReadAllLines(saveFileName);
			Player player = serializer.Deserialize<Player>(input[0]);
			Player output = new Player(true);
			output.Health = player.Health;
			output.equippedItem = player.equippedItem;
			for (int i = 1; i <= (input.Length - 1); i++)
			{
				string[] itemInfo = input[i].Split('-');
				switch (itemInfo[0])
				{
					case "Weapon":
						output.inventory.AddItem(serializer.Deserialize<Weapon>(itemInfo[1]));
						break;
					case "HealthPotion":
						output.inventory.AddItem(serializer.Deserialize<HealthPotion>(itemInfo[1]));
						break;
					case "Key":
						Key key = serializer.Deserialize<Key>(itemInfo[1]);
						Console.WriteLine("As you come back to this world the " + key.Name + " vanishes from your inventory.");
						break;
					case "Item":
						output.inventory.AddItem(serializer.Deserialize<Item>(itemInfo[1]));
						break;
				}
			}
			output.CurrentRoom = player.CurrentRoom;
			return output;
		}
	}
}
