using Microsoft.AspNetCore.Http;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualClinic.Services.EmailService
{
    public class MailRequest
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public MimeEntity Body { get; set; }
        public List<IFormFile> Attachments { get; set; }
        public string UserName { get; set; }
    }
}
