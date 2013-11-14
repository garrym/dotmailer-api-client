using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using dotMailer.Api.Resources.Models;
using NUnit.Framework;

namespace dotMailer.Api.Tests
{
    [TestFixture]
    public class ClientTests : ClientTestBase
    {
        [Test]
        public void Ensure_Code_Coverage()
        {
            var clientMethods = typeof(Client).GetMethods().Where(m => m.DeclaringType != typeof(object)).OrderBy(x => x.Name).ToList();
            var testMethods = typeof(ClientTests).GetMethods().Where(m => m.DeclaringType != typeof(object)).OrderBy(x => x.Name).ToList();
            var testMethodNames = testMethods.Select(x => x.Name).Distinct().ToList();

            foreach (var clientMethod in clientMethods)
            {
                var expectedTestMethodName = string.Format("Ensure_{0}_Works", clientMethod.Name);
                Assert.Contains(expectedTestMethodName, testMethodNames);

                var testMethod = testMethods.Single(x => x.Name.Equals(expectedTestMethodName));

                if (!testMethod.GetCustomAttributes(typeof(TestAttribute)).Any())
                    Assert.Fail("Client method '{0}' is not covered by expected test '{1}'", clientMethod.Name, expectedTestMethodName);

                if (testMethod.GetCustomAttributes(typeof(IgnoreAttribute)).Any())
                    Assert.Fail("Client method '{0}' is not covered by expected test '{1}' because it has an ignore flag", clientMethod.Name, expectedTestMethodName);
            }
        }

        #region Delete Methods

        [Test]
        public void Ensure_DeleteAddressBook_Works()
        {
            var client = GetClient();
            AssertResult(() => client.DeleteAddressBook(sampleAddressBookId));
        }

        [Test]
        public void Ensure_DeleteAddressBookContact_Works()
        {
            var client = GetClient();
            AssertResult(() => client.DeleteAddressBookContact(sampleAddressBookId, sampleContactId));
        }

        [Test]
        public void Ensure_DeleteAddressBookContacts_Works()
        {
            var client = GetClient();
            AssertResult(() => client.DeleteAddressBookContacts(sampleAddressBookId));
        }

        //[Test]
        //public void Ensure_DeleteAddressBookContactsInbulk_Works()
        //{
        //    var client = GetClient();
        //    var contacts = client.GetAddressBookContacts(sampleAddressBookId).Data;
        //    var contactIds = new Int32List();
        //    contactIds.AddRange(contacts.Select(x => x.Id));

        //    AssertResult(() => client.DeleteAddressBookContactsInbulk(sampleAddressBookId, contactIds));
        //}

        [Test]
        public void Ensure_DeleteCampaignAttachment_Works()
        {
            var client = GetClient();
            AssertResult(() => client.DeleteCampaignAttachment(sampleCampaignId, sampleDocumentId));
        }

        [Test]
        public void Ensure_DeleteContact_Works()
        {
            var client = GetClient();
            AssertResult(() => client.DeleteContact(sampleContactId));
        }

        [Test]
        public void Ensure_DeleteContactsTransactionalData_Works()
        {
            var client = GetClient();
            AssertResult(() => client.DeleteContactsTransactionalData(sampleTransactionalDataCollectionName, sampleTransactionalDataKey));
        }

        [Test]
        public void Ensure_DeleteContactTransactionalData_Works()
        {
            var client = GetClient();
            AssertResult(() => client.DeleteContactTransactionalData(sampleTransactionalDataCollectionName, sampleContactId));
        }

        [Test]
        public void Ensure_DeleteDataField_Works()
        {
            var client = GetClient();
            AssertResult(() => client.DeleteDataField(sampleDataFieldName));
        }

        #endregion

        #region Get Methods

        [Test]
        public void Ensure_GetAccountInfo_Works()
        {
            var client = GetClient();
            AssertResult(client.GetAccountInfo);
        }

