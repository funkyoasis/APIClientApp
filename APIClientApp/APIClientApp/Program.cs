using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace APIClientApp
{
	class Program
	{
		static void Main(string[] args)
		{
			////SET UP SINGLE POSTCODE REQUEST //////////
			//Client property which is equal to a new "ResetScharp", create an api project
			var restClient = new RestClient(@"Https://api.postcodes.io/");
				//set up the request
			var restRequest = new RestRequest();
			//set method as GET
			restRequest.Method = Method.GET;
			restRequest.AddHeader("Content-Type", "application/json");
			//Set Time Out
			restRequest.Timeout = -1;
			var postcode = "EC2Y 5AS";
			//Define request resource path
			restRequest.Resource = $"postcodes/{postcode.ToLower().Replace(" ", "")}";
			/////EXERCUTE REQUEST//////	
			var singlePostcodeResponse = restClient.Execute(restRequest);
			Console.WriteLine("Response Content as string");
			Console.WriteLine(singlePostcodeResponse.Content);

			///SET UP BULK POSTCODE REQUEST ///
			var client = new RestClient(@"HTTPS://api.postcodes.io/postcodes/");
			client.Timeout = -1;
			var request = new RestRequest(Method.POST);
			request.AddHeader("Content-Type", "application/json");
			JObject postcodes = new JObject
			{
				new JProperty("postcodes", new JArray(new string[]{"OX49 5NU", "M32 0JG", "NE30 1DP"}))
			};
			request.AddParameter("application/json", postcodes, ParameterType.RequestBody);
			IRestResponse bulkPostcodeResponse = client.Execute(request);
			//Console.WriteLine(bulkPostcodeResponse.Content);

			Console.WriteLine($"Status Code:{(int)bulkPostcodeResponse.StatusCode}");


			///Creating your onw JObject
			/*
			var course = new JObject
			{
				new JProperty("name","eng91"),
				new JProperty("trainees", new JArray(new string[]{"Ringo","Paul","Geroge","Jhon" })),
				new JProperty("Total",4)
			}
			*/

			////QUERY OUR RESPONSE AS A JOBJECT /////
			var bulJsonResponse = JObject.Parse(bulkPostcodeResponse.Content);
			var singleJsonResponse = JObject.Parse(singlePostcodeResponse.Content);
			//Console.WriteLine(singleJsonResponse["status"]);
			//Console.WriteLine(singleJsonResponse["result"]["country"]);
			//Console.WriteLine(singleJsonResponse["result"]["codes"]["parish"]);
			//Console.WriteLine(singleJsonResponse["result"][1]["result"]["country"]);

			var singlePostcode = JsonConvert.DeserializeObject<SinglePostcodeResponse>(singlePostcodeResponse.Content);
			var bulkPostcode = JsonConvert.DeserializeObject<BulkPostcodeResponse>(bulkPostcodeResponse.Content);

			//Console.WriteLine(singlePostcode.result.country);
			//Console.WriteLine(singlePostcode.result.codes.admin_county);

			foreach (var result in bulkPostcode.result)
			{
				Console.WriteLine(result.query);
				Console.WriteLine(result.postcode.region);
			}

			//var result2 = bulkPostcode.result.Where(p => p.query == "OX49 5NU").Select(p => p.postcode.parish).FirstOrDefault();
			
			
			
			////SET UP OUTCODE REQUEST/////
			var outcodeRestRequest = new RestRequest();
			//set method as GET
			outcodeRestRequest.Method = Method.GET;
			outcodeRestRequest.AddHeader("Content-Type", "application/json");
			//Set Time Out
			outcodeRestRequest.Timeout = -1;
			var outcode = "WV1";
			//Define request resource path
			outcodeRestRequest.Resource = $"outcodes/{outcode.ToLower()}";
			/////EXERCUTE REQUEST//////	
			var singleOutcodeResponse = restClient.Execute(outcodeRestRequest);
			Console.WriteLine("Response Content as string");
			Console.WriteLine(singleOutcodeResponse.Content);

			//QUERY AS JOBJECT
			var OutcodeJsonResponse = JObject.Parse(singleOutcodeResponse.Content);
			Console.WriteLine(OutcodeJsonResponse["result"]["admin_ward"]);
			var adminDistrict = OutcodeJsonResponse["result"]["admin_district"];
			Console.WriteLine(adminDistrict.Type);
			//Console.WriteLine(OutcodeJsonResponse["result"]["admin_district"]);
			// QUERY AS C# OBJECT
			var singleOutcode = JsonConvert.DeserializeObject<OutcodeResponse>(singleOutcodeResponse.Content);
			/*
			Console.WriteLine(singleOutcode.result.country[0]);

			foreach (var result in singleOutcode.result.admin_ward)
			{
				Console.WriteLine(result);
			}
			*/
		}
	}
}
