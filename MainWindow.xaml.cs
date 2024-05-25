using GW2_Legendaries.Model;
using GW2_Legendaries.Repository;
using System.Windows;

// requirement: load embedded resources
// GetManifestResourceStream

namespace GW2_Legendaries
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			List<Item>? items = ItemRepository.GetItems(string.Empty);
			int a = 3;
		}
	}
}
