using System;
using dotMailer.Api.Resources.Enums;

namespace dotMailer.Api.Resources.Models
{
	public class ApiTransactionalDataImport
	{
		public Guid Id
		{ get; set; }

		public ApiTransactionalDataImportStatuses Status
		{ get; set; }
	}
}
