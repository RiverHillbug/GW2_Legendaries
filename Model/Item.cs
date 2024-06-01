using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace GW2_Legendaries.Model
{
	public class Item
	{
		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; } = string.Empty;

		[JsonProperty(PropertyName = "description")]
		private string m_Description = string.Empty;

		public string Description
		{
			get
			{
				if (m_Description == string.Empty)
					return "This very cool item does not have a description available.";

				return Regex.Replace(m_Description, "<.*?>", string.Empty);
			}
			set { m_Description = value; }
		}

		[JsonProperty(PropertyName = "type")]
		public string Type { get; set; } = string.Empty;

		[JsonProperty(PropertyName = "id")]
		public int ID { get; set; } = 0;

		[JsonProperty(PropertyName = "icon")]
		public string Icon { get; set; } = string.Empty;

		public string ImagePath => $"../Resources/Images/{ID}.png"; // The API does not have preview pictures so the only way to show those is manually making a list/folder

		[JsonProperty(PropertyName = "details")]
		public ItemDetails Details { get; set; } = new();
	}

	public class ItemDetails
	{
		[JsonProperty(PropertyName = "type")]
		public string Type { get; set; } = string.Empty;

		[JsonProperty(PropertyName = "weight_class")]
		public string WeightClass { get; set; } = string.Empty;
	}
}
