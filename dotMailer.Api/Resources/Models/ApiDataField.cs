using dotMailer.Api.Resources.Enums;

namespace dotMailer.Api.Resources.Models
{
	public class ApiDataField
	{
		public string Name
		{ get; set; }

		public ApiDataTypes Type
		{ get; set; }

		public ApiDataFieldVisibility Visibility
		{ get; set; }
	}
}
