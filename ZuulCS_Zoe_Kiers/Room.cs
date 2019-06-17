using System.Collections.Generic;

namespace ZuulCS
{
	public class Room
	{
		private string description;
		private Dictionary<string, Room> exits; // stores exits of this room.
		public Inventory inventory = new Inventory(0);

		//Makes a key, only used if the SetLocked() function is activated.
		private string keyString;
		private Key keyItem;
		private bool isLocked = false;

		/**
	     * Create a room described "description". Initially, it has no exits.
	     * "description" is something like "in a kitchen" or "in an open court
	     * yard".
	     */
		public Room(string description)
		{
			this.description = description;
			exits = new Dictionary<string, Room>();
		}

		/**
	     * Define an exit from this room.
	     */
		public void SetExit(string direction, Room neighbor)
		{
			exits[direction] = neighbor;
		}

		/**
	     * Return the description of the room (the one that was defined in the
	     * constructor).
	     */
		public string GetShortDescription()
		{
			return description;
		}
		public string GetItemDescription()
		{
			if (inventory.ShowItems() != null)
			{
				if (inventory.GetItemAmount() == 1)
				{
					return "There is one item in this room: " + inventory.ShowItems() + ".";
				}
				return "There are multiple items in this room: " + inventory.ShowItems() + ".";
			}
			return "There are no items in this room.";
		}
		/**
	     * Return a long description of this room, in the form:
	     *     You are in the kitchen.
	     *     Exits: north west
	     */
		public string GetLongDescription()
		{
			string returnstring = "You are ";
			returnstring += description;
			returnstring += ".\n";
			returnstring += GetItemDescription();
			returnstring += "\n";
			returnstring += GetExitstring();
			return returnstring;
		}

		/**
	     * Return a string describing the room's exits, for example
	     * "Exits: north, west".
	     */
		private string GetExitstring()
		{
			string returnstring = "Exits:";

			// because `exits` is a Dictionary, we can't use a `for` loop
			int commas = 0;
			foreach (string key in exits.Keys)
			{
				if (commas != 0 && commas != exits.Count)
				{
					returnstring += ",";
				}
				commas++;
				returnstring += " " + key;
			}
			return returnstring;
		}

		/**
	     * Return the room that is reached if we go from this room in direction
	     * "direction". If there is no room in that direction, return null.
	     */
		public Room GetExit(string direction)
		{
			if (exits.ContainsKey(direction))
			{
				return (Room)exits[direction];
			}
			else
			{
				return null;
			}

		}
		public void SetLocked(Room keyLocation, string keyName)
		{
			this.isLocked = true;
			keyItem = new Key(keyName, 0.1);
			keyString = keyItem.GenerateKey();
			keyLocation.inventory.AddItem(keyItem);
		}
		public bool IsLocked()
		{
			return isLocked;
		}
		public bool UseKey(Key key)
		{
			string hash = key.GetKey();
			if (hash == this.keyString)
			{
				this.isLocked = false;
				return true;
			}
			return false;
		}
	}
}
