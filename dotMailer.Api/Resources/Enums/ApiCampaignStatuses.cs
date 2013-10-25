namespace dotMailer.Api.Resources.Enums
{
	public enum ApiCampaignStatuses
	{
		Unsent,
		Sending,
		Sent,
		Paused,
		Cancelled,
		RequiresSystemApproval,
		RequiresSMSApproval,
		RequiresWorkflowApproval,
		Triggered,
		NotAvailableInThisVersion
	}
}
