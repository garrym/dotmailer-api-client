using dotMailer.Api.Resources.Enums;

namespace dotMailer.Api.Resources.Models
{
	public class ApiSegmentRefresh
	{
		public int Id
		{ get; set; }

		public ApiSegmentRefreshStatuses Status
		{ get; set; }
	}
}
