using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZuulCS
{
	public class Item
	{
		private String name;
		private int weight;
		public bool isWeapon;
		public Item()
		{
			isWeapon = false;
		}
		public String GetName()
		{
			return name;
		}
		public void SetName(String input)
		{
			this.name = input;
		}
		public int GetWeight()
		{
			return weight;
		}
		public void SetWeight(int input)
		{
			this.weight = input;
		}
		public virtual String UseItem()
		{
			return "This is just a normal item, it has no use...\n What did you want to do? Snort it??";
		}
	}
}
