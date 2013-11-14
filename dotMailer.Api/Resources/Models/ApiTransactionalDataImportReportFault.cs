using System.Collections.Generic;
using dotMailer.Api.Resources.Enums;

namespace dotMailer.Api.Resources.Models
{
	public class ApiTransactionalDataImportReportFault
	{
		public string Key
		{ get; set; }

		public ApiTransactionalDataImportFaultReason Reason
		{ get; set; }
	}
}
