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
					this.attackDesc = "The BadBoi swings at you with it's weak arms and hits you!";
					return this.Damage;
                case "Fritz":
                    this.attackDesc = "Fritz stabs you with a stick!";
                    return this.Damage;
			}
			return 0;
		}
		public Weapon DropWeapon()
		{
			List<string> materials = new List<string>();
			materials.Add("copper");
			materials.Add("bronze");
			materials.Add("iron");
			materials.Add("steel");

			List<string> weapons = new List<string>();
			weapons.Add("dagger");
			weapons.Add("sword");
			weapons.Add("longsword");
			weapons.Add("mace");
			weapons.Add("hammer");
			weapons.Add("axe");

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
			names.Add("shiny potion");
			names.Add("glistering potion");
			names.Add("shining potion");
			names.Add("life potion");

			Random RNG = new Random();

			string name = names[RNG.Next(names.Count())];
			double weight = this.lootQuality / 20;
			int healingPower = this.lootQuality;

			HealthPotion output = new HealthPotion(name, weight, healingPower);
			Console.WriteLine("The " + this.DisplayName + " has dropped a " + name + "!");
			return output;
		}
	}
}
