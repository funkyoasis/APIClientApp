using NUnit.Framework;
using APITestApp.Services;
using System.Threading.Tasks;

namespace APITestApp.Tests
{
	public class WhenTheBulkPostcodeServiceIsCalled_WithValidPostcodes
	{
		private BulkPostcodeService _bulkPostcodeService;
		[OneTimeSetUp]
		public async Task OneTimeSetUpAsync()
		{
			_bulkPostcodeService = new BulkPostcodeService();
			await _bulkPostcodeService.MakeRequestAsync(new string[]{ "OX49 5NU", "M32 0JG", "NE30 1DP"});
		}
		[Test]

		public void Statusis200()
		{
			Assert.That(_bulkPostcodeService.ResponseContent["status"].ToString(), Is.EqualTo("200"));
		}
	}
}
