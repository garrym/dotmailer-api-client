using System;
using System.Collections.Generic;

namespace dotMailer.Api.Resources.Models
{
	public class ApiCampaignContactClick
	{
		public int ContactId
		{ get; set; }

		public string Email
		{ get; set; }

		public string Url
		{ get; set; }

		public string IpAddress
		{ get; set; }

		public string UserAgent
		{ get; set; }

		public DateTime DateClicked
		{ get; set; }

		public string Keyword
		{ get; set; }
	}
}
