namespace dotMailer.Api.Resources.Enums
{
	public enum ApiContactImportStatuses
	{
		Finished,
		NotFinished,
		RejectedByWatchdog,
		InvalidFileFormat,
		Unknown,
		Failed,
		ExceedsAllowedContactLimit,
		NotAvailableInThisVersion
	}
}
