using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Askianoor.Models
{
    public class ReCaptcha
    {
        public string Success { get; set; }
        public string Score { get; set; }
        public string ChallengeTs { get; set; }
        public string HostName { get; set; }
    }
}
