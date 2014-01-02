using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace dotMailer.Api.TestBench
{
    class Program
    {
        static void Main()
        {
            ErrorTest();


            //Sync();
            //Console.WriteLine("");
            //Async();

            Console.ReadKey();
        }

        private static void ErrorTest()
        {
            var client = new Client("demo@apiconnector.com", "demo");
            var result = client.DeleteAddressBook(838);
            //result = client.DeleteAddressBookContact(838, 838);
            //result = client.DeleteAddressBookContacts(838);
            //result = client.DeleteCampaignAttachment(838, 838);
            //result = client.GetAddressBookById(838);
            //result = client.GetAddressBookCampaigns(838);
            //result = client.GetAddressBookContacts(838);
            //result = client.GetCampaignActivities(838);
            //result = client.GetContactsImportByImportId(Guid.Empty);
            //result = client.GetContactsImportReport(Guid.Empty);
            //result = client.GetContactsImportReportFaults(Guid.Empty);
            //result = client.GetContactsTransactionalDataImportByImportId(Guid.Empty);
            result = client.GetContactTransactionalDataByCollectionName("test", 300);
        }

        private static void Sync()
        {
            var sync = Benchmark.This(() =>
            {
                var client = new Client("demo@apiconnector.com", "demo");
                var addressBooks = client.GetAddressBooks();
                var contacts = client.GetContacts();
                var campaigns = client.GetCampaigns();

                Console.WriteLine("AddressBooks: {0}", addressBooks.Data.Count);
                Console.WriteLine("Contacts: {0}", contacts.Data.Count);
                Console.WriteLine("Campaigns: {0}", campaigns.Data.Count);
            });
            Console.WriteLine("Sync: " + sync.Execute());
        }

        private static void Async()
        {
            var async = Benchmark.This(() =>
            {
                var client = new Client("demo@apiconnector.com", "demo");
                var addressBookTask = client.GetAddressBooksAsync();
                var contactsTask = client.GetContactsAsync();
                var campaignsTask = client.GetCampaignsAsync();

                Task.WaitAll(new Task[] { addressBookTask, contactsTask, campaignsTask });

                Console.WriteLine("AddressBooks: {0}", addressBookTask.Result.Data.Count);
                Console.WriteLine("Contacts: {0}", contactsTask.Result.Data.Count);
                Console.WriteLine("Campaigns: {0}", campaignsTask.Result.Data.Count);
            });
            Console.WriteLine("Async: " + async.Execute());
        }

        public sealed class Benchmark
        {
            private readonly Action subject;
            private Benchmark(Action subject) { this.subject = subject; }

            public static Benchmark This(Action subject)
            {
                return new Benchmark(subject);
            }

            public string Execute()
            {
                var watch = new Stopwatch();
                while (true)
                {
                    watch.Reset();
                    watch.Start();
                    subject();
                    watch.Stop();
                    return "Executed in: " + watch.ElapsedMilliseconds.ToString("###,###.##") + "ms";
                }
            }
        }
    }
}
