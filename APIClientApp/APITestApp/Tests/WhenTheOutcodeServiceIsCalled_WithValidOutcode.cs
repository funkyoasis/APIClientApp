using System;
using NUnit.Framework;
using APITestApp.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
namespace APITestApp.Tests
{
	class WhenTheOutcodeServiceIsCalled_WithValidOutcode
	{
		private OutcodeService _outcodeService;
		[OneTimeSetUp]

		public async Task OneTimeSetUpAsync()
		{
			_outcodeService = new OutcodeService();
			await _outcodeService.MakeRequestAsync("WV1");
		}

		[Test]
		public void StatusIs200()
		{
			Assert.That(_outcodeService.ResponseContent["status"].ToString(), Is.EqualTo("200"));
		}

		[Test]
		public void StatusIs200_Alt()
		{
			Assert.That(_outcodeService.StatusCode, Is.EqualTo(200));
		}

		[Test]
		public void CorrectOutcodeIsReturned()
		{
			var result = _outcodeService.ResponseContent["result"]["outcode"].ToString();
			Assert.That(result, Is.EqualTo("WV1"));
		}

		[Test]
		public void ObjectStatusIs200()
		{
			var result = _outcodeService.ResponseObject.status;
			Assert.That(result, Is.EqualTo(200));
		}
		[Test]
		public void admin_ward__IsCorrectLength()
		{
			var admin_wardtLenth = _outcodeService.ResponseObject.result.admin_ward.Length;
			Assert.That(admin_wardtLenth, Is.EqualTo(5));
		}
	}
}
