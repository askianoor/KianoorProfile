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
    }
}
