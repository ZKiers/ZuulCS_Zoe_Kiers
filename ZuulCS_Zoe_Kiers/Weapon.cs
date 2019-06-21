using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZuulCS
{
	public class Weapon : Item
	{
		public int damage { get; set; }
		public Weapon(String name, double weight, int setDamage)
		{
			this.Name = name;
			this.Weight = weight;
			this.damage = setDamage;
		}
		public Weapon() { }
		public override string UseItem(Command command, Room currentRoom)
		{
			return "This is a weapon, you don't just \"use\" it.";
		}
		public int GetDamage()
		{
			return damage;
		}
	}
}
