namespace TestCMTrading.Models
{
	public class CreateLeadResponse
	{
		public string ErrorMessage { get; set; }
		public int LeadId { get; set; }
		public string CrmContactId { get; set; }
		public bool IsSuccess { get; set; }
	}
}
