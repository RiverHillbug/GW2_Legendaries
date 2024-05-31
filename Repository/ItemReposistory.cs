using GW2_Legendaries.Model;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;

namespace GW2_Legendaries.Repository
{
	public class ItemRepository
	{
		private static readonly List<Item> m_Items = [];

		private static void DeserializeItems()
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			string resourceName = "GW2_Legendaries.Resources.Data.Items.json";

			using Stream? stream = assembly.GetManifestResourceStream(resourceName);
			if (stream == null)
				return;

			using StreamReader reader = new(stream);

			List<Item>? items = JsonConvert.DeserializeObject<List<Item>>(reader.ReadToEnd());
			if (items != null)
			{
				m_Items.AddRange(items);
			}
		}

		public static Item? GetItemWithID(int ID)
		{
			return m_Items.Find(x => x.ID == ID);
		}

		public static List<Item>? GetItems(string type)
		{
			if (m_Items.Count == 0)
				DeserializeItems();

			return type != string.Empty ?
					m_Items.Where(item => item.Type == type).ToList() :
					m_Items;
		}
	}
}
