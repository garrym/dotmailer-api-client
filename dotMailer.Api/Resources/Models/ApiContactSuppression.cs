using System;
using dotMailer.Api.Resources.Enums;

namespace dotMailer.Api.Resources.Models
{
	public class ApiContactSuppression
	{
		public ApiContact SuppressedContact
		{ get; set; }

		public DateTime DateRemoved
		{ get; set; }

		public ApiContactStatuses Reason
		{ get; set; }
	}
}
