using Mailjet.Client.Exceptions;
using Mailjet.Client.TransactionalEmails;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mailjet.Tests;

[TestClass]
public class TransactionalEmailBuilderTests
{
    [TestMethod]
    public void BuildTransactionEmail_WhenNoTemplateIdAndTemplateProvided_ReturnsEmailObject()
    {
        var variables = new Dictionary<string, object> { { "day", "Friday" } };
        var email = new TransactionalEmailBuilder()
            .WithFrom(new SendContact("test@mailjet.com"))
            .WithSubject("Test subject")
            .WithHtmlPart("<h1>Happy {{var:day}}</h1>")
            .WithTo(new SendContact("test@mailjet.com"))
            .WithTemplateLanguage(true)
            .WithVariables(variables)
            .Build();
        Assert.IsNotNull(email);
    }

    [TestMethod]
    public void BuildTransactionEmail_WhenTemplateIdProvided_ReturnsEmailObject()
    {
        var email = new TransactionalEmailBuilder()
            .WithFrom(new SendContact("test@mailjet.com"))
            .WithSubject("Test subject")
            .WithTo(new SendContact("test@mailjet.com"))
            .WithTemplateId(1)
            .WithTemplateLanguage(true)
            .Build();
        Assert.IsNotNull(email);
    }

    [TestMethod]
    public void BuildTransactionEmail_WhenNoFromAddressAndTemplateIdProvided__ReturnsEmailObject()
    {
        var email = new TransactionalEmailBuilder()
            .WithTemplateId(1)
            .WithSubject("Test subject")
            .WithTo(new SendContact("test@mailjet.com"))
            .Build();
        Assert.IsNotNull(email);
    }

    [TestMethod]
    [ExpectedException(typeof(MailjetClientConfigurationException))]
    public void BuildTransactionEmail_WhenNoFromAddress_ThrowsException()
    {
        new TransactionalEmailBuilder()
            .WithSubject("Test subject")
            .WithHtmlPart("<h1>Test</h1>")
            .WithTo(new SendContact("test@mailjet.com"))
            .Build();
    }

    [TestMethod]
    [ExpectedException(typeof(MailjetClientConfigurationException))]
    public void BuildTransactionEmail_WhenNoTemplateIdAndNoHtmlPartAndNoTextPart_ThrowsException()
    {
        new TransactionalEmailBuilder()
            .WithFrom(new SendContact("test@mailjet.com"))
            .WithSubject("Test subject")
            .WithTo(new SendContact("test@mailjet.com"))
            .Build();
    }

    [TestMethod]
    [ExpectedException(typeof(MailjetClientConfigurationException))]
    public void BuildTransactionEmail_WhenTemplateIdAndHtmlPartProvided_ThrowsException()
    {
        new TransactionalEmailBuilder()
            .WithTemplateId(1)
            .WithFrom(new SendContact("test@mailjet.com"))
            .WithHtmlPart("<h1>Test</h1>")
            .WithTo(new SendContact("test@mailjet.com"))
            .Build();
    }

    [TestMethod]
    [ExpectedException(typeof(MailjetClientConfigurationException))]
    public void BuildTransactionEmail_WhenTemplateIdAndTextPartProvided_ThrowsException()
    {
        new TransactionalEmailBuilder()
            .WithTemplateId(1)
            .WithFrom(new SendContact("test@mailjet.com"))
            .WithTextPart("Test")
            .WithTo(new SendContact("test@mailjet.com"))
            .Build();
    }

    [TestMethod]
    [ExpectedException(typeof(MailjetClientConfigurationException))]
    public void BuildTransactionEmail_WhenNoTemplateIdAndTemplateErrorDeliverProvided_ThrowsException()
    {
        new TransactionalEmailBuilder()
            .WithTemplateErrorDeliver(true)
            .WithFrom(new SendContact("test@mailjet.com"))
            .WithTextPart("Test")
            .WithTo(new SendContact("test@mailjet.com"))
            .Build();
    }

    [TestMethod]
    [ExpectedException(typeof(MailjetClientConfigurationException))]
    public void BuildTransactionEmail_WhenNoTemplateIdAndTemplateErrorReportingProvided_ThrowsException()
    {
        new TransactionalEmailBuilder()
            .WithTemplateErrorReporting(new SendContact("test@mailjet.com"))
            .WithFrom(new SendContact("test@mailjet.com"))
            .WithTextPart("Test")
            .WithTo(new SendContact("test@mailjet.com"))
            .Build();
    }
}
