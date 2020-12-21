using LoteriasAPIConsumer.Models;
using LoteriasAPIConsumer.Services;
using System;
using System.Linq;
using System.Collections.Generic;

namespace LoteriasAPIConsumer
{
	public class Program
	{
		static void Main(string[] args)
		{
			var results = GetLatestResults();
			var frequencyOfValues = AnalizeResults(results);
		}

		static IEnumerable<Response> GetLatestResults()
		{
			var loteriasApiConfig = new LoteriasAPIConfig("https://apiloterias.com.br/");
			var accessConfig = new AccessConfig("megasena", "yourTokenHere"); 
			var apiConsumer = new APIConsumer(accessConfig, loteriasApiConfig);
			var start = 2275; 
			var end = 2329;
			var result = apiConsumer.GetResults(start, end);
			return result;
		}

		static IOrderedEnumerable<KeyValuePair<string, long>> AnalizeResults(IEnumerable<Response> results)
		{
			var frequencyOfValues = new Dictionary<string, long>();
			foreach (var result in results)
			{
				foreach (var value in result.Numbers)
				{
					if (frequencyOfValues.ContainsKey(value))
						frequencyOfValues[value]++;
					else
						frequencyOfValues[value] = 1;
				}
			}

			var sortedDict = from entry in frequencyOfValues orderby frequencyOfValues.Values descending select entry;
			return sortedDict;
		}
	}
}
