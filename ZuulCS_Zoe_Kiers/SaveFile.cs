using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZuulCS
{
	public static class SaveFile
	{
		public static void GenerateSaveFile(Player player)
		{
			string[] output = {"", ""};
			output[0] = "" + player.Health;
			output[1] = player.CurrentRoom.GetShortDescription();
			System.IO.File.WriteAllLines("save.txt", output);
		}
		public static void LoadPlayerFromSaveFile()
		{
			Player player = new Player();
			player.inventory.RemoveItem(player.inventory.FindItem("Dagger"));
			string[] input = System.IO.File.ReadAllLines("save.txt");
			player.Health = int.Parse(input[0]);
			TextEffects.ErrorMessage(input[1]);
		}
	}
}
