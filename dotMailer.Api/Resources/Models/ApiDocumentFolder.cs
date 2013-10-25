namespace dotMailer.Api.Resources.Models
{
	public class ApiDocumentFolder
	{
		public int Id
		{ get; set; }

		public string Name
		{ get; set; }

		public ApiDocumentFolderList ChildFolders
		{ get; set; }
	}
}
