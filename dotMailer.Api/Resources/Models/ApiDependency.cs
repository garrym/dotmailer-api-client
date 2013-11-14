using System.Collections.Generic;
using dotMailer.Api.Resources.Enums;

namespace dotMailer.Api.Resources.Models
{
	public class ApiDependency
	{
		public ApiBusinessObjectType Type
		{ get; set; }

		public int ObjectId
		{ get; set; }
	}
}
