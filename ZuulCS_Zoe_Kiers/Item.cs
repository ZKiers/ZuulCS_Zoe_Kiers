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
		public Item(String setName)
		{
			name = setName;
		}
		public String GetName()
		{
			return name;
		}
	}
}
