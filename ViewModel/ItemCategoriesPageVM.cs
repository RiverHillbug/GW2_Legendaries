using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace GW2_Legendaries.ViewModel
{
	internal class ItemCategoriesPageVM : ObservableObject
	{
		public RelayCommand ShowItemsCommand { get; private set; }

		public ItemCategoriesPageVM()
		{
			ShowItemsCommand = new RelayCommand(ShowItems);
		}

		private void ShowItems()
		{
			
		}
	}
}
