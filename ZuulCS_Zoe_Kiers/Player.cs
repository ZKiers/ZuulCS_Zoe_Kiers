using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZuulCS
{
	public class Player
	{
		public Inventory inventory = new Inventory();
		private Room currentRoom;
		private int health;

		//Starting Items
		private Item dagger = new Item("Dagger");
		public Player()
		{
			health = 100;
			inventory.AddItem(dagger);
		}
		public void heal(int amount)
		{
			if ((health + amount) > 100)
			{
				health = 100;
			}
			else
			{
				health += amount;
			}
		}
		public void damage(int amount)
		{
			health -= amount;
		}
		public string getHealth()
		{
			return "You have " + health + " health!";
		}
		public bool isAlive()
		{
			if (health <= 0)
			{
				return false;
			}
			else
			{
				return true;
			}
		}
		public void SetCurrentRoom(Room input)
		{
			currentRoom = input;
		}
		public Room GetCurrentRoom()
		{
			return currentRoom;
		}
		public string GetInventory()
		{
			return "You have in your inventory: " + inventory.ShowItems();
		}
		public String PickupItem(Command command)
		{
			if (!command.hasSecondWord())
			{
				return "Take what?";
			} else if (!inventory.TakeItemFrom(currentRoom.inventory, command.getSecondWord()))
			{
				return command.getSecondWord() + " is not present in this room.";
			} else
			{
				return "You picked up: " + command.getSecondWord() + ".";
			}
		}
		public String DropItem(Command command)
		{
			if (!command.hasSecondWord())
			{
				return "Drop what?";
			} else if (!currentRoom.inventory.TakeItemFrom(inventory, command.getSecondWord()))
			{
				return command.getSecondWord() + " is not in your inventory.";
			} else
			{
				return "You dropped: " + command.getSecondWord() + ".";
			}
		}
	}
}
