using System;
using System.Linq;
using System.Reflection;
using dotMailer.Api.Resources.Models;
using NUnit.Framework;

namespace dotMailer.Api.Tests
{
    [TestFixture]
    public class ClientTests
    {
        #region Helpers

        private Client GetClient()
        {
            return new Client("demo@apiconnector.com", "demo");
        }

        private void AssertResult(Func<ServiceResult> method)
        {
            var result = method();
            Assert.IsTrue(result.Success);
        }

        private void AssertResult<T>(Func<ServiceResult<T>> method)
        {
            var result = method();
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Data);
            Assert.IsInstanceOf<T>(result.Data);
        }

        #endregion

        [SetUp]
        public void Setup()
        {
            // Get some common resource identifiers so we don't have to hit the API all the time
            var client = GetClient();
            sampleAddressBookId = client.GetAddressBooksPublic().Data.First().Id;
            sampleCampaignId = client.GetCampaigns().Data.First().Id;
            var sampleContact = client.GetContacts().Data.First();
            sampleContactId = sampleContact.Id;
            sampleContactEmail = sampleContact.Email;
            sampleSinceDate = DateTime.Now.AddMonths(-1);
            sampleImageFolderId = client.GetImageFolders().Data.First().Id;
            sampleTemplateId = client.GetTemplates().Data.First().Id;
            sampleDocumentFolderId = client.GetDocumentFolders().Data.First().Id;
            sampleDocumentId = client.GetDocumentFolderDocuments(sampleDocumentFolderId).Data.First().Id;
        }

        private int sampleAddressBookId = -1;
        private int sampleCampaignId = -1;
        private int sampleContactId = -1;
        private string sampleContactEmail = string.Empty;
        private DateTime sampleSinceDate;
        private int sampleTemplateId = -1;
        private int sampleImageFolderId = -1;
        private int sampleDocumentFolderId = -1;
        private int sampleDocumentId = -1;
        
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
        public void Ensure_DeleteAddressBookContact_Works()
        {
            var client = GetClient();
            var addressBook = client.GetAddressBooks().Data.First();
            var contact = client.GetAddressBookContacts(addressBook.Id).Data.First();
            AssertResult(() => client.DeleteAddressBookContact(addressBook.Id, contact.Id));
        }

        [Test]
        public void Ensure_DeleteAddressBookContacts_Works()
        {
            var client = GetClient();
            AssertResult(() => client.DeleteAddressBookContacts(sampleAddressBookId));
        }

        [Test]
        public void Ensure_DeleteAddressBookContactsInbulk_Works()
        {
            var client = GetClient();
            var contacts = client.GetAddressBookContacts(sampleAddressBookId).Data;
            // TODO: Do we really need an Int32List class generated from the WADL? Would be better to convert this to an int[] or List<int>
            var contactIds = new Int32List();
            contactIds.AddRange(contacts.Select(x => x.Id));

            AssertResult(() => client.DeleteAddressBookContactsInbulk(sampleAddressBookId, contactIds));
        }

        [Test]
        public void Ensure_DeleteCampaignAttachment_Works()
        {
            var client = GetClient();
            AssertResult(() => client.DeleteCampaignAttachment(sampleCampaignId, sampleDocumentId));
        }

        [Test]
        public void Ensure_DeleteContactTransactionalData_Works()
        {
            var client = GetClient();
            AssertResult(() => client.DeleteContactTransactionalData("Collection name", sampleContactId));
        }

