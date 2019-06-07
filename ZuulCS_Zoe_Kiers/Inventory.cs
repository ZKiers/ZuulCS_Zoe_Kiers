using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZuulCS
{
	public class Inventory
	{
		private List<Item> contents = new List<Item>();
		public Inventory()
		{

		}
		public void AddItem(Item input)
		{
			contents.Add(input);
		}
		public void RemoveItem(Item input)
		{
			contents.Remove(input);
		}
		public Item FindItem(String name)
		{
			for (int i = 0; i <= (contents.Count - 1); i++)
			{
				if (contents[i].GetName() == name)
				{
					return contents[i];
				}
			}
			return null;
		}
		public bool TakeItemFrom(Inventory target, String itemName)
		{
			Item item = target.FindItem(itemName);
			if (item != null)
			{
				target.RemoveItem(item);
				AddItem(item);
				return true;
			}
			return false;
		}
		public int GetItemAmount()
		{
			return contents.Count;
		}
		public string ShowItems()
		{
			if (contents.Count < 1)
			{
				return null;
			}
			string output = "";
			for (int i = 0; i < contents.Count; i++)
				output += (contents.Count - i) > 1 ? contents[i].GetName() + ", " : contents[i].GetName();
			return output;
		}
	}
}
