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
			//string[] output = {"", ""};
			//output[0] = "" + player.Health;
			//output[1] = player.CurrentRoom.GetShortDescription();
			//System.IO.File.WriteAllLines("save.txt", output);
		}
		public static Player LoadPlayerFromSaveFile()
		{
			Player player;
			string input = System.IO.File.ReadAllText(saveFileName);
			player = serializer.Deserialize<Player>(input);
			return player;
		}
	}
}
