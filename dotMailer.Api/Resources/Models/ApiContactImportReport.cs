namespace dotMailer.Api.Resources.Models
{
	public class ApiContactImportReport
	{
		public int NewContacts
		{ get; set; }

		public int UpdatedContacts
		{ get; set; }

		public int GloballySuppressed
		{ get; set; }

		public int InvalidEntries
		{ get; set; }

		public int DuplicateEmails
		{ get; set; }

		public int Blocked
		{ get; set; }

		public int Unsubscribed
		{ get; set; }

		public int HardBounced
		{ get; set; }

		public int SoftBounced
		{ get; set; }

		public int IspComplaints
		{ get; set; }

		public int MailBlocked
		{ get; set; }

		public int DomainSuppressed
		{ get; set; }

		public int PendingDoubleOptin
		{ get; set; }

		public int Failures
		{ get; set; }
	}
}
