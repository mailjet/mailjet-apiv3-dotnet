
[doc]: http://dev.mailjet.com/
[api_credential]: https://app.mailjet.com/account/api_keys
[issues]: https://github.com/mailjet/mailjet-apiv3-dotnet/issues
[go_documentation]:http://dev.mailjet.com/guides/?csharp
[api_doc_repo]:https://github.com/mailjet/api-documentation

![alt text](http://cdn.appstorm.net/web.appstorm.net/files/2012/02/mailjet_logo_200x200.png "Mailjet")

[![Build Status](https://travis-ci.org/mailjet/mailjet-apiv3-go.svg?branch=master)](https://travis-ci.org/mailjet/mailjet-apiv3-go)
[![GoDoc](https://godoc.org/github.com/mailjet/mailjet-apiv3-go?status.svg)](https://godoc.org/github.com/mailjet/mailjet-apiv3-go)

# Official Mailjet API v3 .NET wrapper

This .NET library is a client for version 3 of the [Mailjet API][doc].

## Getting Started

Every code examples can be find on the [Mailjet Documentation][go_documentation]

(Please refer to the [Mailjet Documentation Repository][api_doc_repo] to contribute to the documentation examples)

### Prerequisites

Make sure you have the following requirements:
* A Mailjet API Key
* A Mailjet API Secret Key
* 

Both API key and an API secret can be found [here][api_credential].



### Installation

Get package:

It's ready to use !

## Examples

### List resources

### Create a resource

```C#

using Mailjet.Client;
using Mailjet.Client.Resources;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace Mailjet.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            RunAsync().Wait();
        }

        static async Task RunAsync()
        {
            MailjetClient client = new MailjetClient(ConfigurationManager.AppSettings["apiKey"], ConfigurationManager.AppSettings["apiSecret"]);

            MailjetRequest request = new MailjetRequest()
            {
                Resource = Contact.Resource,
            }
            .Property(Contact.Email, "Mister@mailjet.com");

            MailjetResponse response = await client.PostAsync(request);

            Console.WriteLine(string.Format("StatusCode: {0}\n", response.StatusCode));

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(string.Format("Total: {0}, Count: {1}\n", response.GetTotal(), response.GetCount()));
                Console.WriteLine(response.GetData());
            }
            else
            {
                Console.WriteLine(string.Format("ErrorInfo: {0}\n", response.GetErrorInfo()));
                Console.WriteLine(string.Format("ErrorMessage: {0}\n", response.GetErrorMessage()));
            }

            Console.ReadLine();
        }
    }
}

```

### Update a resource

### View a resource

Using the resource ID (ResourceId.Numeric):

```C#
using Mailjet.Client;
using Mailjet.Client.Resources;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace Mailjet.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            RunAsync().Wait();
        }

        static async Task RunAsync()
        {
            MailjetClient client = new MailjetClient(ConfigurationManager.AppSettings["apiKey"], ConfigurationManager.AppSettings["apiSecret"]);

            MailjetRequest request = new MailjetRequest()
            {
                Resource = Contact.Resource,
                ResourceId = ResourceId.Numeric(1727238050),
            };

            MailjetResponse response = await client.GetAsync(request);

            Console.WriteLine(string.Format("StatusCode: {0}\n", response.StatusCode));

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(string.Format("Total: {0}, Count: {1}\n", response.GetTotal(), response.GetCount()));
                Console.WriteLine(response.GetData());
            }
            else
            {
                Console.WriteLine(string.Format("ErrorInfo: {0}\n", response.GetErrorInfo()));
                Console.WriteLine(string.Format("ErrorMessage: {0}\n", response.GetErrorMessage()));
            }

            Console.ReadLine();
        }
    }
}
```

Using the resource alternate ID (ResourceId.Alphanumeric):

```C#
using Mailjet.Client;
using Mailjet.Client.Resources;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace Mailjet.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            RunAsync().Wait();
        }

        static async Task RunAsync()
        {
            MailjetClient client = new MailjetClient(ConfigurationManager.AppSettings["apiKey"], ConfigurationManager.AppSettings["apiSecret"]);

            MailjetRequest request = new MailjetRequest()
            {
                Resource = Contact.Resource,
                ResourceId = ResourceId.Alphanumeric("mister@mailjet.com"),
            };

            MailjetResponse response = await client.GetAsync(request);

            Console.WriteLine(string.Format("StatusCode: {0}\n", response.StatusCode));

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(string.Format("Total: {0}, Count: {1}\n", response.GetTotal(), response.GetCount()));
                Console.WriteLine(response.GetData());
            }
            else
            {
                Console.WriteLine(string.Format("ErrorInfo: {0}\n", response.GetErrorInfo()));
                Console.WriteLine(string.Format("ErrorMessage: {0}\n", response.GetErrorMessage()));
            }

            Console.ReadLine();
        }
    }
}
```

### Delete a resource

### Send a mail

## Contribute

We welcome any contribution.

Please make sure you follow this step-by-step guide before contributing :

* Fork the project.
* Create a topic branch.
* Implement your feature or bug fix.
* Add documentation for your feature or bug fix.
* Commit and push your changes.
* Submit a pull request

Submit your issues [here][issues].

