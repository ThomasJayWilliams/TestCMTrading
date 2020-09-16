using RestSharp;
using System.Threading.Tasks;
using TestCMTrading.Models;

namespace TestCMTrading.Extensions
{
	public static class IRestClientExtensions
	{
		public static Task<IRestResponse> RegisterUserAsync(this IRestClient restClient,
			User user)
		{
			var request = new RestRequest
			{
				Resource = Constants.API_ENDPOINT_REGISTER,
				Method = Constants.API_ENDPOINT_METHOD
			};

			request.AddJsonBody(user);
			request.AddHeader("Content-Type", "application/json");
			restClient.Authenticator.Authenticate(restClient, request);

			return restClient.ExecuteAsync(request);
		}
	}
}
