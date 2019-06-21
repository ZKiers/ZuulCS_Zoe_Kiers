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
		private int lootQuality;
		public string attackDesc { get; private set; }
		public Enemy(string type)
		{
			this.DisplayName = type;
			switch (type)
			{
				case "BadBoi":
					this.Health = 5;
					this.Damage = 1;
					this.lootQuality = 100;
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
					this.attackDesc = "The BadBoi swings at you with it's little knife and hits you!";
					return this.Damage;
			}
			return 0;
		}
	}
}
