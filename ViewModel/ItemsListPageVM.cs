using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using GW2_Legendaries.Model;

namespace GW2_Legendaries.ViewModel
{
	public class ItemsListPageVM : ObservableObject
	{
		public List<Item> Items { get; set; }

		public ItemsListPageVM()
		{
			Items = [];
		}
	}
}
