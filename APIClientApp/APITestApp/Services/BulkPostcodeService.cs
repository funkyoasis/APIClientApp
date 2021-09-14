using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace APITestApp.Services
{
    public class BulkPostcodeService
    {
        #region Properties
        // RestSharp Object which handles comms with the API
        public RestClient Client;
        // A newtonsoft object representing the json response
        public JObject ResponseContent { get; set; }
        // the postcode used in this API request
        public string[] PostcodesSelected { get; set; }
        // store the status code
        public int StatusCode { get; set; }
        // an object model of the response
        public BulkPostcodeResponse ResponseObject { get; set; }
        #endregion

        // Constructor - creates the restclient object
        public BulkPostcodeService()
        {
            Client = new RestClient { BaseUrl = new Uri(AppConfigReader.BaseUrl) };
        }

        public async Task MakeRequestAsync(string[] inputPostcodes)
        {
            // Setup the request
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            PostcodesSelected = inputPostcodes;

            // Make the request
            JObject postcodes = new JObject
            {
                new JProperty("postcodes", new JArray(PostcodesSelected))
            };

            request.AddParameter("application/json", postcodes, ParameterType.RequestBody);
            IRestResponse response = await Client.ExecuteAsync(request);
            // Parse JSON in response content
            ResponseContent = JObject.Parse(response.Content);

            // Capture status code
            StatusCode = (int)response.StatusCode;

            // Parse thje JSON string into an object tree
            ResponseObject = JsonConvert.DeserializeObject<BulkPostcodeResponse>(response.Content);
        }
    }
}
