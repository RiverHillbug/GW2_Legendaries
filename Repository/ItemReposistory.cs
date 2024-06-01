#define LOCAL

using GW2_Legendaries.Model;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Reflection.PortableExecutable;


namespace GW2_Legendaries.Repository
{
	public class ItemRepository
	{
		private static Mutex m_Mutex = new Mutex();
		private static readonly List<Item> m_Items = [];

		private static async Task DeserializeItems()
		{
#if LOCAL
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
#else
			using HttpClient client = new();
			const string itemIDsEndpoint = "https://api.guildwars2.com/v2/legendaryarmory";  // We get a list of the IDs of legendary items here
			const string itemsEndpoint = "https://api.guildwars2.com/v2/items/";  // Add the item ID at the end to get details

			try
			{
				var response = await client.GetAsync(itemIDsEndpoint);

				if (!response.IsSuccessStatusCode)
					throw new HttpRequestException(response.ReasonPhrase);

				string itemIDsJson = await response.Content.ReadAsStringAsync();

				List<int>? IDs = JsonConvert.DeserializeObject<List<int>>(itemIDsJson);
				List<Item>? items = [];

				Object listLock = new();

				Parallel.ForEach(IDs, id =>
				{
					response = client.GetAsync(itemsEndpoint + id).Result;

					if (!response.IsSuccessStatusCode)
						throw new HttpRequestException(response.ReasonPhrase);

					string itemJson = response.Content.ReadAsStringAsync().Result;

					Item? item = JsonConvert.DeserializeObject<Item>(itemJson);

					if (item != null)
						items.Add(item);

					/*lock (listLock)
					{
						if (item != null)
							items.Add(item);
					}*/

					/*m_Mutex.WaitOne();
					if (item != null)
						items.Add(item);
					m_Mutex.ReleaseMutex();*/
				});

				if (items != null)
				{
					m_Items.AddRange(items);
				}

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
#endif
		}

		public static Item? GetItemWithID(int ID)
		{
			return m_Items.Find(x => x.ID == ID);
		}

		public static async Task<List<Item>>? GetItemsAsync(string type)
		{
			if (m_Items.Count == 0)
				await DeserializeItems();

			if (type == "Misc")
			{
				List<Item>? list = [];
				list.AddRange(m_Items.Where(item => item.Type == "rune").ToList());
				list.AddRange(m_Items.Where(item => item.Type == "sigil").ToList());

				return list;
			}

			return type != string.Empty ?
					m_Items.Where(item => item.Type == type).ToList() :
					m_Items;
		}
	}
}
