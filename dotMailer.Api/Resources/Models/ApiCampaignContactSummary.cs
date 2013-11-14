using System;
using System.Collections.Generic;

namespace dotMailer.Api.Resources.Models
{
	public class ApiCampaignContactSummary
	{
		public int ContactId
		{ get; set; }

		public string Email
		{ get; set; }

		public int NumOpens
		{ get; set; }

		public int NumPageViews
		{ get; set; }

		public int NumClicks
		{ get; set; }

		public int NumForwards
		{ get; set; }

		public int NumEstimatedForwards
		{ get; set; }

		public int NumReplies
		{ get; set; }

		public DateTime DateSent
		{ get; set; }

		public DateTime DateFirstOpened
		{ get; set; }

		public DateTime DateLastOpened
		{ get; set; }

		public string FirstOpenIp
		{ get; set; }

		public bool Unsubscribed
		{ get; set; }

		public bool SoftBounced
		{ get; set; }

		public bool HardBounced
		{ get; set; }
	}
}