        [Test]
        public void Ensure_DeleteDataField_Works()
        {
            var client = GetClient();
            AssertResult(() => client.DeleteDataField("Sample data field name"));
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
        public void Ensure_GetCustomFromAddresses_Works()
        {
            var client = GetClient();
            AssertResult(() => client.GetCustomFromAddresses());
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
        public void Ensure_PostAddressBookContactsImport_Works()
        {
            var client = GetClient();
            AssertResult(() => client.PostAddressBookContactsImport(sampleAddressBookId));
        }

        [Test]
        public void Ensure_PostAddressBookContactsResubscribe_Works()
        {
            var client = GetClient();
            var contactResubscription = new ApiContactResubscription();
            AssertResult(() => client.PostAddressBookContactsResubscribe(sampleAddressBookId, contactResubscription));
        }
        
        [Test]
        public void Ensure_PostAddressBookContactsUnsubscribe_Works()
        {
            var client = GetClient();
            var contact = new ApiContact();
            AssertResult(() => client.PostAddressBookContactsUnsubscribe(sampleAddressBookId, contact));
        }
        
        [Test]
        public void Ensure_PostAddressBooks_Works()
        {
            var client = GetClient();
            var addressBook = new ApiAddressBook();
            AssertResult(() => client.PostAddressBooks(addressBook));
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
            var campaign = new ApiCampaign();
            AssertResult(() => client.PostCampaigns(campaign));
        }
        
        [Test]
        public void Ensure_PostCampaignsSend_Works()
        {
            var client = GetClient();
            var campaignSend = new ApiCampaignSend();
            AssertResult(() => client.PostCampaignsSend(campaignSend));
        }
        
        [Test]
        public void Ensure_PostContacts_Works()
        {
            var client = GetClient();
            var contact = new ApiContact();
            AssertResult(() => client.PostContacts(contact));
        }
        
        [Test]
        public void Ensure_PostContactsImport_Works()
        {
            var client = GetClient();
            AssertResult(client.PostContactsImport);
        }
        
        [Test]
        public void Ensure_PostContactsResubscribe_Works()
        {
            var client = GetClient();
            var resubscription = new ApiContactResubscription();
            AssertResult(() => client.PostContactsResubscribe(resubscription));
        }
        
        [Test]
        public void Ensure_PostContactsTransactionalData_Works()
        {
            var client = GetClient();
            var transactionalData = new ApiTransactionalData();
            AssertResult(() => client.PostContactsTransactionalData("Collection name", transactionalData));
        }
        
        [Test]
        public void Ensure_PostContactsTransactionalDataImport_Works()
        {
            var client = GetClient();
            var transactionalDataList = new ApiTransactionalDataList();
            AssertResult(() => client.PostContactsTransactionalDataImport("Collection name", transactionalDataList));
        }
        
        [Test]
        public void Ensure_PostContactsUnsubscribe_Works()
        {
            var client = GetClient();
            var contact = new ApiContact();
            AssertResult(() => client.PostContactsUnsubscribe(contact));
        }
        
        [Test]
        public void Ensure_PostDataFields_Works()
        {
            var client = GetClient();
            var dataField = new ApiDataField();
            AssertResult(() => client.PostDataFields(dataField));
        }
        
        [Test]
        public void Ensure_PostDocumentFolder_Works()
        {
            var client = GetClient();
            var documentFolder = new ApiDocumentFolder();
            AssertResult(() => client.PostDocumentFolder(1, documentFolder));
        }
        
        [Test]
        public void Ensure_PostImageFolderImages_Works()
        {
            var client = GetClient();
            AssertResult(() => client.PostImageFolderImages(sampleImageFolderId));
        }
        
        [Test]
        public void Ensure_PostSegmentsRefreh_Works()
        {
            // TODO: Spelling mistake
            var client = GetClient();
            AssertResult(() => client.PostSegmentsRefreh(1));
        }
        
        [Test]
        public void Ensure_PostSmsMessagesSendTo_Works()
        {
            var client = GetClient();
            const string telephoneNumber = "01234567890";
            var sms = new ApiSms();
            AssertResult(() => client.PostSmsMessagesSendTo(telephoneNumber, sms));
        }
        
        [Test]
        public void Ensure_PostTemplates_Works()
        {
            var client = GetClient();
            var template = new ApiTemplate();
            AssertResult(() => client.PostTemplates(template));
        }

        #endregion
    }
}
