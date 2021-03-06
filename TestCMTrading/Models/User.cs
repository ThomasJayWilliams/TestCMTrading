﻿using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestCMTrading.Models
{
	[Table("Users")]
	public class User
	{
		[Key]
		[JsonIgnore]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public long Id { get; set; }

		[Required]
		public string CountryIso { get; set; }

		[Required]
		[RegularExpression(Constants.REGEX_NAME,
			ErrorMessage = "Only letters allowed.")]
		public string FirstName { get; set; }

		[Required]
		[RegularExpression(Constants.REGEX_NAME,
			ErrorMessage = "Only letters allowed.")]
		public string LastName { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required]
		[Phone]
		[MinLength(6)]
		[MaxLength(20)]
		public string PhoneNumber { get; set; }

		[JsonIgnore]
		public int LeadId { get; set; }

		[JsonIgnore]
		public string CrmContactId { get; set; }
	}
}
