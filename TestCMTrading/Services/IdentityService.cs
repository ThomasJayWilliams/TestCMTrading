using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestSharp;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestCMTrading.Extensions;
using TestCMTrading.Infrastructure.Interfaces;
using TestCMTrading.Models;

namespace TestCMTrading.Services
{
	public class IdentityService : IIdentityService
	{
		private readonly IRestClient _cmTradingClient;

		public IdentityService(IRestClient restClient)
		{
			_cmTradingClient = restClient;
		}

		[HttpPost]
		public async Task RegisterUser(User user)
		{
			var apiResponse = await _cmTradingClient
				.RegisterUserAsync(user);

			if (apiResponse.StatusCode != HttpStatusCode.OK)
				throw new InvalidOperationException();

			var leadXml = XDocument.Parse(apiResponse.Content);
			var children = leadXml.Descendants()
				.ToList();

			if (!Convert.ToBoolean(children.FirstOrDefault(n => n.Name.LocalName == "IsSuccess").Value))
				throw new InvalidOperationException(
					children.FirstOrDefault(n => n.Name.LocalName == "ErrorMessage").Value);

			user.CrmContactId = children.FirstOrDefault(
				n => n.Name.LocalName == "CrmContactId").Value;
			user.LeadId = Convert.ToInt32(
				children.FirstOrDefault(n => n.Name.LocalName == "LeadId").Value);

			using var context = new DataContext();

			await context.Users.AddAsync(user);
			await context.SaveChangesAsync();
		}

		[HttpPost]
		public async Task SignIn(Login login)
		{
			using var context = new DataContext();

			var user = await context.Users
				.FirstOrDefaultAsync(u => u.Email == login.Email);

			if (user == null)
				throw new InvalidOperationException("User with such email does not exist.");

			if (string.CompareOrdinal(user.Password, login.Password) == 0)
				throw new InvalidOperationException("Incorrect password.");
		}
	}
}
