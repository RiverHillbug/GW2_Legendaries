using GW2_Legendaries.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Reflection;

namespace GW2_Legendaries.Repository
{
	public class ItemRepository
	{
		private static readonly List<Item> m_Items = new();

		private static void DeserializeItems()
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			string resourceName = "GW2_Legendaries.Resources.Data.Items.json";

			using Stream? stream = assembly.GetManifestResourceStream(resourceName);
			if (stream == null)
			{
				return;
			}

			using StreamReader reader = new(stream);
			string json = reader.ReadToEnd();

			List<Item>? items = JsonConvert.DeserializeObject<List<Item>>(json);
			if (items != null)
			{
				m_Items.AddRange(items);
			}
		}

		public static List<Item>? GetItems(string type)
		{
			if (m_Items.Count == 0)
			{
				DeserializeItems();
			}

			Assembly assembly = Assembly.GetExecutingAssembly();
			string resourceName = "GW2_Legendaries.Resources.Data.Items.json";

			using Stream? stream = assembly.GetManifestResourceStream(resourceName);
			if (stream == null)
				return null;

			using StreamReader reader = new(stream);
			string json = File.ReadAllText("D:\\Others\\School\\ToolDevelopment\\GW2_Legendaries\\Resources\\Data\\Items.json");

			if (json != null)
			{
				List<JObject>? jItems = JToken.Parse(json).ToObject<List<JObject>>();

				if (jItems == null)
					return null;

				foreach (JObject jItem in jItems)
				{
					Item? item = (Item?)jItem.ToObject(typeof(Item));

					if (item != null)
						m_Items.Add(item);
				}

				List<Item> itemsOfType = new();

				if (type == string.Empty)
					return m_Items;

				foreach (Item item in m_Items)
				{
					if (item.Type == type)
					{
						itemsOfType.Add(item);
					}
				}

				return itemsOfType;
			}

			return null;
		}
	}
}
