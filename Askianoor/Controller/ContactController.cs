using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Askianoor.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using Askianoor.Models;
using Microsoft.Extensions.Options;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices.WindowsRuntime;

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

            MailAddress to = new MailAddress(_appSettings.ContactEmail);
            MailAddress from = new MailAddress(contact.Email);

            try
            {
                if (string.IsNullOrEmpty(to.Address) || string.IsNullOrEmpty(from.Address))
                    return BadRequest();

                MailMessage message = new MailMessage(from, to);
                message.Subject = contact.Subject;
                message.Body = contact.Message;

                int port = Convert.ToInt16(_appSettings.SmtpPort, new CultureInfo("en-us"));

                SmtpClient client = new SmtpClient(_appSettings.SmtpServer, port)
                {
                    Credentials = new NetworkCredential(_appSettings.SmtpUser, _appSettings.SmtpPassword),
                    EnableSsl = true
                };


                client.Send(message);
                message.Dispose();
                client.Dispose();
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest();
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest();
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest();
            }

            return contact;
        }
    }
}