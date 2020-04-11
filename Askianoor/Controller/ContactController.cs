using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Askianoor.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
//using System.Net.Mail;
using Microsoft.Extensions.Options;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices.WindowsRuntime;
using MailKit.Security;
using MimeKit;
using MailKit.Net.Smtp;

namespace Askianoor.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly ApplicationSettings _appSettings;

        public ContactController( IOptions<ApplicationSettings> appSettings)
        { 
            if (appSettings != null)
            {
                _appSettings = appSettings.Value;
            }
        }

        [HttpPost]
        public ActionResult<Contact> PostContact(Contact contact)
        {
            if (contact == null)
            {
                return BadRequest();
            }

            try
            {
                if (string.IsNullOrEmpty(_appSettings.ContactEmail) || string.IsNullOrEmpty(contact.Email))
                    return BadRequest();

                var message = new MimeMessage();
                var bodyBuilder = new BodyBuilder();

                // from
                message.From.Add(new MailboxAddress(contact.Name, contact.Email));
                // to
                message.To.Add(new MailboxAddress("Admin", _appSettings.ContactEmail));

                //// reply to
                //message.ReplyTo.Add(new MailboxAddress("reply_name", "reply_email@example.com"));

                message.Subject = contact.Subject;
                bodyBuilder.HtmlBody = "Email From : " + contact.Email + " and Message is : " + contact.Message;
                message.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    int port = Convert.ToInt16(_appSettings.SmtpPort, new CultureInfo("en-us"));

                    //client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Connect(_appSettings.SmtpServer, port, SecureSocketOptions.SslOnConnect);
                    client.Authenticate(_appSettings.SmtpUser, _appSettings.SmtpPassword);
                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(new { message = "Please Complete all the requirements." });
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(new { message = "Email Format is Wrong!" });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(new { message = ex.Message });
                throw;
            }


            return contact;
        }
    }
}