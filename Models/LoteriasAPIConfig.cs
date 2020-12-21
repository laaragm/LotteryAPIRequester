using System;
using System.Collections.Generic;
using System.Text;

namespace LoteriasAPIConsumer.Models
{
	public class LoteriasAPIConfig
	{
		public string BaseAddress { get; set; }

		public LoteriasAPIConfig(string baseAddress)
		{
			BaseAddress = baseAddress;
		}
	}
}
