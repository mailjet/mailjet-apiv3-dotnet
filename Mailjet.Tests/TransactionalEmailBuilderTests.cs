using Mailjet.Client.Exceptions;
using Mailjet.Client.TransactionalEmails;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Mailjet.Tests
{
    [TestClass]
    public class TransactionalEmailBuilderTests
    {
        [TestMethod]
        public void BuildTransactionEmail_WhenNoTemplateIdAndTemplateProvided_ReturnsEmailObject()
        {
            // arrange
            var variables = new Dictionary<string, object>
            {
                { "day", "Friday" }
            };

            // act
            var email = new TransactionalEmailBuilder()
                .WithFrom(new SendContact("test@mailjet.com"))
                .WithSubject("Test subject")
                .WithHtmlPart("<h1>Happy {{var:day}}</h1>")
                .WithTo(new SendContact("test@mailjet.com"))
                .WithTemplateLanguage(true)
                .WithVariables(variables)
                .Build();

            // assert
            Assert.IsNotNull(email);
        }

        [TestMethod]
        public void BuildTransactionEmail_WhenTemplateIdProvided_ReturnsEmailObject()
        {
            // arrange
            var variables = new Dictionary<string, object>
            {
                { "day", "Friday" }
            };

            // act
            var email = new TransactionalEmailBuilder()
                .WithFrom(new SendContact("test@mailjet.com"))
                .WithSubject("Test subject")
                .WithTo(new SendContact("test@mailjet.com"))
                .WithTemplateId(1)
                .WithTemplateLanguage(true)
                .WithVariables(variables)
                .Build();

            // assert
            Assert.IsNotNull(email);
        }

        [TestMethod]
        public void BuildTransactionEmail_WhenNoFromAddressAndTemplateIdProvided__ReturnsEmailObject()
        {
            // arrange
            var variables = new Dictionary<string, object>
            {
                { "day", "Friday" }
            };

            // act
            var email = new TransactionalEmailBuilder()
                .WithTemplateId(1)
                .WithSubject("Test subject")
                .WithTo(new SendContact("test@mailjet.com"))
                .WithTemplateLanguage(true)
                .WithVariables(variables)
                .Build();

            // assert
            Assert.IsNotNull(email);
        }

        [TestMethod]
        [ExpectedException(typeof(MailjetClientConfigurationException))]
        public void BuildTransactionEmail_WhenNoFromAddress_ThrowsException()
        {
            // arrange
            var variables = new Dictionary<string, object>
            {
                { "day", "Friday" }
            };

            // act
            new TransactionalEmailBuilder()
                .WithSubject("Test subject")
                .WithHtmlPart("<h1>Happy {{var:day}}</h1>")
                .WithTo(new SendContact("test@mailjet.com"))
                .WithTemplateLanguage(true)
                .WithVariables(variables)
                .Build();
        }

        [TestMethod]
        [ExpectedException(typeof(MailjetClientConfigurationException))]
        public void BuildTransactionEmail_WhenNoTemplateIdAndNoHtmlPartAndNoTextPart_ThrowsException()
        {
            // act
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
            // act
            new TransactionalEmailBuilder()
                .WithTemplateId(1)
                .WithFrom(new SendContact("test@mailjet.com"))
                .WithSubject("Test subject")
                .WithHtmlPart("<h1>Happy {{var:day}}</h1>")
                .WithTo(new SendContact("test@mailjet.com"))
                .Build();
        }

        [TestMethod]
        [ExpectedException(typeof(MailjetClientConfigurationException))]
        public void BuildTransactionEmail_WhenTemplateIdAndTextPartProvided_ThrowsException()
        {
            // act
            new TransactionalEmailBuilder()
                .WithTemplateId(1)
                .WithFrom(new SendContact("test@mailjet.com"))
                .WithSubject("Test subject")
                .WithTextPart("<h1>Happy Friday</h1>")
                .WithTo(new SendContact("test@mailjet.com"))
                .Build();
        }

        [TestMethod]
        [ExpectedException(typeof(MailjetClientConfigurationException))]
        public void BuildTransactionEmail_WhenNoTemplateIdAndTemplateErrorDeliverProvided_ThrowsException()
        {
            // act
            new TransactionalEmailBuilder()
                .WithTemplateErrorDeliver(true)
                .WithFrom(new SendContact("test@mailjet.com"))
                .WithSubject("Test subject")
                .WithTextPart("<h1>Happy Friday</h1>")
                .WithTo(new SendContact("test@mailjet.com"))
                .Build();
        }

        [TestMethod]
        [ExpectedException(typeof(MailjetClientConfigurationException))]
        public void BuildTransactionEmail_WhenNoTemplateIdAndTemplateErrorReportingProvided_ThrowsException()
        {
            // act
            new TransactionalEmailBuilder()
                .WithTemplateErrorReporting(new SendContact("test@mailjet.com"))
                .WithFrom(new SendContact("test@mailjet.com"))
                .WithSubject("Test subject")
                .WithTextPart("<h1>Happy Friday</h1>")
                .WithTo(new SendContact("test@mailjet.com"))
                .Build();
        }
    }
}
