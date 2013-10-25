using System;
using dotMailer.Api.Resources.Enums;

namespace dotMailer.Api.Resources.Models
{
	public class ApiCampaignSend
	{
		public Guid Id
		{ get; set; }

		public int CampaignId
		{ get; set; }

		public Int32List AddressBookIds
		{ get; set; }

		public Int32List ContactIds
		{ get; set; }

		public DateTime SendDate
		{ get; set; }

		public ApiSplitTestSendOptions SplitTestOptions
		{ get; set; }

		public ApiCampaignSendStatuses Status
		{ get; set; }
	}
}
