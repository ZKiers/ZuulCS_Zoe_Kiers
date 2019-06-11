using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZuulCS
{
	public class Player
	{
		public Inventory inventory = new Inventory(4);
		private Room currentRoom;
		private int health;
		private Weapon equippedItem = null;

		//Starting Items
		private Weapon dagger = new Weapon("Dagger", 1, 5);
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
		public string GetInventoryDesc()
		{
			return "You have in your inventory: " + inventory.ShowItems() + "\nThe weight of the items in your inventory is: " + inventory.GetCurrentWeight() + "kg\nYou can carry a maximum of: " + inventory.getWeightLimit() + "kg";
		}
		public String PickupItem(Command command)
		{
			string itemName = command.getSecondWord();
			if (!command.hasSecondWord())
			{
				String output = "Take what?";
				TextEffects.ErrorMessage(output);
				return null;
			} else
			{
				String output;
				switch (inventory.TakeItemFrom(currentRoom.inventory, command.getSecondWord())) {
					case 0:
						output = itemName + " is not present in this room.";
						TextEffects.ErrorMessage(output);
						return null;
					case 1:
						return "You picked up: " + itemName + ".";
					case 2:
						output = "This item is too heavy!\n" + itemName + " weighs: " + currentRoom.inventory.FindItem(itemName).GetWeight() + "kg!";
						TextEffects.ErrorMessage(output);
						return null;
				}
				output = "Error: something went wrong in the pickup item function, maybe the developer should have sticked to a boolean...";
				TextEffects.ErrorMessage(output);
				return null;
			}
		}
		public String DropItem(Command command)
		{
			if (!command.hasSecondWord())
			{
				TextEffects.ErrorMessage("Drop what?");
				return null;
			} else if (currentRoom.inventory.TakeItemFrom(inventory, command.getSecondWord()) == 0)
			{
				String output = command.getSecondWord() + " is not in your inventory.";
				TextEffects.ErrorMessage(output);
				return null;
			} else
			{
				return "You dropped: " + command.getSecondWord() + ".";
			}
		}
		public String UseItem(Command command)
		{
			Item item = this.inventory.FindItem(command.getSecondWord());
			if (!command.hasSecondWord())
			{
				TextEffects.ErrorMessage("Use what?");
				return null;
			} else if (item == null)
			{
				TextEffects.ErrorMessage("You cannot use something you don't have.");
				return null;
			}
			return inventory.FindItem(command.getSecondWord()).useItem();
		}
		public String Attack()
		{
			if (this.equippedItem == null)
			{
				TextEffects.ErrorMessage("You don't have a weapon equipped!");
				return null;
			}
			this.damage(equippedItem.getDamage());
			return "Since there are no enemies you decide to fight an imaginary creature.\n You hurt yourself like the imbecile you are...";
		}
		public String EquipItem(Command command)
		{
			Weapon item;
			item = (Weapon)this.inventory.FindItem(command.getSecondWord());
			if (!command.hasSecondWord())
			{
				TextEffects.ErrorMessage("Equip what?");
				return null;
			} else if (item == null)
			{
				String output = "You cannot equip something you don't have: " + command.getSecondWord() + ".";
				TextEffects.ErrorMessage(output);
				return null;
			} else if (this.inventory.FindItem(command.getSecondWord()).isWeapon)
			{
				String output = "";
				if (this.equippedItem != null)
				{
					inventory.AddItem(this.equippedItem);
					output += "You've unequipped: " + this.equippedItem.GetName() + ".\n";
				}
				this.equippedItem = item;
				this.inventory.RemoveItem(item);
				output += "You've equipped: " + this.equippedItem.GetName() + ".";
				return output;
			} else
			{
				String output = command.getSecondWord() + " is not a weapon!";
				TextEffects.ErrorMessage(output);
				return null;
			}
		}
	}
}
