using System;
using System.Linq;
using System.Reflection;
using dotMailer.Api.Resources.Enums;
using dotMailer.Api.Resources.Models;
using NUnit.Framework;

namespace dotMailer.Api.Tests
{
    public abstract class ClientTestBase
    {
        private const string sampleCampaignSubject = "Sample Campaign";
        private const string sampleCampaignName = "Sample Campaign";
        private const string sampleCampaignFromName = "test@test.com";
        private const string sampleCampaignHtmlContent = "<h1>Hello World!</h1>";
        private const string sampleCampaignPlainTextContent = "Hello World";
        private const string sampleDocumentFolderName = "Document Name";
        private const string sampleImageFolderName = "Image Folder Name";
        protected const string sampleTelephoneNumber = "01234567890";
        protected const string sampleDataFieldName = "Data Field";
        protected const string sampleTransactionalDataKey = "CollectionKey";
        protected const string sampleTransactionalDataCollectionName = "Collection Name";
        protected int sampleAddressBookId = -1;
        protected int sampleCampaignId = -1;
        protected int sampleContactId = -1;
        protected string sampleContactEmail = string.Empty;
        protected DateTime sampleSinceDate;
        protected int sampleTemplateId = -1;
        protected int sampleImageFolderId = -1;
        protected int sampleDocumentFolderId = -1;
        protected int sampleDocumentId = -1;
        protected int sampleSegmentId = -1;

        protected Client GetClient()
        {
            return new Client("demo@apiconnector.com", "demo");
        }

        protected void AssertResult(Func<ServiceResult> method)
        {
            var result = method();
            Assert.IsTrue(result.Success, result.Message);
        }

        protected void AssertResult<T>(Func<ServiceResult<T>> method)
        {
            var result = method();
            Assert.IsTrue(result.Success, result.Message);
            Assert.IsNotNull(result.Data);
            Assert.IsInstanceOf<T>(result.Data);
        }

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
            sampleSegmentId = client.GetSegments().Data.First().Id;
        }

        #region Sample Data

        protected ApiTemplate GetSampleTemplate()
        {
            return new ApiTemplate
            {
                Name = sampleCampaignName,
                Subject = sampleCampaignSubject,
                FromName = sampleCampaignFromName,
                HtmlContent = sampleCampaignHtmlContent,
                PlainTextContent = sampleCampaignPlainTextContent
            };
        }

        protected ApiCampaign GetSampleCampaign()
        {
            return new ApiCampaign
            {
                Id = sampleCampaignId,
                Name = sampleCampaignName,
                Subject = sampleCampaignSubject,
                FromName = sampleCampaignFromName,
                HtmlContent = sampleCampaignHtmlContent,
                PlainTextContent = sampleCampaignPlainTextContent
            };
        }

        protected ApiDataField GetSampleDataField()
        {
            return new ApiDataField
            {
                Name = sampleDataFieldName,
                Type = ApiDataTypes.String,
                Visibility = ApiDataFieldVisibility.Public
            };
        }

        protected ApiImageFolder GetSampleImageFolder()
        {
            return new ApiImageFolder
            {
                Name = sampleImageFolderName
            };
        }

        protected ApiDocumentFolder GetSampleDocumentFolder()
        {
            return new ApiDocumentFolder
            {
                Name = sampleDocumentFolderName
            };
        }

        protected ApiContact GetSampleContact()
        {
            return new ApiContact
            {
                Email = sampleContactEmail
            };
        }

        protected ApiAddressBook GetSampleAddressBook()
        {
            return new ApiAddressBook();
        }

        protected ApiContactResubscription GetSampleResubscription()
        {
            return new ApiContactResubscription
            {
                UnsubscribedContact = GetSampleContact()
            };
        }

        protected ApiTransactionalData GetSampleTransactionalData()
        {
            return new ApiTransactionalData();
        }

        protected ApiSms GetSampleSms()
        {
            return new ApiSms();
        }

        protected ApiTransactionalDataList GetSampleTransactionalDataList()
        {
            return new ApiTransactionalDataList();
        }

        protected ApiCampaignSend GetSampleCampaignSend()
        {
            var contactIds = new Int32List { sampleContactId };
            var addressBookIds = new Int32List { sampleAddressBookId };
            return new ApiCampaignSend
            {
                CampaignId = sampleCampaignId,
                ContactIds = contactIds,
                AddressBookIds = addressBookIds
            };
        }

        protected ApiDocument GetSampleDocument()
        {
            return new ApiDocument();
        }

        #endregion

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
    }
}