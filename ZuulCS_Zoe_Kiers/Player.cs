﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZuulCS
{
	public class Player
	{
		public Inventory inventory = new Inventory(10);
		public Room CurrentRoom { get; set; }
		public Room LastRoom { get; set; }
		public int Health { get; set; }
		public Weapon equippedItem { get; set; } = null;
		public Room FightingInRoom { get; set; }
		public Player(bool fromsave)
		{
			if (!fromsave)
			{
				Health = 100;
				Weapon startingDagger = new Weapon("Dagger", 1, 5);
				inventory.AddItem(startingDagger);
			}
			LastRoom = null;
		}
		public Player()
		{
			LastRoom = null;
		}
		public string Heal(int amount)
		{
			if ((Health + amount) > 100)
			{
				int healAmount = amount - ((Health + amount) - 100);
				Health = 100;
				return "You've healed by: " + healAmount + "!";
			}
			else
			{
				Health += amount;
				return "You've healed by: " + amount + "!";
			}
		}
		public void Damage(int amount)
		{
			Health -= amount;
		}
		public string GetHealth()
		{
			string output = "";
			output += "\nYou have " + Health + " health!\n";
			return output;
		}
		public bool IsAlive()
		{
			if (Health <= 0)
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
            int lineLength = 40;
			String output = "";
            output += "<<" + TextEffects.GenerateLine("-", lineLength) + ">>\n";
            output += "╔" + TextEffects.GenerateLineText("═", "Equipment Slot", lineLength - 1) + "╕\n";
            if (this.equippedItem != null)
			{
                output += TextEffects.FinishLine(lineLength,"╟─ " + this.equippedItem.Name, " ") + "│\n";
                output += TextEffects.FinishLine(lineLength + 6,"║   <S>Weight: " + this.equippedItem.Weight + "kg<S>", " ") + "│\n";
                output += TextEffects.FinishLine(lineLength + 6,"║   <S>Damage: " + this.equippedItem.damage + "<S>", " ") + "│\n";

            } else
			{
                output += TextEffects.FinishLine(lineLength, "║  Empty.", " ") + "│\n";
            }
            output += TextEffects.FinishLine(lineLength, "╠" + TextEffects.GenerateLineText("═", "Inventory", lineLength - 1), " ") + "╡\n";
            if (inventory.ShowItems() != null)
			{
				for (int i = 0; i < inventory.contents.Count(); i++)
                {
                    Item item = inventory.contents[i];
                    output += TextEffects.FinishLine(lineLength,"╟─ " + item.Name, " ") + "│\n";
                    output += TextEffects.FinishLine(lineLength + 6,"║   <S>Weight: " + item.Weight + "kg<S>", " ") + "│\n";
                    if (item is Weapon)
                    {
                        Weapon weapon = (Weapon)item;
                        output += TextEffects.FinishLine(lineLength + 6,"║   <S>Damage: " + weapon.damage + "<S>", " ") + "│\n";
                    } else if (item is HealthPotion)
                    {
                        HealthPotion potion = (HealthPotion)item;
                        output += TextEffects.FinishLine(lineLength + 6, "║   <S>Heals : " + potion.HealingPower + "<S>", " ") + "│\n";
                    }
                }
            } else
			{
				output += TextEffects.FinishLine(lineLength, "║  Empty.", " ") + "│\n";
			}
            output += "╚" + TextEffects.GenerateLine("═", lineLength - 1) + "╛\n";
            output += "Current total weight   : " + inventory.GetCurrentWeight() + "kg.\n";
			output += "Current maximum weight : " + inventory.GetWeightLimit() + "kg.\n";
            output += "<<" + TextEffects.GenerateLine("-", lineLength) + ">>\n";
            return output;
		}
		public String PickupItem(Command command)
		{
			string itemName = command.SecondWord;
			if (command.HasThirdWord()) { itemName += " " + command.ThirdWord; }
			if (!command.HasSecondWord())
			{
				String output = "Take what?";
				TextEffects.ColoredMessage(output, "DarkRed");
				return null;
			} else
			{
				String output;
				switch (inventory.TakeItemFrom(CurrentRoom.inventory, itemName)) {
					case 0:
						output = itemName + " is not present in this room.";
						TextEffects.ColoredMessage(output, "DarkRed");
						return null;
					case 1:
						return "You picked up: " + itemName + ".";
					case 2:
						output = "This item is too heavy!\n" + itemName + " weighs: " + CurrentRoom.inventory.FindItem(itemName).Weight + "kg!";
						TextEffects.ColoredMessage(output, "DarkRed");
						return null;
				}
				output = "Error: something went wrong in the pickup item function, maybe the developer should have sticked to a boolean...";
				TextEffects.ColoredMessage(output, "DarkRed");
				return null;
			}
		}
		public String DropItem(Command command)
		{
			string itemName = command.SecondWord;
			if (command.HasThirdWord()) { itemName += " " + command.ThirdWord; }
			if (!command.HasSecondWord())
			{
				TextEffects.ColoredMessage("Drop what?", "DarkRed");
				return null;
			} else if (CurrentRoom.inventory.TakeItemFrom(inventory, itemName) == 0)
			{
				String output = itemName + " is not in your inventory.";
				TextEffects.ColoredMessage(output, "DarkRed");
				return null;
			} else
			{
				return "You dropped: " + itemName + ".";
			}
		}
		public String UseItem(Command command)
		{
			Item item = null;
			if(command.HasSecondWord())
			{
                item = this.inventory.FindItem(command.SecondWord);
            }
			if (item == null && command.HasThirdWord())
			{ 
				item = this.inventory.FindItem((command.SecondWord + " " + command.ThirdWord));
			}
			if (!command.HasSecondWord())
			{
				TextEffects.ColoredMessage("Use what?\n", "DarkRed");
				return null;
			} else if (item == null)
			{
				TextEffects.ColoredMessage("You cannot use something you don't have.\n", "DarkRed");
				return null;
			}
			/*if (item is HealthPotion)
			{
				string output = "You chug the potion not really knowing what it does.\n";
				output += this.Heal(((HealthPotion)item).HealingPower);
				this.inventory.RemoveItem(item);
				return output;

			} else if (item is Key)
			{
				string output = item.UseItem(command, this.CurrentRoom);
				if (output != null)
				{
					this.inventory.RemoveItem(item);
				}
				return output;
			}*/
			string output = item.UseItem(command, this);
			if(item.Consumed)
			{
				this.inventory.RemoveItem(item);
			}
			return output;
		}
		public string Attack()
		{
			if (this.equippedItem == null)
			{
				TextEffects.ColoredMessage("You don't have a weapon equipped!", "DarkRed");
				return null;
			}
			this.Damage(equippedItem.GetDamage());
			return "Since there are no enemies you decide to fight an imaginary creature.\nYou hurt yourself like the imbecile you are...";
		}
		public string AttackEnemy()
		{
			if (this.equippedItem == null)
			{
				TextEffects.ColoredMessage("You don't have a weapon equipped!", "DarkRed");
				return null;
			}
			FightingInRoom.Enemies[0].DamageEnemy(this.equippedItem.damage);
			return "You hit the " + FightingInRoom.Enemies[0].DisplayName + " with your " + this.equippedItem.Name + " and did " + this.equippedItem.damage + " damage!";
		}
		public String EquipItem(Command command)
		{
            string itemName = command.SecondWord;
            if (command.HasThirdWord()) { itemName += " " + command.ThirdWord; }
			Weapon item;
			item = (Weapon)this.inventory.FindItem(itemName);
			if (!command.HasSecondWord())
			{
				TextEffects.ColoredMessage("Equip what?", "DarkRed");
				return null;
			} else if (item == null)
			{
				String output = "You cannot equip something you don't have: " + itemName + ".";
				TextEffects.ColoredMessage(output, "DarkRed");
				return null;
			} else if (item is Weapon)
			{
				string oldItemName = null;
				String output = "";
				if (this.equippedItem != null)
				{
					oldItemName = this.equippedItem.Name;
					inventory.AddItem(this.equippedItem);
					output += "You've unequipped: " + this.equippedItem.Name + ".\n";
				}
				this.equippedItem = item;
				this.inventory.RemoveItem(item);
				output += "You've equipped: " + this.equippedItem.Name + ".";
				if (inventory.GetWeightLeft() < 0)
				{
					CurrentRoom.inventory.TakeItemFrom(inventory, oldItemName);
					output += "\nUnfortunately you were too weak to carry this item so you dropped it instead.";
				}
				return output;
			} else
			{
				String output = command.SecondWord + " is not a weapon!";
				TextEffects.ColoredMessage(output, "DarkRed");
				return null;
			}
		}
		public String Unequip()
		{
			if(this.equippedItem != null)
			{
				Weapon item = this.equippedItem;
				string itemName = item.Name;
				this.inventory.AddItem(item);
				this.equippedItem = null;
				if (inventory.GetWeightLeft() < 0)
				{
					CurrentRoom.inventory.TakeItemFrom(inventory, itemName);
					return "You have unequipped: " + itemName + "!\nUnfortunately you were too weak to carry this item so you dropped it instead.";
				}
				return "You have unequipped: " + itemName + "!";
			}
			TextEffects.ColoredMessage("You don't have any weapons equipped!", "DarkRed");
			return null;
		}
	}
}
