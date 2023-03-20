using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZuulCS
{
	public abstract class Item
	{
		public string Name { get; set; }
		public double Weight { get; set; }
		public bool Consumed { get; set; }
		public Item()
		{
		}
		public virtual string UseItem(Command command, Player player)
		{
			return "This is just a normal item, it has no use...\n What did you want to do? Snort it??\n";
		}
	}
}
