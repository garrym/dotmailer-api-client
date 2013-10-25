using dotMailer.Api.Resources.Enums;

namespace dotMailer.Api.Resources.Models
{
	public class ApiTemplate
	{
		public int Id
		{ get; set; }

		public string Name
		{ get; set; }

		public string Subject
		{ get; set; }

		public string FromName
		{ get; set; }

		public string HtmlContent
		{ get; set; }

		public string PlainTextContent
		{ get; set; }

		public ApiCampaignReplyActions ReplyAction
		{ get; set; }

		public string ReplyToAddress
		{ get; set; }
	}
}
