using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoteriasAPIConsumer.Models
{
	public class AccessConfig
	{
		[JsonProperty(PropertyName = "loteria")]
		public string Name { get; set; }
		[JsonProperty(PropertyName = "token")]
		public string Token { get; set; }
		[JsonProperty(PropertyName = "concurso")]
		public string Contest { get; set; }

		public AccessConfig(string name, string token, string contest)
		{
			Name = name;
			Token = token;
			Contest = contest;
		}

		public AccessConfig(string name, string token)
		{
			Name = name;
			Token = token;
		}
	}
}
