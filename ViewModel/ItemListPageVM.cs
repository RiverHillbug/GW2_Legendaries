using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GW2_Legendaries.Model;
using GW2_Legendaries.Repository;
using System.Collections.ObjectModel;

namespace GW2_Legendaries.ViewModel
{
	internal class ItemListPageVM : ObservableObject
	{
		public RelayCommand<int> ShowItemDescriptionCommand { get; }
		public RelayCommand GoBackCommand { get; }
		public ObservableCollection<Item> Items { get; } = [];
		public string? CurrentCategory { get; private set; } = null;
		public Item? SelectedItem { get; set; } = null;
		public static ItemListPageVM? Instance { get; private set; } = null;

		public ItemListPageVM()
		{
			ShowItemDescriptionCommand = new(ShowItemDescription);
			GoBackCommand = new RelayCommand(GoBack);
			Instance = this;
		}

		public void UpdateList(string category)
		{
			Items.Clear();
			OnPropertyChanged(nameof(Items));
			CurrentCategory = category;
			OnPropertyChanged(nameof(CurrentCategory));

			Task<List<Item>> taskRes = Task.Run(() =>
			{
#if LOCAL
#else
				List<Item> items = ItemRepository.GetItemsAsync(category)?.Result ?? [];
#endif
				return items;
			});

			taskRes.ConfigureAwait(true).GetAwaiter().OnCompleted(() =>
			{
				if (taskRes != null)
				{
					foreach (Item item in taskRes.Result)
					{
						Items.Add(item);
					}
					
					OnPropertyChanged(nameof(Items));
				}
			});
		}

		public void ShowItemDescription(int ID)
		{
			SelectedItem = ItemRepository.GetItemWithID(ID);
			OnPropertyChanged(nameof(SelectedItem));
			MainWindowVM.Instance?.SwitchPage();
		}


		private void GoBack()
		{
			MainWindowVM.Instance?.GoBack();
		}
	}
}
