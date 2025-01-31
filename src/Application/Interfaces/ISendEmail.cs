using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISendEmail
    {
        void SendEmail(string to, string subject , string body);
    }
}