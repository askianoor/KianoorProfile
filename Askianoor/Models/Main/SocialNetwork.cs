using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Askianoor.Models
{
    public class SocialNetwork
    {
        [Key]
        public Guid SocialId { get; set; }

        public string Name { get; set; }
        public string Icon { get; set; }
        public string Link { get; set; }
    }
}
