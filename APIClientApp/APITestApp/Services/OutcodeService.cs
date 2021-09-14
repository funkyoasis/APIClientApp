using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;


namespace APITestApp.Services
{
	public class OutcodeService
	{
        // RestSharp Object which handles comms with the API
        public RestClient Client;
        // A newtonsoft object representing the json response
        public JObject ResponseContent { get; set; }
        // the postcode used in this API request
        public string OutcodeSelected { get; set; }
        // store the status code
        public int StatusCode { get; set; }
        // an object model of the response
        public OutcodeResponse ResponseObject { get; set; }

        public OutcodeService()
        {
            Client = new RestClient { BaseUrl = new Uri(AppConfigReader.BaseUrl) };
        }


        public async Task MakeRequestAsync(string outcode)
        {
            // Setup the request
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            OutcodeSelected = outcode;

            // Define the request resource path
            request.Resource = $"outcodes/{outcode.ToLower()}";

            // Make the request
            IRestResponse response = await Client.ExecuteAsync(request);

            // Parse JSON in response content
            ResponseContent = JObject.Parse(response.Content);

            // Capture status code
            StatusCode = (int)response.StatusCode;

            // Parse thje JSON string into an object tree
            ResponseObject = JsonConvert.DeserializeObject<OutcodeResponse>(response.Content);
        }
    }
}