        [Test]
        public void Ensure_GetAddressBookById_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetAddressBookById(sampleAddressBookId));
        }

        [Test]
        public void Ensure_GetAddressBookCampaigns_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetAddressBookCampaigns(sampleAddressBookId));
        }

        [Test]
        public void Ensure_GetAddressBookContacts_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetAddressBookContacts(sampleAddressBookId));
        }

        [Test]
        public void Ensure_GetAddressBookContactsModifiedSinceDate_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetAddressBookContactsModifiedSinceDate(sampleAddressBookId, sampleSinceDate));
        }

        [Test]
        public void Ensure_GetAddressBookContactsUnsubscribedSinceDate_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetAddressBookContactsUnsubscribedSinceDate(sampleAddressBookId, sampleSinceDate));
        }

        [Test]
        public void Ensure_GetAddressBooks_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetAddressBooks(sampleAddressBookId));
        }

        [Test]
        public void Ensure_GetAddressBooksPrivate_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetAddressBooksPrivate());
        }

        [Test]
        public void Ensure_GetAddressBooksPublic_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetAddressBooksPublic());
        }

        [Test]
        public void Ensure_GetCampaignActivities_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetCampaignActivities(sampleCampaignId));
        }

        [Test]
        public void Ensure_GetCampaignActivitiesSinceDateByDate_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetCampaignActivitiesSinceDateByDate(sampleCampaignId, sampleSinceDate));
        }

        [Test]
        public void Ensure_GetCampaignActivityByContactId_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetCampaignActivityByContactId(sampleCampaignId, sampleContactId));
        }

        [Test]
        public void Ensure_GetCampaignActivityClicks_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetCampaignActivityClicks(sampleCampaignId, sampleContactId));
        }

        [Test]
        public void Ensure_GetCampaignActivityOpens_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetCampaignActivityOpens(sampleCampaignId, sampleContactId));
        }

        [Test]
        public void Ensure_GetCampaignActivityPageViews_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetCampaignActivityPageViews(sampleCampaignId, sampleContactId));
        }

        [Test]
        public void Ensure_GetCampaignActivityReplies_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetCampaignActivityReplies(sampleCampaignId, sampleContactId));
        }

        [Test]
        public void Ensure_GetCampaignActivityRoiDetails_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetCampaignActivityRoiDetails(sampleCampaignId, sampleContactId));
        }

        [Test]
        public void Ensure_GetCampaignActivitySocialBookmarkViews_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetCampaignActivitySocialBookmarkViews(sampleCampaignId, sampleContactId));
        }

        [Test]
        public void Ensure_GetCampaignAddressBooks_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetCampaignAddressBooks(sampleCampaignId));
        }

        [Test]
        public void Ensure_GetCampaignAttachments_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetCampaignAttachments(sampleCampaignId));
        }

        [Test]
        public void Ensure_GetCampaignById_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetCampaignById(sampleCampaignId));
        }

        [Test]
        public void Ensure_GetCampaignClicks_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetCampaignClicks(sampleCampaignId));
        }

        [Test]
        public void Ensure_GetCampaignHardBouncingContacts_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetCampaignHardBouncingContacts(sampleCampaignId));
        }

        [Test]
        public void Ensure_GetCampaignOpens_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetCampaignOpens(sampleCampaignId));
        }

        [Test]
        public void Ensure_GetCampaignPageViewsSinceDateByDate_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetCampaignPageViewsSinceDateByDate(sampleCampaignId, sampleSinceDate));
        }

        [Test]
        public void Ensure_GetCampaignRoiDetailsSinceDateByDate_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetCampaignRoiDetailsSinceDateByDate(sampleCampaignId, sampleSinceDate));
        }

        [Test]
        public void Ensure_GetCampaigns_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetCampaigns());
        }

        [Test]
        public void Ensure_GetCampaignSocialBookmarkViews_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetCampaignSocialBookmarkViews(sampleCampaignId));
        }

        [Test]
        public void Ensure_GetCampaignsSendBySendId_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetCampaignsSendBySendId(Guid.Empty));
        }

        [Test]
        public void Ensure_GetCampaignSummary_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetCampaignSummary(sampleCampaignId));
        }

        [Test]
        public void Ensure_GetCampaignsWithActivitySinceDate_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetCampaignsWithActivitySinceDate(sampleSinceDate));
        }

        [Test]
        public void Ensure_GetContactAddressBooks_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetContactAddressBooks(sampleContactId));
        }

        [Test]
        public void Ensure_GetContactByEmail_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetContactByEmail(sampleContactEmail));
        }

        [Test]
        public void Ensure_GetContactById_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetContactById(sampleContactId));
        }

        [Test]
        public void Ensure_GetContacts_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetContacts());
        }

        [Test]
        public void Ensure_GetContactsCreatedSinceDate_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetContactsCreatedSinceDate(sampleSinceDate));
        }

        [Test]
        public void Ensure_GetContactsImportByImportId_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetContactsImportByImportId(Guid.Empty));
        }

        [Test]
        public void Ensure_GetContactsImportReport_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetContactsImportReport(Guid.Empty));
        }

        [Test]
        public void Ensure_GetContactsImportReportFaults_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetContactsImportReportFaults(Guid.Empty));
        }

        [Test]
        public void Ensure_GetContactsModifiedSinceDate_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetContactsModifiedSinceDate(sampleSinceDate));
        }

        [Test]
        public void Ensure_GetContactsSuppressedSinceDate_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetContactsSuppressedSinceDate(sampleSinceDate));
        }

        [Test]
        public void Ensure_GetContactsTransactionalDataByKey_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetContactsTransactionalDataByKey(sampleTransactionalDataCollectionName, sampleTransactionalDataKey));
        }

        [Test]
        public void Ensure_GetContactsTransactionalDataImportByImportId_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetContactsTransactionalDataImportByImportId(Guid.Empty));
        }

        [Test]
        public void Ensure_GetContactsTransactionalDataImportReport_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetContactsTransactionalDataImportReport(Guid.Empty));
        }

        [Test]
        public void Ensure_GetContactsUnsubscribedSinceDate_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetContactsUnsubscribedSinceDate(sampleSinceDate));
        }

        [Test]
        public void Ensure_GetContactTransactionalDataByCollectionName_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetContactTransactionalDataByCollectionName(sampleTransactionalDataCollectionName, sampleContactEmail));
        }

        [Test]
        public void Ensure_GetCustomFromAddresses_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetCustomFromAddresses());
        }

        [Test]
        public void Ensure_GetDataFields_Works()
        {
            var client = GetClient();
            AssertResult(client.GetDataFields);
        }

        [Test]
        public void Ensure_GetDocumentFolderDocuments_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetDocumentFolderDocuments(1));
        }

        [Test]
        public void Ensure_GetDocumentFolders_Works()
        {
            var client = GetClient();
            AssertResult(client.GetDocumentFolders);
        }

        [Test]
        public void Ensure_GetImageFolderById_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetImageFolderById(sampleImageFolderId));
        }

        [Test]
        public void Ensure_GetImageFolders_Works()
        {
            var client = GetClient();
            AssertResult(client.GetImageFolders);
        }

        [Test]
        public void Ensure_GetSegments_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetSegments());
        }

        [Test]
        public void Ensure_GetSegmentsRefreshById_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetSegmentsRefreshById(sampleSegmentId));
        }

        [Test]
        public void Ensure_GetServerTime_Works()
        {
            var client = GetClient();
            AssertResult(client.GetServerTime);
        }

        [Test]
        public void Ensure_GetTemplateById_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetTemplateById(sampleTemplateId));
        }

        [Test]
        public void Ensure_GetTemplates_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetTemplates());
        }

        #endregion

        #region Post Methods

        [Test]
        public void Ensure_PostAddressBookContacts_Works()
        {
            var client = GetClient();
            var contact = GetSampleContact();
            AssertResult(() => client.PostAddressBookContacts(sampleAddressBookId, contact));
        }

        [Test]
        public void Ensure_PostAddressBookContactsDelete_Works()
        {
            var client = GetClient();
            AssertResult(() => client.PostAddressBookContactsDelete(sampleAddressBookId, new List<int> { sampleContactId }));
        }

        [Test]
        public void Ensure_PostAddressBookContactsImport_Works()
        {
            var client = GetClient();
            var file = new ApiFileMedia();
            AssertResult(() => client.PostAddressBookContactsImport(sampleAddressBookId, file));
        }

        [Test]
        public void Ensure_PostAddressBookContactsResubscribe_Works()
        {
            var client = GetClient();
            var contactResubscription = GetSampleResubscription();
            AssertResult(() => client.PostAddressBookContactsResubscribe(sampleAddressBookId, contactResubscription));
        }

        [Test]
        public void Ensure_PostAddressBookContactsUnsubscribe_Works()
        {
            var client = GetClient();
            var contact = GetSampleContact();
            AssertResult(() => client.PostAddressBookContactsUnsubscribe(sampleAddressBookId, contact));
        }

        [Test]
        public void Ensure_PostAddressBooks_Works()
        {
            var client = GetClient();
            var addressBook = GetSampleAddressBook();
            AssertResult(() => client.PostAddressBooks(addressBook));
        }

        [Test]
        public void Ensure_PostCampaignAttachments_Works()
        {
            var client = GetClient();
            var document = GetSampleDocument();
            AssertResult(() => client.PostCampaignAttachments(sampleCampaignId, document));
        }

        [Test]
        public void Ensure_PostCampaignCopy_Works()
        {
            var client = GetClient();
            AssertResult(() => client.PostCampaignCopy(sampleCampaignId));
        }

        [Test]
        public void Ensure_PostCampaigns_Works()
        {
            var client = GetClient();
            var campaign = GetSampleCampaign();
            AssertResult(() => client.PostCampaigns(campaign));
        }

        [Test]
        public void Ensure_PostCampaignsSend_Works()
        {
            var client = GetClient();
            var campaignSend = GetSampleCampaignSend();
            AssertResult(() => client.PostCampaignsSend(campaignSend));
        }

        [Test]
        public void Ensure_PostContacts_Works()
        {
            var client = GetClient();
            var contact = GetSampleContact();
            AssertResult(() => client.PostContacts(contact));
        }

        [Test]
        public void Ensure_PostContactsImport_Works()
        {
            var client = GetClient();
            var file = new ApiFileMedia();
            AssertResult(() => client.PostContactsImport(file));
        }

        [Test]
        public void Ensure_PostContactsResubscribe_Works()
        {
            var client = GetClient();
            var resubscription = GetSampleResubscription();
            AssertResult(() => client.PostContactsResubscribe(resubscription));
        }

        [Test]
        public void Ensure_PostContactsTransactionalData_Works()
        {
            var client = GetClient();
            var transactionalData = GetSampleTransactionalData();
            AssertResult(() => client.PostContactsTransactionalData(sampleTransactionalDataCollectionName, transactionalData));
        }

        [Test]
        public void Ensure_PostContactsTransactionalDataImport_Works()
        {
            var client = GetClient();
            var transactionalDataList = GetSampleTransactionalDataList();
            AssertResult(() => client.PostContactsTransactionalDataImport(sampleTransactionalDataCollectionName, transactionalDataList));
        }

        [Test]
        public void Ensure_PostContactsUnsubscribe_Works()
        {
            var client = GetClient();
            var contact = GetSampleContact();
            AssertResult(() => client.PostContactsUnsubscribe(contact));
        }

        [Test]
        public void Ensure_PostDataFields_Works()
        {
            var client = GetClient();
            var dataField = GetSampleDataField();
            AssertResult(() => client.PostDataFields(dataField));
        }

        [Test]
        public void Ensure_PostDocumentFolder_Works()
        {
            var client = GetClient();
            var documentFolder = GetSampleDocumentFolder();
            AssertResult(() => client.PostDocumentFolder(1, documentFolder));
        }

        [Test]
        public void Ensure_PostDocumentFolderDocuments_Works()
        {
            var client = GetClient();
            var file = new ApiFileMedia();
            AssertResult(() => client.PostDocumentFolderDocuments(sampleDocumentFolderId, file));
        }

        [Test]
        public void Ensure_PostImageFolder_Works()
        {
            var client = GetClient();
            var folder = GetSampleImageFolder();
            AssertResult(() => client.PostImageFolder(sampleCampaignId, folder));
        }

        [Test]
        public void Ensure_PostImageFolderImages_Works()
        {
            var client = GetClient();
            var file = new ApiFileMedia();
            AssertResult(() => client.PostImageFolderImages(sampleImageFolderId, file));
        }

        [Test]
        public void Ensure_PostSegmentsRefresh_Works()
        {
            var client = GetClient();
            AssertResult(() => client.PostSegmentsRefresh(sampleSegmentId));
        }

        [Test]
        public void Ensure_PostSmsMessagesSendTo_Works()
        {
            var client = GetClient();
            var sms = GetSampleSms();
            AssertResult(() => client.PostSmsMessagesSendTo(sampleTelephoneNumber, sms));
        }

        [Test]
        public void Ensure_PostTemplates_Works()
        {
            var client = GetClient();
            var template = GetSampleTemplate();
            AssertResult(() => client.PostTemplates(template));
        }

        #endregion

        #region Update Methods

        [Test]
        public void Ensure_UpdateAddressBook_Works()
        {
            var client = GetClient();
            var addressBook = GetSampleAddressBook();
            AssertResult(() => client.UpdateAddressBook(sampleAddressBookId, addressBook));
        }

        [Test]
        public void Ensure_UpdateCampaign_Works()
        {
            var client = GetClient();
            var campaign = GetSampleCampaign();
            AssertResult(() => client.UpdateCampaign(sampleCampaignId, campaign));
        }

        [Test]
        public void Ensure_UpdateContact_Works()
        {
            var client = GetClient();
            var contact = GetSampleContact();
            AssertResult(() => client.UpdateContact(sampleContactId, contact));
        }

        [Test]
        public void Ensure_UpdateTemplate_Works()
        {
            var client = GetClient();
            var template = GetSampleTemplate();
            AssertResult(() => client.UpdateTemplate(sampleTemplateId, template));
        }

        #endregion

        #region Delete Async Methods

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

        //[Test]
        //public void Ensure_DeleteAddressBookContactsInbulkAsync_Works()
        //{
        //    var client = GetClient();
        //    var contacts = client.GetAddressBookContacts(sampleAddressBookId).Data;
        //    var contactIds = new Int32List();
        //    contactIds.AddRange(contacts.Select(x => x.Id));

        //    AssertResult(() => client.DeleteAddressBookContacts(sampleAddressBookId, contactIds).Result);
        //}

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

        #region Get Async Methods

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
        public void Ensure_GetCampaignAttachmentsAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetCampaignAttachmentsAsync(sampleCampaignId).Result);
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
        public void Ensure_GetContactsModifiedSinceDateAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetContactsModifiedSinceDateAsync(sampleSinceDate).Result);
        }

        [Test]
        public void Ensure_GetContactsSuppressedSinceDateAsync_Works()
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
        public void Ensure_GetContactsTransactionalDataImportReportAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetContactsTransactionalDataImportReportAsync(Guid.Empty).Result);
        }

        [Test]
        public void Ensure_GetContactsUnsubscribedSinceDateAsync_Works()
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
        public void Ensure_GetDocumentFoldersAsync_Works()
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

        #region Post Async Methods

        [Test]
        public void Ensure_PostAddressBookContactsAsync_Works()
        {
            var client = GetClient();
            var contact = GetSampleContact();
            AssertResult(() => client.PostAddressBookContactsAsync(sampleAddressBookId, contact).Result);
        }

        [Test]
        public void Ensure_PostAddressBookContactsDeleteAsync_Works()
        {
            var client = GetClient();
            AssertResult(() => client.PostAddressBookContactsDeleteAsync(sampleAddressBookId, new List<int> { sampleContactId }).Result);
        }

        [Test]
        public void Ensure_PostAddressBookContactsImportAsync_Works()
        {
            var client = GetClient();
            var file = new ApiFileMedia();
            AssertResult(() => client.PostAddressBookContactsImportAsync(sampleAddressBookId, file).Result);
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
        public void Ensure_PostAddressBooksAsync_Works()
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
            var file = new ApiFileMedia();
            AssertResult(() => client.PostContactsImportAsync(file).Result);
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
            var file = new ApiFileMedia();
            AssertResult(() => client.PostDocumentFolderDocumentsAsync(sampleDocumentFolderId, file).Result);
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
            var file = new ApiFileMedia();
            AssertResult(() => client.PostImageFolderImagesAsync(sampleImageFolderId, file).Result);
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

        #region Update Async Methods

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
