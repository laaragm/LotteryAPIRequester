using LoteriasAPIConsumer.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using RestSharp;
using System.Threading;

namespace LoteriasAPIConsumer.Services
{
	public class APIConsumer
	{
		public Response Response { get; set; }
		public AccessConfig AccessConfig { get; set; }
		public LoteriasAPIConfig LoteriasAPIConfig { get; set; }

		public APIConsumer(AccessConfig accessConfig, LoteriasAPIConfig loteriasAPIConfig)
		{
			AccessConfig = accessConfig;
			LoteriasAPIConfig = loteriasAPIConfig;
		}

		public IEnumerable<Response> GetResults(IEnumerable<long> contestNumbers)
		{
			IList<Response> results = new List<Response>();
			foreach (var contestNumber in contestNumbers)
			{
				var response = GetResults(contestNumber);
				results.Add(response);
			}
			return results;
		}

		public IEnumerable<Response> GetResults(long startContestNumber, long endContestNumber)
		{
			IList<Response> results = new List<Response>();
			while (startContestNumber <= endContestNumber)
			{
				var response = GetResults(startContestNumber);
				results.Add(response);
				startContestNumber++;
				Thread.Sleep(2000);
			}
			return results;
		}

		public Response GetResults(long contestNumber)
		{
			var queryParams = string.Join("&", GetDetailParameters(contestNumber).Select(p => $"{p.Key}={p.Value}"));
			var endpoint = $"app/resultado?{queryParams}";
			var client = new RestClient(@$"{LoteriasAPIConfig.BaseAddress}{endpoint}");
			client.Timeout = -1;
			var request = new RestRequest(Method.GET);
			IRestResponse response = client.Execute(request);
			return JsonConvert.DeserializeObject<Response>(response.Content);
		}

		public Response GetLatestResult()
		{
			var queryParams = string.Join("&", GetDetailParameters().Select(p => $"{p.Key}={p.Value}"));
			var endpoint = $"app/resultado?{queryParams}";
			var client = new RestClient(@$"{LoteriasAPIConfig.BaseAddress}{endpoint}");
			client.Timeout = -1;
			var request = new RestRequest(Method.GET);
			IRestResponse response = client.Execute(request);
			return JsonConvert.DeserializeObject<Response>(response.Content);
		}

		private IDictionary<string, object> GetDetailParameters()
		{
			return new Dictionary<string, object> {
				{ "loteria", AccessConfig.Name },
				{ "token", AccessConfig.Token }
			};
		}

		private IDictionary<string, object> GetDetailParameters(long contestNumber)
		{
			return new Dictionary<string, object> {
				{ "loteria", AccessConfig.Name },
				{ "token", AccessConfig.Token },
				{ "concurso", contestNumber }
			};
		}

	}
}
