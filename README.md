
[doc]: http://dev.mailjet.com/
[api_ref]: https://dev.mailjet.com/reference/email
[api_credential]: https://app.mailjet.com/account/api_keys
[issues]: https://github.com/mailjet/mailjet-apiv3-dotnet/issues
[csharp_documentation]: http://dev.mailjet.com/guides/?csharp
[api_doc_repo]: https://github.com/mailjet/api-documentation
[nuget]: https://www.nuget.org/packages/Mailjet.Api/
[smsDashboard]: https://app.mailjet.com/sms?_ga=2.81581655.1972348350.1522654521-1279766791.1506937572
[smsInfo]: https://app.mailjet.com/docs/transactional-sms?_ga=2.183303910.1972348350.1522654521-1279766791.1506937572#sms-token
[mailjet]: http://www.mailjet.com
![alt text](https://www.mailjet.com/images/email/transac/logo_header.png "Mailjet")

# Mailjet .NET Wrapper

[![Build Status](https://travis-ci.org/mailjet/mailjet-apiv3-dotnet.svg?branch=master)](https://travis-ci.org/mailjet/mailjet-apiv3-dotnet)
![Current Version](https://img.shields.io/badge/version-2.0.0-green.svg)

## Overview

Welcome to the [Mailjet][mailjet] official .NET API wrapper!

Check out all the resources and .NET code examples in the official [Mailjet Documentation][csharp_documentation].

## Table of contents

- [Mailjet .NET Wrapper](#mailjet-net-wrapper)
  - [Overview](#overview)
  - [Table of contents](#table-of-contents)
  - [Release notes](#release-notes)
    - [v 2.0.2](#v-202)
    - [v 2.0.1](#v-201)
    - [v 2.0.0](#v-200)
  - [Compatibility](#compatibility)
    - [Dependencies .NETStandard 1.1](#dependencies-netstandard-11)
  - [Installation](#installation)
  - [Authentication](#authentication)
  - [Make your first call](#make-your-first-call)
  - [Client / Call configuration specifics](#client--call-configuration-specifics)
    - [Dependency Injection](#dependency-injection)
    - [API Versioning](#api-versioning)
    - [Base URL](#base-url)
    - [Proxy](#proxy)
  - [List of resources](#list-of-resources)
  - [Request Examples](#request-examples)
    - [POST Request](#post-request)
      - [Simple POST request](#simple-post-request)
      - [Using actions](#using-actions)
    - [GET request](#get-request)
      - [Retrieve all objects](#retrieve-all-objects)
      - [Use filtering](#use-filtering)
      - [Retrieve a single object](#retrieve-a-single-object)
    - [PUT request](#put-request)
    - [DELETE request](#delete-request)
  - [SMS API](#sms-api)
    - [Token authentication](#token-authentication)
    - [Example request](#example-request)
    - [Response](#response)
  - [More examples](#more-examples)
  - [Contribute](#contribute)

## Release notes

### v 2.0.2
   - TransactionalEmailBuidler extension now not exposed in the IMailjetClient interface
### v 2.0.1
   - TransactionalEmailBuidler now accepts variables of any type (previously only strings)
### v 2.0.0
   - Added TransactionalEmailBuidler and TransactionalEmail strongly typed models - now you can send transactional emails more easily! Please, check [tests for more information](Mailjet.Tests/Integration/SendTransactionalEmailIntegrationTests.cs)
   - Added Contacts delete API - now you can support [GDPR delete contacts](https://dev.mailjet.com/email/guides/contact-management/#gdpr-delete-contacts) easily
   - Removed ApiVersion from the client configuration - now client will determine needed API version automatically based on resource! Less configuration - easier life!
   - Added strong name for the assembly for backward compatibility with .NET framework strong signed scenarios

## Compatibility

This .NET library is supported by:

 - .NET Core 1.0
 - .NET Framework 4.5
 - Mono 4.6
 - Xamarin.iOS 10.0
 - Xamarin.Android 7.0
 - Universal Windows Platform 10
 - Windows 8.0
 - Windows Phone 8.1

### Dependencies .NETStandard 1.1

 - NETStandard.Library (>= 1.6.1)
 - Newtonsoft.Json (>= 12.0.3)

## Installation

You can either clone the present Github repository or use NuGet package manager with the following command:

```
PM> Install-Package Mailjet.Api
```

For more information about our Nuget package, follow this [link][nuget].

## Authentication

The Mailjet Email API uses your API and Secret keys for authentication. [Grab](api_credential) and save your Mailjet API credentials.

```
setx -m $MJ_APIKEY_PUBLIC "Enter your API Key here"
setx -m $MJ_APIKEY_PRIVATE "Enter your API Secret here"
```


> Note: For the SMS API the authorization is based on a Bearer token. See information about it in the [SMS API](#sms-api) section of the readme.

## Make your first call

Here's an example on how to send an email:

```csharp
using Mailjet.Client;
using Mailjet.Client.Resources;
using System;
using Newtonsoft.Json.Linq;
namespace Mailjet.ConsoleApplication
{
   class Program
   {
      /// <summary>
      /// Run:
      /// </summary>
      static void Main(string[] args)
      {
         // TODO: blocking call, try to use async Main and async eventhandlers in WinForms/WPF
         RunAsync().Wait();
      }

      static async Task RunAsync()
      {
         MailjetClient client = new MailjetClient(
            Environment.GetEnvironmentVariable("MJ_APIKEY_PUBLIC"), 
            Environment.GetEnvironmentVariable("MJ_APIKEY_PRIVATE"));

         MailjetRequest request = new MailjetRequest
         {
            Resource = Send.Resource,
         }

         // construct your email with builder
         var email = new TransactionalEmailBuilder()
                .WithFrom(new SendContact("from@test.com"))
                .WithSubject("Test subject")
                .WithHtmlPart("<h1>Header</h1>")
                .WithTo(new SendContact("to@test.com"))
                .Build();

         // invoke API to send email
         var response = await _client.SendTransactionalEmailAsync(email);

         // check response
         Assert.AreEqual(1, response.Messages.Length);
      }
   }
}
```

## Client / Call configuration specifics

### Dependency Injection

The MailJet client supports HttpClientFactory.
To configure IMailjetClient to be Typed HttpClient, see an example. 
```csharp
services.AddHttpClient<IMailjetClient, MailjetClient>(client =>
{
    //set BaseAddress, MediaType, UserAgent
    client.SetDefaultSettings();

    client.UseBearerAuthentication("access_token");
    //or
    client.UseBasicAuthentication("apiKey", "apiSecret");
});
```
Then IMailjetClient can be used like:
```csharp
public class EmailService : IEmailService
{
    private readonly IMailjetClient _mailjetClient;

    public EmailService(IMailjetClient mailjetClient)
    {
        _mailjetClient = mailjetClient;
    }
}
```

### API Versioning

The Mailjet API is spread among three distinct versions:

- v3 : `ApiVersion.V3` - The Email API
- v3.1 : `ApiVersion.V3_1` - Email Send API v3.1, which is the latest version of our Send API
- v4 : `ApiVersion.V4` - SMS API, contacts delete API etc

Right now, client wrapper will determine what API version to call under the hood.

For additional information refer to our [API Reference](https://dev.preprod.mailjet.com/reference/overview/versioning/).

### Base URL

The default base domain name for the Mailjet API is `https://api.mailjet.com`. You can modify this base URL by setting a value for `BaseAdress` in your call:

```csharp
MailjetClient client = new MailjetClient(Environment.GetEnvironmentVariable("MJ_APIKEY_PUBLIC"), Environment.GetEnvironmentVariable("MJ_APIKEY_PRIVATE"))
         {
            BaseAdress = "https://api.us.mailjet.com",
         };
```

If your account has been moved to Mailjet's US architecture, the URL value you need to set is `https://api.us.mailjet.com`.

### Proxy

You can set proxy for the client by passing it's parameters to the httpClientHandler:

```csharp
            // create instance of proxy with configuration
            var proxy = new WebProxy
            {
                // Credentials = new NetworkCredential("<proxyusername>", "<proxypassword>"),
                // if your proxy requires credentials, uncomment this line ^^

                // address of the proxy
                // please, note - even if it's https proxy, the address should start from http
                // https://github.com/dotnet/core/issues/2043
                Address = new Uri("http://51.79.xx.xx:8080"),

                UseDefaultCredentials = false
            };

            // pass the created proxy to HttpClientHandler
            HttpClientHandler handler = new HttpClientHandler
            {
                Proxy = proxy,
            };

            // pass HttpClientHandler to the MailjetClient, so client will be using a proxy
            var client = new MailjetClient(Environment.GetEnvironmentVariable("MJ_APIKEY_PUBLIC"),
                Environment.GetEnvironmentVariable("MJ_APIKEY_PRIVATE"), handler);

            // use client here...
```

## List of resources

You can find the list of all available resources for this library in, as well as their configuration, in [/Mailjet.Client/Resources](https://github.com/mailjet/mailjet-apiv3-dotnet/tree/master/Mailjet.Client/Resources). Check the [API reference][api_ref] for complete details on the functionality of these resources.

## Request Examples

### POST Request

Use the `PostAsync` method of the Mailjet CLient (i.e. `MailjetResponse response = await client.PostAsync(request);`).
`request` will be a `MailjetRequest` object.

#### Simple POST request

```csharp
using Mailjet.Client;
using Mailjet.Client.Resources;
using System;
using Newtonsoft.Json.Linq;
namespace Mailjet.ConsoleApplication
{
   class Program
   {
      /// <summary>
      /// Create a new contact:
      /// </summary>
      static void Main(string[] args)
      {
         // TODO: blocking call, try to use async Main and async eventhandlers in WinForms/WPF
         RunAsync().Wait();
      }
      static async Task RunAsync()
      {
         MailjetClient client = new MailjetClient(Environment.GetEnvironmentVariable("MJ_APIKEY_PUBLIC"), Environment.GetEnvironmentVariable("MJ_APIKEY_PRIVATE"));
         MailjetRequest request = new MailjetRequest
         {
            Resource = Contact.Resource,
         }
            .Property(Contact.Email, "passenger@mailjet.com")
            .Property(Contact.IsExcludedFromCampaigns, "false")
            .Property(Contact.Name, "New Contact");
         MailjetResponse response = await client.PostAsync(request);
         if (response.IsSuccessStatusCode)
         {
            Console.WriteLine(string.Format("Total: {0}, Count: {1}\n", response.GetTotal(), response.GetCount()));
            Console.WriteLine(response.GetData());
         }
         else
         {
            Console.WriteLine(string.Format("StatusCode: {0}\n", response.StatusCode));
            Console.WriteLine(string.Format("ErrorInfo: {0}\n", response.GetErrorInfo()));
            Console.WriteLine(response.GetData());
            Console.WriteLine(string.Format("ErrorMessage: {0}\n", response.GetErrorMessage()));
         }
      }
   }
}
```

#### Using actions

To access endpoints with action, you will be able to find Resources object definition. For example, the `/Contact/$ID/Managecontactslists` endpoint can be used with the object `ContactManagecontactslists` available in `Mailjet.Client.Resources` 

```csharp
using Mailjet.Client;
using Mailjet.Client.Resources;
using System;
using Newtonsoft.Json.Linq;
namespace Mailjet.ConsoleApplication
{
   class Program
   {
      /// <summary>
      /// Manage the subscription status of a contact to multiple lists
      /// </summary>
      static void Main(string[] args)
      {
         RunAsync().Wait();
      }
      static async Task RunAsync()
      {
         MailjetClient client = new MailjetClient(Environment.GetEnvironmentVariable("MJ_APIKEY_PUBLIC"), Environment.GetEnvironmentVariable("MJ_APIKEY_PRIVATE"));
         MailjetRequest request = new MailjetRequest
         {
            Resource = ContactManagecontactslists.Resource,
            ResourceId = ResourceId.Numeric(ID)
         }
            .Property(ContactManagecontactslists.ContactsLists, new JArray {
                new JObject {
                 {"ListID", "$ListID_1"},
                 {"Action", "addnoforce"}
                 },
                new JObject {
                 {"ListID", "$ListID_2"},
                 {"Action", "addforce"}
                 }
                });
         MailjetResponse response = await client.PostAsync(request);
         if (response.IsSuccessStatusCode)
         {
            Console.WriteLine(string.Format("Total: {0}, Count: {1}\n", response.GetTotal(), response.GetCount()));
            Console.WriteLine(response.GetData());
         }
         else
         {
            Console.WriteLine(string.Format("StatusCode: {0}\n", response.StatusCode));
            Console.WriteLine(string.Format("ErrorInfo: {0}\n", response.GetErrorInfo()));
            Console.WriteLine(response.GetData());
            Console.WriteLine(string.Format("ErrorMessage: {0}\n", response.GetErrorMessage()));
         }
      }
   }
}
```

### GET request

Use the `PostAsync` method of the Mailjet CLient (i.e. `MailjetResponse response = await client.GetAsync(request);`).
`request` will be a `MailjetRequest` object.

#### Retrieve all objects

```csharp
using Mailjet.Client;
using Mailjet.Client.Resources;
using System;
using Newtonsoft.Json.Linq;
namespace Mailjet.ConsoleApplication
{
   class Program
   {
      /// <summary>
      /// Retrieve all contacts
      /// </summary>
      static void Main(string[] args)
      {
         RunAsync().Wait();
      }
      static async Task RunAsync()
      {
         MailjetClient client = new MailjetClient(Environment.GetEnvironmentVariable("MJ_APIKEY_PUBLIC"), Environment.GetEnvironmentVariable("MJ_APIKEY_PRIVATE"));
         MailjetRequest request = new MailjetRequest
         {
            Resource = Contact.Resource,
         }
         MailjetResponse response = await client.GetAsync(request);
         if (response.IsSuccessStatusCode)
         {
            Console.WriteLine(string.Format("Total: {0}, Count: {1}\n", response.GetTotal(), response.GetCount()));
            Console.WriteLine(response.GetData());
         }
         else
         {
            Console.WriteLine(string.Format("StatusCode: {0}\n", response.StatusCode));
            Console.WriteLine(string.Format("ErrorInfo: {0}\n", response.GetErrorInfo()));
            Console.WriteLine(response.GetData());
            Console.WriteLine(string.Format("ErrorMessage: {0}\n", response.GetErrorMessage()));
         }
      }
   }
}
```

#### Use filtering

In case API endpoint supports query params filtering, you can add filter for your API call on the `MailjetRequest` by using the `Filter` method. 
Example: `.Filter(Contact.IsExcludedFromCampaigns, "false")`

```csharp
using Mailjet.Client;
using Mailjet.Client.Resources;
using System;
using Newtonsoft.Json.Linq;
namespace Mailjet.ConsoleApplication
{
   class Program
   {
      /// <summary>
      /// Retrieve all contacts
      /// </summary>
      static void Main(string[] args)
      {
         RunAsync().Wait();
      }
      static async Task RunAsync()
      {
         MailjetClient client = new MailjetClient(Environment.GetEnvironmentVariable("MJ_APIKEY_PUBLIC"), Environment.GetEnvironmentVariable("MJ_APIKEY_PRIVATE"));
         MailjetRequest request = new MailjetRequest
         {
            Resource = Contact.Resource,
         }
         .Filter(Contact.IsExcludedFromCampaigns, "false")         
         MailjetResponse response = await client.GetAsync(request);
         if (response.IsSuccessStatusCode)
         {
            Console.WriteLine(string.Format("Total: {0}, Count: {1}\n", response.GetTotal(), response.GetCount()));
            Console.WriteLine(response.GetData());
         }
         else
         {
            Console.WriteLine(string.Format("StatusCode: {0}\n", response.StatusCode));
            Console.WriteLine(string.Format("ErrorInfo: {0}\n", response.GetErrorInfo()));
            Console.WriteLine(response.GetData());
            Console.WriteLine(string.Format("ErrorMessage: {0}\n", response.GetErrorMessage()));
         }
      }
   }
}
```

To filter resource by it's identifier, common REST style convention is used, so you can specify resource id in the request itself. For example, you can get single contact by email:

```csharp
MailjetRequest request = new MailjetRequest
{
    Resource = Contact.Resource,
    ResourceId = ResourceId.Alphanumeric(email)
};

MailjetResponse response = await client.GetAsync(request);
```

#### Retrieve a single object

When instantiating the `MailjetRequest`, you can specify the Id of the resource you want to access with `ResourceId` (example: `ResourceId = ResourceId.Numeric(ID)`).

```csharp
using Mailjet.Client;
using Mailjet.Client.Resources;
using System;
using Newtonsoft.Json.Linq;
namespace Mailjet.ConsoleApplication
{
   class Program
   {
      /// <summary>
      /// View : Retrieve a specific contact. Includes information about contact status and creation / activity timestamps.
      /// </summary>
      static void Main(string[] args)
      {
         RunAsync().Wait();
      }
      static async Task RunAsync()
      {
         MailjetClient client = new MailjetClient(Environment.GetEnvironmentVariable("MJ_APIKEY_PUBLIC"), Environment.GetEnvironmentVariable("MJ_APIKEY_PRIVATE"));
         MailjetRequest request = new MailjetRequest
         {
            Resource = Contact.Resource,
            ResourceId = ResourceId.Numeric(ID)
         }
         MailjetResponse response = await client.GetAsync(request);
         if (response.IsSuccessStatusCode)
         {
            Console.WriteLine(string.Format("Total: {0}, Count: {1}\n", response.GetTotal(), response.GetCount()));
            Console.WriteLine(response.GetData());
         }
         else
         {
            Console.WriteLine(string.Format("StatusCode: {0}\n", response.StatusCode));
            Console.WriteLine(string.Format("ErrorInfo: {0}\n", response.GetErrorInfo()));
            Console.WriteLine(response.GetData());
            Console.WriteLine(string.Format("ErrorMessage: {0}\n", response.GetErrorMessage()));
         }
      }
   }
}
```

### PUT request

A `PUT` request in the Mailjet API will work as a `PATCH` request - the update will affect only the specified properties. The other properties of an existing resource will neither be modified, nor deleted. It also means that all non-mandatory properties can be omitted from your payload.

Use the `PutAsync` method of the Mailjet CLient (i.e. `MailjetResponse response = await client.PutAsync(request);`).
`request` will be a `MailjetRequest` object.

```csharp
using Mailjet.Client;
using Mailjet.Client.Resources;
using System;
using Newtonsoft.Json.Linq;
namespace Mailjet.ConsoleApplication
{
   class Program
   {
      /// <summary>
      /// Modify : Modify the static custom contact data
      /// </summary>
      static void Main(string[] args)
      {
         RunAsync().Wait();
      }
      static async Task RunAsync()
      {
         MailjetClient client = new MailjetClient(Environment.GetEnvironmentVariable("MJ_APIKEY_PUBLIC"), Environment.GetEnvironmentVariable("MJ_APIKEY_PRIVATE"));
         MailjetRequest request = new MailjetRequest
         {
            Resource = Contactdata.Resource,
            ResourceId = ResourceId.Numeric(ID)
         }
            .Property(Contactdata.Data, new JArray {
                new JObject {
                 {"first_name", "John"},
                 {"last_name", "Smith"}
                 }
                });
         MailjetResponse response = await client.PutAsync(request);
         if (response.IsSuccessStatusCode)
         {
            Console.WriteLine(string.Format("Total: {0}, Count: {1}\n", response.GetTotal(), response.GetCount()));
            Console.WriteLine(response.GetData());
         }
         else
         {
            Console.WriteLine(string.Format("StatusCode: {0}\n", response.StatusCode));
            Console.WriteLine(string.Format("ErrorInfo: {0}\n", response.GetErrorInfo()));
            Console.WriteLine(response.GetData());
            Console.WriteLine(string.Format("ErrorMessage: {0}\n", response.GetErrorMessage()));
         }
      }
   }
}
```

### DELETE request

Upon a successful `DELETE` request the response will not include a response body, but only a `204 No Content` response code.

Use the `DeleteAsync` method of the Mailjet CLient (i.e. `MailjetResponse response = await client.DeleteAsync(request);`).
`request` will be a `MailjetRequest` object.

```csharp
using Mailjet.Client;
using Mailjet.Client.Resources;
using System;
using Newtonsoft.Json.Linq;
namespace Mailjet.ConsoleApplication
{
   class Program
   {
      /// <summary>
      /// Delete : Delete an email template.
      /// </summary>
      static void Main(string[] args)
      {
         RunAsync().Wait();
      }
      static async Task RunAsync()
      {
         MailjetClient client = new MailjetClient(Environment.GetEnvironmentVariable("MJ_APIKEY_PUBLIC"), Environment.GetEnvironmentVariable("MJ_APIKEY_PRIVATE"));
         MailjetRequest request = new MailjetRequest
         {
            Resource = Template.Resource,
            ResourceId = ResourceId.Numeric(ID)
         }
         MailjetResponse response = await client.DeleteAsync(request);
         if (response.IsSuccessStatusCode)
         {
            Console.WriteLine(string.Format("Total: {0}, Count: {1}\n", response.GetTotal(), response.GetCount()));
            Console.WriteLine(response.GetData());
         }
         else
         {
            Console.WriteLine(string.Format("StatusCode: {0}\n", response.StatusCode));
            Console.WriteLine(string.Format("ErrorInfo: {0}\n", response.GetErrorInfo()));
            Console.WriteLine(response.GetData());
            Console.WriteLine(string.Format("ErrorMessage: {0}\n", response.GetErrorMessage()));
         }
      }
   }
}
```
## SMS API

### Token authentication

Authentication for the SMS API endpoints is done using a bearer token. The bearer token is generated in the [SMS section](https://app.mailjet.com/sms) of your Mailjet account.

To use a Bearer token you will need to client clientin from:

```csharp
...
	MailjetClient client = new MailjetClient(Environment.GetEnvironmentVariable("Your_Bearer_token"));
...
```

### Example request

```csharp
using Mailjet.Client;
using Mailjet.Client.Resources.SMS;
using System;
using Newtonsoft.Json.Linq;
namespace Mailjet.ConsoleApplication
{
   class Program
   {
      /// <summary>
      /// Send an SMS:
      /// </summary>
      static void Main(string[] args)
      {
         RunAsync().Wait();
      }
      static async Task RunAsync()
      {
         MailjetClient client = new MailjetClient(Environment.GetEnvironmentVariable("Your_Bearer_token"));

         MailjetRequest request = new MailjetRequest
         {
            Resource = Send.Resource,
         }
         .Property(Send.From, "MJ Pilot")
         .Property(Send.To, "+336000000000")
         .Property(Send.Text, "Have a nice SMS flight with Mailjet !");

         MailjetResponse response = await client.PostAsync(request);
         if (response.IsSuccessStatusCode)
         {
            Console.WriteLine(string.Format("Total: {0}, Count: {1}\n", response.GetTotal(), response.GetCount()));
            Console.WriteLine(response.GetData());
         }
         else
         {
            Console.WriteLine(string.Format("StatusCode: {0}\n", response.StatusCode));
            Console.WriteLine(string.Format("ErrorInfo: {0}\n", response.GetErrorInfo()));
            Console.WriteLine(response.GetData());
            Console.WriteLine(string.Format("ErrorMessage: {0}\n", response.GetErrorMessage()));
         }
      }
   }
}
```
### Response 

The `GetAsync`, `PostAsync`, `PutAsync` and `DeleteAsync` method will return a `MailjetResponse` object with the following available methods and properties:

 - `IsSuccessStatusCode` : indicate if the API call was successful
 - `StatusCode` : http status code (ie: 200,400 ...)
 - `GetData()` : content of the property `data` of the JSON response payload if exist or the full JSON payload returned by the API call. This will be PHP associative array.   
 - `GetCount()` : number of elements returned in the response
 - `GetErrorInfo()` : http response message phrases ("OK", "Bad Request" ...)
 - `GetErrorMessage()` :  error reason message from the API response payload


## More examples

* [Send with attached files](https://dev.mailjet.com/email/guides/send-api-v31/#send-with-attached-files)

* [GDPR Delete contacts](https://dev.mailjet.com/email/guides/contact-management/#gdpr-delete-contacts)
* and many more can be found can under [guides section](https://dev.mailjet.com/email/guides/) on our docs site

## Contribute

Mailjet loves developers. You can be part of this project!

This wrapper is a great introduction to the open source world, check out the code!

Feel free to ask anything, and contribute:

- Fork the project.
- Create a new branch.
- Implement your feature or bug fix.
- Add documentation to it.
- Commit, push, open a pull request and voila.

If you have suggestions on how to improve the guides, please submit an issue in our [Official API Documentation repo](https://github.com/mailjet/api-documentation).
