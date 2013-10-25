namespace dotMailer.Api.TestBench
{
    class Program
    {
        static void Main()
        {
            var client = new Client("demo@apiconnector.com", "demo");
            var accountInfo = client.GetAccountInfo();

            var addressBooksPublic = client.GetAddressBooksPublic();
        }
    }
}
