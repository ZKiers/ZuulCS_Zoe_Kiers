﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZuulCS
{
	public class Inventory
	{
		private List<Item> contents = new List<Item>();
		private int weightLimit;
		private bool limitWeight;
		public Inventory(int setWeightLimit)
		{
			weightLimit = setWeightLimit;
			limitWeight = false;
			if (weightLimit > 0)
			{
				limitWeight = true;
			}
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
				if (string.Compare(contents[i].GetName(), name) == 0)
				{
					return contents[i];
				}
			}
			return null;
		}
		public int TakeItemFrom(Inventory target, String itemName)
		{
			Item item = target.FindItem(itemName);
			if (item != null)
			{
				if((item.GetWeight() + GetCurrentWeight()) > weightLimit && this.limitWeight)
				{
					return 2;
				}
				target.RemoveItem(item);
				AddItem(item);
				return 1;
			}
			return 0;
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
		public int GetCurrentWeight()
		{
			int output = 0;
			for (int i = 0; i < contents.Count; i++)
			{
				output += contents[i].GetWeight();
			}
			return output;
		}
		public int GetWeightLimit()
		{
			return weightLimit;
		}
	}
}
