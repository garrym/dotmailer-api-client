using System.Collections.Generic;
using dotMailer.Api.Resources.Enums;

namespace dotMailer.Api.Resources.Models
{
	public class ApiSplitTestSendOptions
	{
		public ApiSplitTestMetrics TestMetric
		{ get; set; }

		public int TestPercentage
		{ get; set; }

		public int TestPeriodHours
		{ get; set; }
	}
}
