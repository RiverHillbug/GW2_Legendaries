using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GW2_Legendaries.Model;
using GW2_Legendaries.Repository;

namespace GW2_Legendaries.ViewModel
{
	internal class ItemsListPageVM : ObservableObject
	{
		public RelayCommand<int> ShowItemDescriptionCommand { get; }
		public List<Item> Items { get; } = [];
		public string? CurrentCategory { get; set; } = null;
		public Item? SelectedItem { get; set; } = null;

		public ItemsListPageVM()
		{
			ShowItemDescriptionCommand = new(ShowItemDescription);
		}

		public void UpdateList(string category)
		{
			Items.Clear();

			List<Item>? items = ItemRepository.GetItemsAsync(category);

			if (items != null)
				Items.AddRange(items);
		}

		public void ShowItemDescription(int ID)
		{
			SelectedItem = ItemRepository.GetItemWithID(ID);
			MainWindowVM.Instance?.SwitchPage();
		}
	}
}
