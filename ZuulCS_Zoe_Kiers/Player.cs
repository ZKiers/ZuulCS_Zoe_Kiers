﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZuulCS
{
	public class Player
	{
		public Inventory inventory = new Inventory(4);
		public Room CurrentRoom { get; set; }
		public Room LastRoom { get; set; }
		private int health;
		private Weapon equippedItem = null;

		//Starting Items
		private Weapon dagger = new Weapon("Dagger", 1, 5);
		public Player()
		{
			health = 100;
			inventory.AddItem(dagger);
			LastRoom = null;
		}
		public void Heal(int amount)
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
		public void Damage(int amount)
		{
			health -= amount;
		}
		public string GetHealth()
		{
			return "You have " + health + " health!";
		}
		public bool IsAlive()
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
		public string GetInventoryDesc()
		{
			String output = "";
			if (this.equippedItem != null)
			{
				output += "You have in your equipment slot: " + this.equippedItem.Name + ".\n";
			} else
			{
				output += "You have no weapon in your equipment slot.\n";
			}
			if (inventory.ShowItems() != null)
			{
				output += "You have in your inventory: " + inventory.ShowItems() + ".\n";
			} else
			{
				output += "You have no items in your inventory.\n";
			}
			output += "The weight of the items in your inventory is: " + inventory.GetCurrentWeight() + "kg.\n";
			output += "You can carry a maximum of: " + inventory.GetWeightLimit() + "kg.";
			return output;
		}
		public String PickupItem(Command command)
		{
			string itemName = command.SecondWord;
			if (!command.HasSecondWord())
			{
				String output = "Take what?";
				TextEffects.ErrorMessage(output);
				return null;
			} else
			{
				String output;
				switch (inventory.TakeItemFrom(CurrentRoom.inventory, command.SecondWord)) {
					case 0:
						output = itemName + " is not present in this room.";
						TextEffects.ErrorMessage(output);
						return null;
					case 1:
						return "You picked up: " + itemName + ".";
					case 2:
						output = "This item is too heavy!\n" + itemName + " weighs: " + CurrentRoom.inventory.FindItem(itemName).Weight + "kg!";
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
			if (!command.HasSecondWord())
			{
				TextEffects.ErrorMessage("Drop what?");
				return null;
			} else if (CurrentRoom.inventory.TakeItemFrom(inventory, command.SecondWord) == 0)
			{
				String output = command.SecondWord + " is not in your inventory.";
				TextEffects.ErrorMessage(output);
				return null;
			} else
			{
				return "You dropped: " + command.SecondWord + ".";
			}
		}
		public String UseItem(Command command)
		{
			Item item = this.inventory.FindItem(command.SecondWord);
			if (!command.HasSecondWord())
			{
				TextEffects.ErrorMessage("Use what?");
				return null;
			} else if (item == null)
			{
				TextEffects.ErrorMessage("You cannot use something you don't have.");
				return null;
			}
			if (item is Key)
			{
				string output = item.UseItem(command, this.CurrentRoom);
				if (output != null)
				{
					this.inventory.RemoveItem(item);
				}
				return output;
			}
			return item.UseItem(command, this.CurrentRoom);
		}
		public string Attack()
		{
			if (this.equippedItem == null)
			{
				TextEffects.ErrorMessage("You don't have a weapon equipped!");
				return null;
			}
			this.Damage(equippedItem.GetDamage());
			return "Since there are no enemies you decide to fight an imaginary creature.\nYou hurt yourself like the imbecile you are...";
		}
		public String EquipItem(Command command)
		{
			Weapon item;
			item = (Weapon)this.inventory.FindItem(command.SecondWord);
			if (!command.HasSecondWord())
			{
				TextEffects.ErrorMessage("Equip what?");
				return null;
			} else if (item == null)
			{
				String output = "You cannot equip something you don't have: " + command.SecondWord + ".";
				TextEffects.ErrorMessage(output);
				return null;
			} else if (this.inventory.FindItem(command.SecondWord) is Weapon)
			{
				String output = "";
				if (this.equippedItem != null)
				{
					inventory.AddItem(this.equippedItem);
					output += "You've unequipped: " + this.equippedItem.Name + ".\n";
				}
				this.equippedItem = item;
				this.inventory.RemoveItem(item);
				output += "You've equipped: " + this.equippedItem.Name + ".";
				return output;
			} else
			{
				String output = command.SecondWord + " is not a weapon!";
				TextEffects.ErrorMessage(output);
				return null;
			}
		}
		public String Unequip()
		{
			if(this.equippedItem != null)
			{
				Weapon item = this.equippedItem;
				String itemName = item.Name;
				this.inventory.AddItem(item);
				this.equippedItem = null;
				return "You have unequipped: " + itemName + "!";
			}
			TextEffects.ErrorMessage("You don't have any weapons equipped!");
			return null;
		}
	}
}
