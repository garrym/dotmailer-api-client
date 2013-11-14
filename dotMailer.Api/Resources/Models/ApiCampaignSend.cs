using System;
using System.Collections.Generic;
using dotMailer.Api.Resources.Enums;

namespace dotMailer.Api.Resources.Models
{
	public class ApiCampaignSend
	{
		public Guid Id
		{ get; set; }

		public int CampaignId
		{ get; set; }

		public List<int> AddressBookIds
		{ get; set; }

		public List<int> ContactIds
		{ get; set; }

		public DateTime SendDate
		{ get; set; }

		public ApiSplitTestSendOptions SplitTestOptions
		{ get; set; }

		public ApiCampaignSendStatuses Status
		{ get; set; }
	}
}
