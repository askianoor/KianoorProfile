using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Askianoor.Models
{
    public class ApplicationSettings
    {
        public string JWTSecret { get; set; }

        private string ClientURL { get; set; }

        public string AdminRoleName { get; set; }

        public string ReCaptchaSecretKey { get; set; }

        public string ContactEmail { get; set; }

        public string SmtpServer { get; set; }

        public string SmtpPort { get; set; }

        public string SmtpUser { get; set; }

        public string SmtpPassword { get; set; }
    }
}
