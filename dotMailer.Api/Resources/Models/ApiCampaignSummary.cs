using System;
using System.Collections.Generic;

namespace dotMailer.Api.Resources.Models
{
	public class ApiCampaignSummary
	{
		public DateTime DateSent
		{ get; set; }

		public int NumUniqueOpens
		{ get; set; }

		public int NumUniqueTextOpens
		{ get; set; }

		public int NumTotalUniqueOpens
		{ get; set; }

		public int NumOpens
		{ get; set; }

		public int NumTextOpens
		{ get; set; }

		public int NumTotalOpens
		{ get; set; }

		public int NumClicks
		{ get; set; }

		public int NumTextClicks
		{ get; set; }

		public int NumTotalClicks
		{ get; set; }

		public int NumPageViews
		{ get; set; }

		public int NumTotalPageViews
		{ get; set; }

		public int NumTextPageViews
		{ get; set; }

		public int NumForwards
		{ get; set; }

		public int NumTextForwards
		{ get; set; }

		public int NumEstimatedForwards
		{ get; set; }

		public int NumTextEstimatedForwards
		{ get; set; }

		public int NumTotalEstimatedForwards
		{ get; set; }

		public int NumReplies
		{ get; set; }

		public int NumTextReplies
		{ get; set; }

		public int NumTotalReplies
		{ get; set; }

		public int NumHardBounces
		{ get; set; }

		public int NumTextHardBounces
		{ get; set; }

		public int NumTotalHardBounces
		{ get; set; }

		public int NumSoftBounces
		{ get; set; }

		public int NumTextSoftBounces
		{ get; set; }

		public int NumTotalSoftBounces
		{ get; set; }

		public int NumUnsubscribes
		{ get; set; }

		public int NumTextUnsubscribes
		{ get; set; }

		public int NumTotalUnsubscribes
		{ get; set; }

		public int NumIspComplaints
		{ get; set; }

		public int NumTextIspComplaints
		{ get; set; }

		public int NumTotalIspComplaints
		{ get; set; }

		public int NumMailBlocks
		{ get; set; }

		public int NumTextMailBlocks
		{ get; set; }

		public int NumTotalMailBlocks
		{ get; set; }

		public int NumSent
		{ get; set; }

		public int NumTextSent
		{ get; set; }

		public int NumTotalSent
		{ get; set; }

		public int NumRecipientsClicked
		{ get; set; }

		public int NumDelivered
		{ get; set; }

		public int NumTextDelivered
		{ get; set; }

		public int NumTotalDelivered
		{ get; set; }

		public double PercentageDelivered
		{ get; set; }

		public double PercentageUniqueOpens
		{ get; set; }

		public double PercentageOpens
		{ get; set; }

		public double PercentageUnsubscribes
		{ get; set; }

		public double PercentageReplies
		{ get; set; }

		public double PercentageHardBounces
		{ get; set; }

		public double PercentageSoftBounces
		{ get; set; }

		public double PercentageUsersClicked
		{ get; set; }

		public double PercentageClicksToOpens
		{ get; set; }
	}
}
