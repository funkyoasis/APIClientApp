//This allows us to access the app.config file
using System.Configuration;

namespace APITestApp
{
	public class AppConfigReader
	{
		public static readonly string BaseUrl = ConfigurationManager.AppSettings["base_url"];
	}
}
