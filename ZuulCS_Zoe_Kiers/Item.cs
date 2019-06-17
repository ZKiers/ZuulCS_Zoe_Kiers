using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZuulCS
{
	public class Item
	{
		public string Name { get; set; }
		public int Weight { get; set; }
		public Item()
		{
		}
		public virtual string UseItem(Command command, Room currentRoom)
		{
			return "This is just a normal item, it has no use...\n What did you want to do? Snort it??";
		}
	}
}
