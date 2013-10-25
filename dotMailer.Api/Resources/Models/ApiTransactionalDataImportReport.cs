namespace dotMailer.Api.Resources.Models
{
	public class ApiTransactionalDataImportReport
	{
		public int TotalItems
		{ get; set; }

		public int TotalImported
		{ get; set; }

		public int TotalRejected
		{ get; set; }

		public ApiTransactionalDataImportReportFaultList Faults
		{ get; set; }
	}
}
