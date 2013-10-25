dotMailer .NET API client
=========

Provides a simple .NET client for the dotMailer REST API that encapsulates some of the lower-level HTTP protocol code leaving consumers with clean methods and simple objects with which to interact with dotMailer.

The solution consists of 3 projects:

### dotMailer.Api

This is the main project for interacting with the dotMailer API.  All the classes within are auto-generated from the dotMailer.Api.WadlParser (see below) so don't modify them individually here.

This contains the main Client class which is used to call methods the API. Here's an example of the Client in action:

```csharp
    var client = new Client("demo@apiconnector.com", "demo");
    var result = client.GetAddressBooks();
    foreach (var addressBook in result.Data)
      Console.WriteLine(addressBook.Name);
```

It really is as simple as that!

All of the API methods return a `ServiceResult` which will let you know whether the call was successful or not.  If the call wasn't succesful you'll also be given a simple human-readable message to explain the problem.

```csharp
    var client = new Client("demo@apiconnector.com", "demo");
    var result = client.GetAddressBooks();
    if (!result.Success)
      Console.WriteLine("Something went wrong: " + result.Message);
```

If you're expecting some data back then you'll be given a `ServiceResult` with a correctly typed `Data` property which you can use.

```csharp
    var client = new Client("demo@apiconnector.com", "demo");
    var result = client.GetContacts();
    foreach (var contact in result.Data)
      Console.WriteLine(contact.Email);
```

### dotMailer.Api.WadlParser

This console application is responsible for retrieving the latest WADL definition from http://api.dotmailer.com/v2/help/wadl and generating C# POCO classes and methods for interacting with the dotMailer API.

This will generate C# classes on the local file system which can then be copied directly into the dotMailer.Api project.  This was specifically built for the current dotMailer API WADL but could potentially be used on other WADL definitions with some minor modifications.

### dotMailer.Api.TestBench

A small console application for testing method calls and objects returned from the dotMailer.Api client class.


#### Known Issues

- Methods that require dates are currently not supported
- Methods that require binary uploads are not supported
- All client methods are synchronous and will be updated to async in the near future


#### Limits

Please be aware that the dotMailer API has [usage limits](http://api.dotmailer.com/) - please use bulk API methods where you can, and use the in-app API call debugger if you need to troubleshoot your integration.

We encourage pull requests - please do contribute if you have any updates or bug fixes for us!

#### Credentials

You can either use your own API credentials, or, if you wish, we have a demo set of credentials to help you learn how to use the API. These demo credentials will allow you to review a typical API response to each method or operation. However, this is not a sandbox - data sent using these demo credentials will not be persisted, and no emails will be sent in response to these API operations.

The credentials are:

Username: demo@apiconnector.com  
Password: demo

