using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Askianoor.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using Newtonsoft.Json;
using System.Globalization;

namespace Askianoor.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecaptchaController : ControllerBase
    {
        private readonly ApplicationSettings _appSettings;

        public RecaptchaController(IOptions<ApplicationSettings> appSettings)
        {
            if (appSettings != null)
            {
                _appSettings = appSettings.Value;
            }
        }

        // POST: api/Recaptcha
        [HttpPost]
        public ActionResult<ReCaptcha> ReCaptchaCheck(string token)
        {
            if (token == null)
            {
                return BadRequest();
            }

            try
            {
                using (var client = new WebClient())
                {

                    string secretKey = _appSettings.ReCaptchaSecretKey;
                    var gReply = client.DownloadString(string.Format(new CultureInfo("en-US"), "https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, token));

                    var response = JsonConvert.DeserializeObject<ReCaptcha>(gReply);
                    if (response.Success.ToLower(new CultureInfo ("en-US")) == "true")
                    {
                        return response;
                    }
                    else
                    {
                        return BadRequest();
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest();
                throw;
            }
        }

    }
}