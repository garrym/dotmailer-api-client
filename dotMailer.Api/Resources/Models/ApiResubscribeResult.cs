using dotMailer.Api.Resources.Enums;

namespace dotMailer.Api.Resources.Models
{
	public class ApiResubscribeResult
	{
		public ApiContact Contact
		{ get; set; }

		public ApiResubscribeStatuses Status
		{ get; set; }
	}
}
