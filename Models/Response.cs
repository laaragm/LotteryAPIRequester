using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoteriasAPIConsumer.Models
{
	public class Response
	{
		[JsonProperty(PropertyName = "nome")]
		public string Name { get; set; }
		[JsonProperty(PropertyName = "numero_concurso")]
		public long ContestNumber { get; set; }
		[JsonProperty(PropertyName = "data_concurso")]
		public DateTime ContestDate { get; set; }
		[JsonProperty(PropertyName = "acumulou")]
		public bool Accumulated { get; set; }
		[JsonProperty(PropertyName = "dezenas")]
		public IEnumerable<string> Numbers { get; set; }

	}
}
