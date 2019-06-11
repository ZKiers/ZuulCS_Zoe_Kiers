using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZuulCS
{
	public class Weapon : Item
	{
		private int damage;
		public Weapon(String name, int weight, int setDamage)
		{
			this.SetName(name);
			this.SetWeight(weight);
			this.damage = setDamage;
			isWeapon = true;
		}
		public override String useItem()
		{
			return "This is a weapon, you don't just \"use\" it.";
		}
		public int getDamage()
		{
			return damage;
		}
	}
}
