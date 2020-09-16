using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestCMTrading.Infrastructure.Interfaces;
using TestCMTrading.Models;

namespace TestCMTrading.Controllers
{
	public class HomeController : Controller
	{
		private readonly IIdentityService _identityService;

		public HomeController(IIdentityService restClient)
		{
			_identityService = restClient;
		}

		public IActionResult Index() =>
			View();

		public IActionResult SignIn() =>
			View();

		public IActionResult SignInPopup() =>
			View();

		[HttpPost]
		public async Task<IActionResult> RegisterUser(User user)
		{
			
			if (!ModelState.IsValid)
			{
				ViewBag.RegistrationStatus = false;
				ViewBag.RegistrationMessage = "Failed.";

				foreach (var modelState in ModelState.Values)
					foreach (var error in modelState.Errors)
						ViewBag.RegistrationMessage += $"</br>{error.ErrorMessage}";

				return View("Index");
			}

			await InvokeAndHandleAsync(_identityService.RegisterUser(user),
				() =>
				{
					ViewBag.RegistrationStatus = true;
					ViewBag.RegistrationMessage = "Successfully registered user.";
				},
				ex =>
				{
					ViewBag.RegistrationStatus = false;
					ViewBag.RegistrationMessage = $"Failed. {ex.Message}.";
				});

			return View("Index");
		}

		[HttpPost]
		public async Task<IActionResult> SignIn(Login login)
		{
			if (!ViewData.ModelState.IsValid)
			{
				ViewBag.SignInStatus = false;
				ViewBag.SignInMessage = "Failed.";

				foreach (var modelState in ModelState.Values)
					foreach (var error in modelState.Errors)
						ViewBag.SignInMessage += $"</br>{error.ErrorMessage}";

				return View("SignIn");
			}
			await InvokeAndHandleAsync(_identityService.SignIn(login),
				() =>
				{
					ViewBag.SignInStatus = true;
					ViewBag.SignInMessage = "Successfully signed in.";
				},
				ex =>
				{
					ViewBag.SignInStatus = false;
					ViewBag.SignInMessage = $"Failed. {ex.Message}.";
				});

			return View("SignIn");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		private async Task InvokeAndHandleAsync(Task task, Action successAction,
			Action<Exception> failAction, string message = "Success")
		{
			try
			{
				await task;
			}
			catch (Exception ex)
			{
				failAction(ex);
				return;
			}

			successAction();
		}
	}
}
