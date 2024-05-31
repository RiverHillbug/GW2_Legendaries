using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace GW2_Legendaries.ViewModel
{
	internal class ItemCategoriesPageVM : ObservableObject
	{
		public RelayCommand<string> ShowItemsCommand { get; }
		public List<string> ItemCategoriesTexts { get; } = [];
		public string SelectedCategory { get; private set; } = string.Empty;

		public ItemCategoriesPageVM()
		{
			ShowItemsCommand = new RelayCommand<string>(ShowItems);
		}

		private void ShowItems(string? category)
		{
			if (category == null)
				return;

			SelectedCategory = category;
			OnPropertyChanged(nameof(SelectedCategory));
			MainWindowVM.Instance?.SwitchPage();
		}
	}
}
