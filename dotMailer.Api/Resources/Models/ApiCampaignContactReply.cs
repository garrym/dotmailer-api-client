using System;
using System.Collections.Generic;
using dotMailer.Api.Resources.Enums;

namespace dotMailer.Api.Resources.Models
{
	public class ApiCampaignContactReply
	{
		public int ContactId
		{ get; set; }

		public string Email
		{ get; set; }

		public string FromAddress
		{ get; set; }

		public string ToAddress
		{ get; set; }

		public string Subject
		{ get; set; }

		public string Message
		{ get; set; }

		public bool IsHtml
		{ get; set; }

		public DateTime DateReplied
		{ get; set; }

		public ApiCampaignReplyTypes ReplyType
		{ get; set; }
	}
}
