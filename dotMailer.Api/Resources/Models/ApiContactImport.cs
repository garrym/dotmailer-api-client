using System;
using System.Collections.Generic;
using dotMailer.Api.Resources.Enums;

namespace dotMailer.Api.Resources.Models
{
	public class ApiContactImport
	{
		public Guid Id
		{ get; set; }

		public ApiContactImportStatuses Status
		{ get; set; }
	}
}
