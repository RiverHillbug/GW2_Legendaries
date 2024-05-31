﻿using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace GW2_Legendaries.Model
{
	public class Item
	{
		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; } = string.Empty;

		//public string Description{ get; set; } = string.Empty;

		[JsonProperty(PropertyName = "description")]
		private string m_Description = string.Empty;

		public string Description
		{
			get
			{
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

		public string ImagePath => $"../Resources/Images/{ID}.png";
	}
}
