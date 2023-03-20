using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZuulCS
{
	public class Enemy
	{
		Random RNG = new Random();
		public string DisplayName { get; }
		public int Health { get; private set; }
		public int MaxHealth { get; }
		public int Damage { get; }
		public int lootQuality { get; }
		public string attackDesc { get; private set; }
		public Enemy(string type)
		{
			this.DisplayName = type;
			switch (type)
			{
				case "BadBoi":
					this.Health = 5;
					this.Damage = 1;
					this.lootQuality = 15;
					break;
                case "Fritz":
                    this.Health = 95;
                    this.Damage = 12;
					this.lootQuality = 30;
                    break;
			}
			this.MaxHealth = this.Health;
		}
		public void DamageEnemy(int input)
		{
			this.Health = this.Health - input;
		}
		public int AttackPlayer()
		{
			switch (DisplayName)
			{
				case "BadBoi":
					this.attackDesc = "The BadBoi swings at you with it's weak arms and hits you!\n";
					return this.Damage;
                case "Fritz":
                    this.attackDesc = "Fritz stabs you with a stick!\n";
                    return this.Damage;
			}
			return 0;
		}
		public Weapon DropWeapon()
		{
			List<string> materials = new List<string>();
			materials.Add("Copper");
			materials.Add("Bronze");
			materials.Add("Iron");
			materials.Add("Steel");

			List<string> weapons = new List<string>();
			weapons.Add("Dagger");
			weapons.Add("Sword");
			weapons.Add("Longsword");
			weapons.Add("Mace");
			weapons.Add("Hammer");
			weapons.Add("Axe");

			Random RNG = new Random();

			string weaponName;
			weaponName = materials[RNG.Next(materials.Count - 1)] + " " + weapons[RNG.Next(weapons.Count - 1)];
			int weaponDamage = RNG.Next(lootQuality);
			double weaponWeight = weaponDamage / 10;

			Weapon output = new Weapon(weaponName, weaponWeight, weaponDamage);
			Console.WriteLine("The " + this.DisplayName + " has dropped a " + weaponName + "!");
			return output;
		}
		public HealthPotion DropHealthPotion()
		{
			List<string> names = new List<string>();
			names.Add("Shiny Potion");
			names.Add("Glistering Potion");
			names.Add("Shining Potion");
			names.Add("Life Potion");

			Random RNG = new Random();

			string name = names[RNG.Next(names.Count())];
			int healingPower = RNG.Next(this.lootQuality);
            double weight = RNG.Next(healingPower);
            weight = weight / 20;

            HealthPotion output = new HealthPotion(name, weight, healingPower);
			Console.WriteLine("The " + this.DisplayName + " has dropped a " + name + "!");
			return output;
		}
		public string statCard(int width)
		{
			string output = "";
			output += "╔" + TextEffects.GenerateLineText("═", this.DisplayName, width) + "╕\n";
			output += "╟─" + TextEffects.FinishLine(width - 2, " Health:", " ") + "│\n";
			output += "║" + TextEffects.GenerateLineText(" ", TextEffects.GenerateProgressBar(width/2, this.MaxHealth, this.Health), width - 2) + "│\n";
			output += "╚" + TextEffects.GenerateLine("═", width - 1) + "╛\n";
            return output;
		}
	}
}
