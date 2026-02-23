using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mailjet.Repositories.Models.MailJet;

namespace Mailjet.Repositories.Exceptions
{
    public class MailJetException: Exception
    {
        public MailJetExceptionModel ExceptionData { get; set; }

        public MailJetException(MailJetExceptionModel exceptionData)
        {
            this.ExceptionData = exceptionData;
        }
    }
}
