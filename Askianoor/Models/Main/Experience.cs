using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Askianoor.Models
{
    public class Experience
    {
        [Key]
        public Guid ExperienceId { get; set; }

        public string jobTitle { get; set; }
        public string companyTitle { get; set; }
        public string companyAddress { get; set; }
        public string description { get; set; }
        public string year { get; set; }
        public string icon { get; set; }
    }
}
