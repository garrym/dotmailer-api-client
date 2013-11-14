using System.Collections.Generic;

namespace dotMailer.Api.Resources.Models
{
	public class ApiAccount
	{
		public int Id
		{ get; set; }

		public ApiAccountPropertyList Properties
		{ get; set; }
	}
}
