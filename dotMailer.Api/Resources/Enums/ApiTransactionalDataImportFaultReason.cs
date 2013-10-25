namespace dotMailer.Api.Resources.Enums
{
	public enum ApiTransactionalDataImportFaultReason
	{
		InvalidClientKey,
		InvalidContactIdentifier,
		InvalidJson,
		DuplicateKey,
		ContactIdDoesNotExist,
		ContactEmailDoesNotExist,
		NotAvailableInThisVersion
	}
}
