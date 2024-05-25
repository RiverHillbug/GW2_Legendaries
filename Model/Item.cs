using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace GW2_Legendaries.Model
{
	public class Item
	{
		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; } = string.Empty;

		[JsonProperty(PropertyName = "description")]
		public string Description { get; set; } = string.Empty;

		[JsonProperty(PropertyName = "type")]
		public string Type { get; set; } = string.Empty;

		[JsonProperty(PropertyName = "id")]
		public int ID { get; set; } = 0;

		[JsonProperty(PropertyName = "icon")]
		public string Icon { get; set; } = string.Empty;
	}
}
