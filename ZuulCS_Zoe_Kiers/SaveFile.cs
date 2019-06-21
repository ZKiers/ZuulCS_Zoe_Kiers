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
		private static string saveFileName = "Save.JSON";
		public static void GenerateSaveFile(Player player)
		{
			string output = serializer.Serialize(player);
			System.IO.File.WriteAllText(saveFileName, output);
		}
		public static Player LoadPlayerFromSaveFile()
		{
			string input = System.IO.File.ReadAllText(saveFileName);
			Player player = serializer.Deserialize<Player>(input);
			Player output = new Player();
			output.Health = player.Health;
			output.inventory = player.inventory;
			output.equippedItem = player.equippedItem;
			output.CurrentRoom = player.CurrentRoom;
			return output;
		}
	}
}
