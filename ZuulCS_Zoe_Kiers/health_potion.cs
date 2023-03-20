using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZuulCS
{
	public class HealthPotion : Item
	{
		public int HealingPower { get; set; }
		public HealthPotion(string name, double weight, int healingPower)
		{
			this.Name = name;
			this.Weight = weight;
			this.HealingPower = healingPower;
		}
		public HealthPotion() { }
		public override string UseItem(Command command, Player player)
		{
			player.Heal(this.HealingPower);
			this.Consumed = true;
			return "You chug the potion down your throat not really sure what it does.";
		}
	}
}
