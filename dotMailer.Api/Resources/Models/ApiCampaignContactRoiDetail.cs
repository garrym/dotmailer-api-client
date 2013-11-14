using System;
using System.Collections.Generic;
using dotMailer.Api.Resources.Enums;

namespace dotMailer.Api.Resources.Models
{
	public class ApiCampaignContactRoiDetail
	{
		public int ContactId
		{ get; set; }

		public string Email
		{ get; set; }

		public string MarkerName
		{ get; set; }

		public ApiRoiDetailDataTypes DataType
		{ get; set; }

		public DateTime DateEntered
		{ get; set; }
	}
}
