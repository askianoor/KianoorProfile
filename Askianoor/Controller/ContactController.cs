using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Askianoor.Models.Main;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using Askianoor.Models;
using Microsoft.Extensions.Options;

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
        public bool PostContact(Contact contact)
        {
            if (contact == null)
            {
                return false;
            }

            MailAddress to = new MailAddress(_appSettings.ContactEmail);
            MailAddress from = new MailAddress(contact.Email);

            MailMessage message = new MailMessage(from, to);
            message.Subject = contact.Subject;
            message.Body = contact.Message;

            SmtpClient client = new SmtpClient("smtp.server.address", 2525)
            {
                Credentials = new NetworkCredential("smtp_username", "smtp_password"),
                EnableSsl = true
            };

            try
            {
                client.Send(message);
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return CreatedAtAction("GetSkill", new { id = skill.SkillId }, skill);
        }
    }
}