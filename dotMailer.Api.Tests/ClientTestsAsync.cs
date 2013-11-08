using System;
using System.Linq;
using dotMailer.Api.Resources.Models;
using NUnit.Framework;

namespace dotMailer.Api.Tests
{
    [TestFixture]
    public class ClientTestsAsync : ClientTestBase
    {
        #region Delete Methods

        [Test]
        public void Ensure_DeleteAddressBookAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.DeleteAddressBookAsync(sampleAddressBookId).Result);
        }

        [Test]
        public void Ensure_DeleteAddressBookContactAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.DeleteAddressBookContactAsync(sampleAddressBookId, sampleContactId).Result);
        }

        [Test]
        public void Ensure_DeleteAddressBookContactsAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.DeleteAddressBookContactsAsync(sampleAddressBookId).Result);
        }

        [Test]
        public void Ensure_DeleteAddressBookContactsInbulkAsync_Works()
        {
            var client = GetClient();
            var contacts = client.GetAddressBookContacts(sampleAddressBookId).Data;
            var contactIds = new Int32List();
            contactIds.AddRange(contacts.Select(x => x.Id));

            AssertResult(() => client.DeleteAddressBookContactsInbulkAsync(sampleAddressBookId, contactIds).Result);
        }

        [Test]
        public void Ensure_DeleteCampaignAttachmentAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.DeleteCampaignAttachmentAsync(sampleCampaignId, sampleDocumentId).Result);
        }

        [Test]
        public void Ensure_DeleteContactAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.DeleteContactAsync(sampleContactId).Result);
        }

        [Test]
        public void Ensure_DeleteContactsTransactionalDataAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.DeleteContactsTransactionalDataAsync(sampleTransactionalDataCollectionName, sampleTransactionalDataKey).Result);
        }

        [Test]
        public void Ensure_DeleteContactTransactionalDataAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.DeleteContactTransactionalDataAsync(sampleTransactionalDataCollectionName, sampleContactId).Result);
        }

        [Test]
        public void Ensure_DeleteDataFieldAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.DeleteDataFieldAsync(sampleDataFieldName).Result);
        }

        #endregion

        #region Get Methods

        [Test]
        public void Ensure_GetAccountInfoAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetAccountInfoAsync().Result);
        }

        [Test]
        public void Ensure_GetAddressBookByIdAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetAddressBookByIdAsync(sampleAddressBookId).Result);
        }

        [Test]
        public void Ensure_GetAddressBookCampaignsAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetAddressBookCampaignsAsync(sampleAddressBookId).Result);
        }

        [Test]
        public void Ensure_GetAddressBookContactsAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetAddressBookContactsAsync(sampleAddressBookId).Result);
        }

        [Test]
        public void Ensure_GetAddressBookContactsModifiedSinceDateAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetAddressBookContactsModifiedSinceDateAsync(sampleAddressBookId, sampleSinceDate).Result);
        }

        [Test]
        public void Ensure_GetAddressBookContactsUnsubscribedSinceDateAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetAddressBookContactsUnsubscribedSinceDateAsync(sampleAddressBookId, sampleSinceDate).Result);
        }

        [Test]
        public void Ensure_GetAddressBooksAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetAddressBooksAsync(sampleAddressBookId).Result);
        }

        [Test]
        public void Ensure_GetAddressBooksPrivateAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetAddressBooksPrivateAsync().Result);
        }

        [Test]
        public void Ensure_GetAddressBooksPublicAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetAddressBooksPublicAsync().Result);
        }

        [Test]
        public void Ensure_GetCampaignActivitiesAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetCampaignActivitiesAsync(sampleCampaignId).Result);
        }

        [Test]
        public void Ensure_GetCampaignActivitiesSinceDateByDateAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetCampaignActivitiesSinceDateByDateAsync(sampleCampaignId, sampleSinceDate).Result);
        }

        [Test]
        public void Ensure_GetCampaignActivityByContactIdAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetCampaignActivityByContactIdAsync(sampleCampaignId, sampleContactId).Result);
        }

        [Test]
        public void Ensure_GetCampaignActivityClicksAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetCampaignActivityClicksAsync(sampleCampaignId, sampleContactId).Result);
        }

        [Test]
        public void Ensure_GetCampaignActivityOpensAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetCampaignActivityOpensAsync(sampleCampaignId, sampleContactId).Result);
        }

        [Test]
        public void Ensure_GetCampaignActivityPageViewsAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetCampaignActivityPageViewsAsync(sampleCampaignId, sampleContactId).Result);
        }

        [Test]
        public void Ensure_GetCampaignActivityRepliesAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetCampaignActivityRepliesAsync(sampleCampaignId, sampleContactId).Result);
        }

        [Test]
        public void Ensure_GetCampaignActivityRoiDetailsAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetCampaignActivityRoiDetailsAsync(sampleCampaignId, sampleContactId).Result);
        }

        [Test]
        public void Ensure_GetCampaignActivitySocialBookmarkViewsAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetCampaignActivitySocialBookmarkViewsAsync(sampleCampaignId, sampleContactId).Result);
        }

        [Test]
        public void Ensure_GetCampaignAddressBooksAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetCampaignAddressBooksAsync(sampleCampaignId).Result);
        }

        [Test]
        public void Ensure_GetCampaignAttachments_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetCampaignAttachments(sampleCampaignId));
        }

        [Test]
        public void Ensure_GetCampaignByIdAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetCampaignByIdAsync(sampleCampaignId).Result);
        }

        [Test]
        public void Ensure_GetCampaignClicksAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetCampaignClicksAsync(sampleCampaignId).Result);
        }

        [Test]
        public void Ensure_GetCampaignHardBouncingContactsAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetCampaignHardBouncingContactsAsync(sampleCampaignId).Result);
        }

        [Test]
        public void Ensure_GetCampaignOpensAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetCampaignOpensAsync(sampleCampaignId).Result);
        }

        [Test]
        public void Ensure_GetCampaignPageViewsSinceDateByDateAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetCampaignPageViewsSinceDateByDateAsync(sampleCampaignId, sampleSinceDate).Result);
        }

        [Test]
        public void Ensure_GetCampaignRoiDetailsSinceDateByDateAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetCampaignRoiDetailsSinceDateByDateAsync(sampleCampaignId, sampleSinceDate).Result);
        }

        [Test]
        public void Ensure_GetCampaignsAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetCampaignsAsync().Result);
        }

        [Test]
        public void Ensure_GetCampaignSocialBookmarkViewsAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetCampaignSocialBookmarkViewsAsync(sampleCampaignId).Result);
        }

        [Test]
        public void Ensure_GetCampaignsSendBySendIdAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetCampaignsSendBySendIdAsync(Guid.Empty).Result);
        }

        [Test]
        public void Ensure_GetCampaignSummaryAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetCampaignSummaryAsync(sampleCampaignId).Result);
        }

        [Test]
        public void Ensure_GetCampaignsWithActivitySinceDateAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetCampaignsWithActivitySinceDateAsync(sampleSinceDate).Result);
        }

        [Test]
        public void Ensure_GetContactAddressBooksAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetContactAddressBooksAsync(sampleContactId).Result);
        }

        [Test]
        public void Ensure_GetContactByEmailAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetContactByEmailAsync(sampleContactEmail).Result);
        }

        [Test]
        public void Ensure_GetContactByIdAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetContactByIdAsync(sampleContactId).Result);
        }

        [Test]
        public void Ensure_GetContactsAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetContactsAsync().Result);
        }

        [Test]
        public void Ensure_GetContactsCreatedSinceDateAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetContactsCreatedSinceDateAsync(sampleSinceDate).Result);
        }

        [Test]
        public void Ensure_GetContactsImportByImportIdAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetContactsImportByImportIdAsync(Guid.Empty).Result);
        }

        [Test]
        public void Ensure_GetContactsImportReportAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetContactsImportReportAsync(Guid.Empty).Result);
        }

        [Test]
        public void Ensure_GetContactsImportReportFaultsAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetContactsImportReportFaultsAsync(Guid.Empty).Result);
        }

        [Test]
        public void Ensure_GetContactsModifiedSinceDate_WorksAsync()
        {
            var client = GetClient();
            AssertResult(() => client.GetContactsModifiedSinceDateAsync(sampleSinceDate).Result);
        }

        [Test]
        public void Ensure_GetContactsSuppressedSinceDate_WorksAsync()
        {
            var client = GetClient();
            AssertResult(() => client.GetContactsSuppressedSinceDateAsync(sampleSinceDate).Result);
        }

        [Test]
        public void Ensure_GetContactsTransactionalDataByKeyAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetContactsTransactionalDataByKeyAsync(sampleTransactionalDataCollectionName, sampleTransactionalDataKey).Result);
        }

        [Test]
        public void Ensure_GetContactsTransactionalDataImportByImportIdAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetContactsTransactionalDataImportByImportIdAsync(Guid.Empty).Result);
        }

        [Test]
        public void Ensure_GetContactsTransactionalDataImportReport_WorksAsync()
        {
            var client = GetClient();
            AssertResult(() => client.GetContactsTransactionalDataImportReportAsync(Guid.Empty).Result);
        }

        [Test]
        public void Ensure_GetContactsUnsubscribedSinceDate_WorksAsync()
        {
            var client = GetClient();
            AssertResult(() => client.GetContactsUnsubscribedSinceDateAsync(sampleSinceDate).Result);
        }

        [Test]
        public void Ensure_GetContactTransactionalDataByCollectionNameAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetContactTransactionalDataByCollectionNameAsync(sampleTransactionalDataCollectionName, sampleContactEmail).Result);
        }

        [Test]
        public void Ensure_GetCustomFromAddressesAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetCustomFromAddressesAsync().Result);
        }

        [Test]
        public void Ensure_GetDataFieldsAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetDataFieldsAsync().Result);
        }

        [Test]
        public void Ensure_GetDocumentFolderDocumentsAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetDocumentFolderDocumentsAsync(1).Result);
        }

        [Test]
        public void Ensure_GetDocumentFolders_WorksAsync()
        {
            var client = GetClient();
            AssertResult(() => client.GetDocumentFoldersAsync().Result);
        }

        [Test]
        public void Ensure_GetImageFolderByIdAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetImageFolderByIdAsync(sampleImageFolderId).Result);
        }

        [Test]
        public void Ensure_GetImageFoldersAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetImageFoldersAsync().Result);
        }

        [Test]
        public void Ensure_GetSegmentsAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetSegmentsAsync().Result);
        }

        [Test]
        public void Ensure_GetSegmentsRefreshByIdAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetSegmentsRefreshByIdAsync(sampleSegmentId).Result);
        }

        [Test]
        public void Ensure_GetServerTimeAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetServerTimeAsync().Result);
        }

        [Test]
        public void Ensure_GetTemplateByIdAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetTemplateByIdAsync(sampleTemplateId).Result);
        }

        [Test]
        public void Ensure_GetTemplatesAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetTemplatesAsync().Result);
        }

        #endregion

        #region Post Methods

        [Test]
        public void Ensure_PostAddressBookContactsAsync_Works()
        {
            var client = GetClient();
            var contact = GetSampleContact();
            AssertResult(() => client.PostAddressBookContactsAsync(sampleAddressBookId, contact).Result);
        }

        [Test]
        public void Ensure_PostAddressBookContactsImportAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.PostAddressBookContactsImportAsync(sampleAddressBookId).Result);
        }

        [Test]
        public void Ensure_PostAddressBookContactsResubscribeAsync_Works()
        {
            var client = GetClient();
            var contactResubscription = GetSampleResubscription();
            AssertResult(() => client.PostAddressBookContactsResubscribeAsync(sampleAddressBookId, contactResubscription).Result);
        }

        [Test]
        public void Ensure_PostAddressBookContactsUnsubscribeAsync_Works()
        {
            var client = GetClient();
            var contact = GetSampleContact();
            AssertResult(() => client.PostAddressBookContactsUnsubscribeAsync(sampleAddressBookId, contact).Result);
        }

        [Test]
        public void Ensure_PostAddressBooks_Works()
        {
            var client = GetClient();
            var addressBook = GetSampleAddressBook();
            AssertResult(() => client.PostAddressBooksAsync(addressBook).Result);
        }

        [Test]
        public void Ensure_PostCampaignAttachmentsAsync_Works()
        {
            var client = GetClient();
            var document = GetSampleDocument();
            AssertResult(() => client.PostCampaignAttachmentsAsync(sampleCampaignId, document).Result);
        }

        [Test]
        public void Ensure_PostCampaignCopyAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.PostCampaignCopyAsync(sampleCampaignId).Result);
        }

        [Test]
        public void Ensure_PostCampaignsAsync_Works()
        {
            var client = GetClient();
            var campaign = GetSampleCampaign();
            AssertResult(() => client.PostCampaignsAsync(campaign).Result);
        }

        [Test]
        public void Ensure_PostCampaignsSendAsync_Works()
        {
            var client = GetClient();
            var campaignSend = GetSampleCampaignSend();
            AssertResult(() => client.PostCampaignsSendAsync(campaignSend).Result);
        }

        [Test]
        public void Ensure_PostContactsAsync_Works()
        {
            var client = GetClient();
            var contact = GetSampleContact();
            AssertResult(() => client.PostContactsAsync(contact).Result);
        }

        [Test]
        public void Ensure_PostContactsImportAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.PostContactsImportAsync().Result);
        }

        [Test]
        public void Ensure_PostContactsResubscribeAsync_Works()
        {
            var client = GetClient();
            var resubscription = GetSampleResubscription();
            AssertResult(() => client.PostContactsResubscribeAsync(resubscription).Result);
        }

        [Test]
        public void Ensure_PostContactsTransactionalDataAsync_Works()
        {
            var client = GetClient();
            var transactionalData = GetSampleTransactionalData();
            AssertResult(() => client.PostContactsTransactionalDataAsync(sampleTransactionalDataCollectionName, transactionalData).Result);
        }

        [Test]
        public void Ensure_PostContactsTransactionalDataImportAsync_Works()
        {
            var client = GetClient();
            var transactionalDataList = GetSampleTransactionalDataList();
            AssertResult(() => client.PostContactsTransactionalDataImportAsync(sampleTransactionalDataCollectionName, transactionalDataList).Result);
        }

        [Test]
        public void Ensure_PostContactsUnsubscribeAsync_Works()
        {
            var client = GetClient();
            var contact = GetSampleContact();
            AssertResult(() => client.PostContactsUnsubscribeAsync(contact).Result);
        }

        [Test]
        public void Ensure_PostDataFieldsAsync_Works()
        {
            var client = GetClient();
            var dataField = GetSampleDataField();
            AssertResult(() => client.PostDataFieldsAsync(dataField).Result);
        }

        [Test]
        public void Ensure_PostDocumentFolderAsync_Works()
        {
            var client = GetClient();
            var documentFolder = GetSampleDocumentFolder();
            AssertResult(() => client.PostDocumentFolderAsync(1, documentFolder).Result);
        }

        [Test]
        public void Ensure_PostDocumentFolderDocumentsAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.PostDocumentFolderDocumentsAsync(sampleDocumentFolderId).Result);
        }

        [Test]
        public void Ensure_PostImageFolderAsync_Works()
        {
            var client = GetClient();
            var folder = GetSampleImageFolder();
            AssertResult(() => client.PostImageFolderAsync(sampleCampaignId, folder).Result);
        }

        [Test]
        public void Ensure_PostImageFolderImagesAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.PostImageFolderImagesAsync(sampleImageFolderId).Result);
        }

        [Test]
        public void Ensure_PostSegmentsRefreshAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.PostSegmentsRefreshAsync(sampleSegmentId).Result);
        }

        [Test]
        public void Ensure_PostSmsMessagesSendToAsync_Works()
        {
            var client = GetClient();
            var sms = GetSampleSms();
            AssertResult(() => client.PostSmsMessagesSendToAsync(sampleTelephoneNumber, sms).Result);
        }

        [Test]
        public void Ensure_PostTemplatesAsync_Works()
        {
            var client = GetClient();
            var template = GetSampleTemplate();
            AssertResult(() => client.PostTemplatesAsync(template).Result);
        }

        #endregion

        #region Update

        [Test]
        public void Ensure_UpdateAddressBookAsync_Works()
        {
            var client = GetClient();
            var addressBook = GetSampleAddressBook();
            AssertResult(() => client.UpdateAddressBookAsync(sampleAddressBookId, addressBook).Result);
        }

        [Test]
        public void Ensure_UpdateCampaignAsync_Works()
        {
            var client = GetClient();
            var campaign = GetSampleCampaign();
            AssertResult(() => client.UpdateCampaignAsync(sampleCampaignId, campaign).Result);
        }

        [Test]
        public void Ensure_UpdateContactAsync_Works()
        {
            var client = GetClient();
            var contact = GetSampleContact();
            AssertResult(() => client.UpdateContactAsync(sampleContactId, contact).Result);
        }

        [Test]
        public void Ensure_UpdateTemplateAsync_Works()
        {
            var client = GetClient();
            var template = GetSampleTemplate();
            AssertResult(() => client.UpdateTemplateAsync(sampleTemplateId, template).Result);
        }

        #endregion
    }
}
