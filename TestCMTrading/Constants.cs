using RestSharp;

namespace TestCMTrading
{
	public static class Constants
	{
		public const string API_CFG_USERNAME = "Username";
		public const string API_CFG_SECTION = "Api";
		public const string API_CFG_PASSWORD = "Password";
		public const string API_CFG_URL = "Url";
		public const string API_ENDPOINT_REGISTER = "/CreateLead";
		public const Method API_ENDPOINT_METHOD = Method.POST;

		public const string REGEX_NAME = "^([a-zA-Z]{2,})?";
	}
}
